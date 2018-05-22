using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AtlasManager))]
public class AtlasManagerEditor : Editor
{
    private const string PathToCharacterTextures = "/Sprites/Standard/Character/";
    private const string PathToEquipmentTextures = "/Sprites/Standard/Equipment/";

    private AtlasManager _am;
    private bool _dirty;

    public override void OnInspectorGUI()
    {
        _am = target as AtlasManager;
        serializedObject.Update();

        _dirty = false;

        if (_am?.SpriteList == null)
        {
            _am = _am ?? new AtlasManager();
            _am.SpriteList = new List<Sprite>();
            _dirty = true;
        }

        if (GUILayout.Button("Load"))
        {
            UnloadSprites();

            var characterSprites = Directory.GetFiles(Application.dataPath + PathToCharacterTextures, "*", SearchOption.AllDirectories);
            var equipmentSprites = Directory.GetFiles(Application.dataPath + PathToEquipmentTextures, "*", SearchOption.AllDirectories);

            LoadFiles(characterSprites);
            LoadFiles(equipmentSprites);

            // update the model-list text file
            using (var outputFile = new StreamWriter("model-list.txt"))
            {
                foreach (var model in _am.ModelList)
                {
                    outputFile.WriteLine(model);
                }
            }
        }

        if (GUILayout.Button("Unload")) UnloadSprites();

        if (_dirty) EditorUtility.SetDirty(_am.gameObject);

        DrawDefaultInspector();
        serializedObject.ApplyModifiedProperties();
    }

    private void LoadFiles(IEnumerable<string> files)
    {
        foreach (var file in files)
        {
            var filepath = file.Replace("\\", "/");

            if (!filepath.EndsWith(".png")) continue;

            filepath = filepath.Replace(Application.dataPath, "");
            UpdateModelList(filepath);

            // Load all sprites and add them to the Atlas Manager's sprite list
            // These will be available at runtime thanks to the data being serialized.
            var items = AssetDatabase.LoadAllAssetsAtPath("Assets" + filepath);
            foreach (var o in items)
            {
                var s = o as Sprite;
                if (s == null) continue;

                Debug.Log(s.name);
                _am.SpriteList.Add(s);
                _dirty = true;
            }
        }
    }

    private void UpdateModelList(string filepath)
    {
        // Add the spritesheet (model) to the model list 

        var pathBranch = filepath.Split('/');
        var prefix = "";
        for (var i = 4; i < pathBranch.Length - 1; i++)
        {
            var node = pathBranch[i];
            var splitNode = node.Split('.');

            prefix += $"{splitNode[0]}_";
        }

        prefix += $"{pathBranch[pathBranch.Length - 1]}";
        prefix = prefix.Replace(".png", "");
        _am.ModelList.Add(prefix);
        _am.ModelsTotal++;
    }

    private void UnloadSprites()
    {
        _am.ModelList.Clear();
        _am.SpriteList.Clear();
        _am.ModelsLoaded = 0;
        _am.ModelsTotal = 0;
    }
}
