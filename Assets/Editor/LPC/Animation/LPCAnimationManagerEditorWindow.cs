using Assets.Editor.Funkhouse;
using Assets.Scripts.Animation;
using Assets.Scripts.Editor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TMPro.EditorUtilities;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

public class LPCAnimationManagerEditorWindow : FunkyEditorWindow {

    private static LPCGlobalAnimationConfiguration data;
    private float _loadingProgress;
    private bool _isLoading;
    private bool _loadRacesIntoMemory = true;

    private const string _assetPath = "Assets/Generated/GlobalAnimationConfiguration.asset";

    private Vector2 _verticalScrollPosition = Vector2.zero;
    private string _lpcMetadataFolder = "Assets/LPC/sheet_definitions/";

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

    private void OnGUI() {
        if (_isLoading)
            EditorUtility.DisplayProgressBar("Simple Progress Bar", "Building LPC animations", _loadingProgress);

        _verticalScrollPosition = GUILayout.BeginScrollView(_verticalScrollPosition);

        GUILayout.BeginHorizontal();
        _lpcMetadataFolder = GUILayout.TextField(_lpcMetadataFolder);
        if (GUILayout.Button("Import")) {
            data.Definitions.Clear();

            Debug.Log($"PATH: {Application.dataPath + _lpcMetadataFolder}");

            var applicationPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Assets"));
            string[] metadataFiles = Directory.GetFiles(
                applicationPath + _lpcMetadataFolder,
                "*.json",
                SearchOption.AllDirectories);

            foreach (var metadataFile in metadataFiles) {
                //if (metadataFile.GetType() == typeof(TextAsset))
                Debug.Log($"attempting to load asset at {metadataFile}");
                var asset = (TextAsset)AssetDatabase.LoadAssetAtPath(
                    metadataFile.Replace(applicationPath, ""), typeof(TextAsset));
                data.Definitions.Add(asset);
            }
        }
        GUILayout.EndHorizontal();

        _loadRacesIntoMemory = GUILayout.Toggle(_loadRacesIntoMemory, "Load races into memory.");

        GUILayout.Label($"Loaded animations: {data.Definitions.Count}");

        RenderProperties(data);

        if (data.CharacterSlotTypes.Count == 0) {
            if (GUILayout.Button("Load"))
                LoadAnimations();
        } else {
            if (GUILayout.Button("Unload"))
                UnloadAnimations();
        }

        GUILayout.EndScrollView();
    }

    private void UnloadAnimations() {
        Debug.Log("Unloading LPC animations.");
        data.CharacterSlotTypes.Clear();
        data.Races.Clear();
    }

    private IEnumerator ParseAnimations() {
        _isLoading = true;
        EditorUtility.DisplayProgressBar("Simple Progress Bar", "Building LPC animations", _loadingProgress);

        var definitionsLoaded = 0;
        foreach (var sheetDefinition in data.Definitions) {
            dynamic definition = JsonConvert.DeserializeObject(sheetDefinition.text);

            string typeName = definition.type_name;
            if (!data.CharacterSlotTypes.Contains(typeName)) {
                data.CharacterSlotTypes.Add(typeName);
            }

            LPCCharacterTypeConfiguration typeConfig = null;
            LoadAsset(
                ref typeConfig,
                $"Assets/Generated/{typeName}_{definition.name}.asset");
            data.Races.Add(typeConfig);

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
            _loadingProgress = definitionsLoaded++ / (float)data.Definitions.Count;
            Debug.Log($"Loading progress: {_loadingProgress * 100}%");
            EditorUtility.UnloadUnusedAssetsImmediate();
            AssetDatabase.Refresh();
            yield return new WaitForSeconds(1f);
        }
        _isLoading = false;
        EditorUtility.ClearProgressBar();
        yield return null;
    }

    private void LoadAnimations() {
        Debug.Log("Loading LPC animations.");

        if (data.CharacterSlotTypes.Count > 0) {
            Debug.LogError("Existing LPC animations must be unloaded first.");
            return;
        }
        TMP_EditorCoroutine.StartCoroutine(ParseAnimations());
    }
}
