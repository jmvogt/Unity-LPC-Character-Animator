using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System;
using Assets.Scripts.Types;

[CustomEditor(typeof(AtlasManager))]
public class AtlasManagerEditor : Editor
{
    const string pathToCharacterTextures = "/Sprites/Character/";
    const string pathToEquipmentTextures = "/Sprites/Equipment/";

    private AtlasManager am;
    private bool dirty;

    public override void OnInspectorGUI()
    {
        am = target as AtlasManager;
        serializedObject.Update();

        dirty = false;

        if (am.spriteList == null) {
            am.spriteList = new List<Sprite>();
            dirty = true;
        }
        
        if (GUILayout.Button("Load"))
        {
            UnloadSprites();
            
            string[] characterSprites = Directory.GetFiles(Application.dataPath + pathToCharacterTextures, "*", SearchOption.AllDirectories);
            string[] equipmentSprites = Directory.GetFiles(Application.dataPath + pathToEquipmentTextures, "*", SearchOption.AllDirectories);

            LoadFiles(characterSprites);
            LoadFiles(equipmentSprites);

            // update the model-list text file
            using (StreamWriter outputFile = new StreamWriter("model-list.txt")) {
                foreach (string model in am.modelList)
                    outputFile.WriteLine(model);
            }

        }

        if (GUILayout.Button("Unload")) {
            UnloadSprites();
        }

        if (dirty) {
            EditorUtility.SetDirty(am.gameObject);
        }

        DrawDefaultInspector();
        serializedObject.ApplyModifiedProperties();
    }

    private void LoadFiles(string[] files) {
        foreach (string file in files) {
            string filepath = file.Replace("\\", "/");

            if (!filepath.EndsWith(".png")) {
                continue;
            }

            filepath = filepath.Replace(Application.dataPath, "");
            UpdateModelList(filepath);

            // Load all sprites and add them to the Atlas Manager's sprite list
            // These will be available at runtime thanks to the data being serialized.
            var items = AssetDatabase.LoadAllAssetsAtPath("Assets" + filepath);
            foreach (object o in items) {
                if (o is Sprite) {
                    Sprite s = o as Sprite;
                    am.spriteList.Add(s);
                    dirty = true;
                }
            }
        }
    }

    private void UpdateModelList(string filepath) {
        /*
         * Add the spritesheet (model) to the model list 
         */
        string[] path_branch = filepath.Split('/');
        string prefix = "";
        for (int i = 3; i < path_branch.Length - 1; i++) {
            string node = path_branch[i];
            string[] split_node = node.Split('.');

            prefix += string.Format("{0}_", split_node[0]);
        }
        prefix += string.Format("{0}", path_branch[path_branch.Length - 1]);
        prefix = prefix.Replace(".png", "");
        am.modelList.Add(prefix);
        am.ModelsTotal++;
    }

    private void UnloadSprites() {
        am.modelList.Clear();
        am.spriteList.Clear();
        am.ModelsLoaded = 0;
        am.ModelsTotal = 0;
    }
}