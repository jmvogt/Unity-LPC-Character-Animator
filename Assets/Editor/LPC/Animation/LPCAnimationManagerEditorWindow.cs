using Assets.Editor.Funkhouse;
using Assets.Scripts.Editor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

public class LPCAnimationManagerEditorWindow : FunkyEditorWindow {

    private static LPCGlobalAnimationConfiguration data;
    private float _importProgress;
    private bool _isImporting;

    private const string _assetPath = "Assets/Generated/GlobalAnimationConfiguration.asset";

    private Vector2 _verticalScrollPosition = Vector2.zero;
    private string _lpcMetadataFolder = "Assets/LPC/sheet_definitions/";

    private readonly string[] _primaryTabs = new string[] { "Import", "Slot Types" };
    private readonly string[] _secondaryImportTabs = new string[] { "LPC Spritesheet", "LPC Unity Assets" };

    private int _currentPrimaryTab;
    private int _currentSecondaryTab;

    // This method will be called on load or recompile
    [InitializeOnLoadMethod]
    private static void OnLoad() {
        IdentifyProperties<LPCGlobalAnimationConfiguration>();
        LoadAsset(ref data, _assetPath);
    }

    [MenuItem("Window/Liberated Pixel Cup/Animations")]
    public static void ShowWindow() {
        GetWindow(typeof(LPCAnimationManagerEditorWindow)).Show();
    }

    private void RenderImportFromSpritesheetView() {

        GUILayout.Label("Fetch From Metadata (optional)", EditorStyles.boldLabel);
        GUILayout.Label("To automatically fetch all TextDefinitions from sanderfrenken's LPC Sprite Character Generator Github page, provide the path to the spritesheets_definitions folder and click on the 'fetch' button.", EditorStyles.wordWrappedMiniLabel);
        GUILayout.Label("If you would rather build your list selectively, skip this step and proceed to building the definitions list down below.", EditorStyles.wordWrappedMiniLabel);

        GUILayout.BeginHorizontal();

        _lpcMetadataFolder = GUILayout.TextField(_lpcMetadataFolder);

        if (GUILayout.Button("Fetch")) {
            data.Definitions.Clear();

            Debug.Log($"PATH: {Application.dataPath + _lpcMetadataFolder}");

            var applicationPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Assets"));
            string[] metadataFiles = Directory.GetFiles(
                applicationPath + _lpcMetadataFolder,
                "*.json",
                SearchOption.AllDirectories);

            foreach (var metadataFile in metadataFiles) {
                //if (metadataFile.GetType() == typeof(TextAsset))
                Debug.Log($"Loading asset at {metadataFile}");
                var asset = (TextAsset)AssetDatabase.LoadAssetAtPath(
                    metadataFile.Replace(applicationPath, ""), typeof(TextAsset));
                data.Definitions.Add(asset);
            }
        }
        GUILayout.EndHorizontal();

        if (data.CharacterSlotTypes.Count == 0) {
            GUILayout.Label("Begin Import", EditorStyles.boldLabel);
            GUILayout.Label("Generate Unity assets from the below definitions list. This process may take a few minutes.", EditorStyles.wordWrappedMiniLabel);
            if (GUILayout.Button("Import"))
                LoadDefinitions();
        } else {
            GUILayout.Label("Reset", EditorStyles.boldLabel);
            GUILayout.Label("Clear all defintiions from the below list.", EditorStyles.wordWrappedMiniLabel);
            if (GUILayout.Button("Reset"))
                ResetDefinitions();
        }
    }

    private void RenderImportFromUnityView() {

    }

    private void RenderSlotTypeView() {
        // This will cause a crash. Paginate?
        if (GUILayout.Button("Load Races")) {
            var applicationPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Assets"));
            string[] metadataFiles = Directory.GetFiles(
                applicationPath + "Assets/Generated/Models/",
                "*.asset",
                SearchOption.AllDirectories);

            foreach (var metadataFile in metadataFiles) {
                //if (metadataFile.GetType() == typeof(TextAsset))
                Debug.Log($"attempting to load asset at {metadataFile}");
                var asset = (LPCCharacterTypeConfiguration)AssetDatabase.LoadAssetAtPath(
                    metadataFile.Replace(applicationPath, ""), typeof(LPCCharacterTypeConfiguration));
                //data.Races.Add(asset);
            }
        }
    }

    private void OnGUI() {
        if (_isImporting)
            EditorUtility.DisplayProgressBar("LPC Importer", "Building LPC animations", _importProgress);

        _verticalScrollPosition = GUILayout.BeginScrollView(_verticalScrollPosition);

        _currentPrimaryTab = GUILayout.Toolbar(_currentPrimaryTab, _primaryTabs);
        _currentSecondaryTab = GUILayout.Toolbar(_currentSecondaryTab, _secondaryImportTabs);

        DrawGuiLine();

        switch (_currentPrimaryTab) {
            case 0:
                switch (_currentSecondaryTab) {
                    case 0:
                        RenderImportFromSpritesheetView();
                        break;
                    case 1:

                        break;
                }
                break;
            case 1:
                RenderSlotTypeView();
                break;
        }

        RenderProperties(data, primaryTab: _primaryTabs[_currentPrimaryTab], secondaryTab: _secondaryImportTabs[_currentSecondaryTab]);

        GUILayout.EndScrollView();
    }

    private void ResetDefinitions() {
        Debug.Log("Unloading LPC animations.");
        data.CharacterSlotTypes.Clear();
        //data.Races.Clear();
    }

    private IEnumerator ParseDefinitions() {
        _isImporting = true;
        EditorUtility.DisplayProgressBar("Simple Progress Bar", "Building LPC animations", _importProgress);

        var definitionsLoaded = 0;
        try {
            foreach (var sheetDefinition in data.Definitions) {
                dynamic definition = JsonConvert.DeserializeObject(sheetDefinition.text);

                string typeName = definition.type_name;
                if (!data.CharacterSlotTypes.Contains(typeName)) {
                    data.CharacterSlotTypes.Add(typeName);
                }

                LPCCharacterTypeConfiguration typeConfig = null;
                LoadAsset(
                    ref typeConfig,
                    $"Assets/Generated/Models/{typeName}_{definition.name}.asset");

                var def = JObject.Parse(sheetDefinition.text);
                var layers = def.Properties().Where(
                    p => p.Name.ToLower().StartsWith("layer"))
                .ToList();

                typeConfig.Name = definition.name;
                typeConfig.Type = typeName;

                typeConfig.Layers.Clear();
                foreach (var layer in layers) {
                    var layerObject = JObject.Parse(layer.Value.ToString());
                    var zPosition = (int)layerObject.Properties().Single(p => p.Name == "zPos");
                    var bodyTypes = layerObject.Properties()
                        .Where(p => p.Name != "zPos")
                        .ToDictionary(p => p.Name, p => p.Value);

                    var characterTypeLaper = new LPCCharacterTypeLayer {
                        zPosition = zPosition
                    };

                    foreach (var bodyType in bodyTypes) {
                        var raceVariantSpriteMap = new LPCCharacterTypeVariantSpriteMap();
                        foreach (var variant in definition.variants) {
                            var spriteFileName = variant.ToString().Replace(" ", "_");
                            var spriteAssetPath = $"Assets/LPC/spritesheets/{bodyType.Value}{spriteFileName}.png";
                            Debug.Log($"Looking for sprite at {spriteAssetPath}");
                            raceVariantSpriteMap.VariantSpriteList.Add(new KeyValuePair<string, Texture2D>() {
                                Key = variant,
                                Value = (Texture2D)AssetDatabase.LoadAssetAtPath(spriteAssetPath, typeof(Texture2D))
                            });
                        };
                        characterTypeLaper.RaceVariantSpritePathList.Add(new KeyValuePair<string, LPCCharacterTypeVariantSpriteMap>() {
                            Key = bodyType.Key,
                            Value = raceVariantSpriteMap
                        });
                    }

                    typeConfig.Layers.Add(new KeyValuePair<string, LPCCharacterTypeLayer> {
                        Key = layer.Name,
                        Value = characterTypeLaper
                    });
                }
                _importProgress = definitionsLoaded++ / (float)data.Definitions.Count;
                Debug.Log($"Loading progress: {_importProgress * 100}%");
                //EditorUtility.SetDirty(typeConfig);
                AssetDatabase.SaveAssets();
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        } catch (Exception ex) {
            Debug.LogError(ex.ToString());
        }

        _isImporting = false;
        EditorUtility.ClearProgressBar();
        yield return null;
    }

    private void LoadDefinitions() {
        Debug.Log("Loading LPC animations.");

        if (data.CharacterSlotTypes.Count > 0) {
            Debug.LogError("Existing LPC animations must be unloaded first.");
            return;
        }
        TMP_EditorCoroutine.StartCoroutine(ParseDefinitions());
    }
}
