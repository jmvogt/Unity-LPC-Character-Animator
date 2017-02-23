using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System;

[CustomEditor(typeof(AtlasManager))]
public class AtlasManagerEditor : Editor
{
    const string pathToCharacterTextures = "/Sprites/Character/";
    const string pathToEquipmentTextures = "/Sprites/Equipment/";
    public Dictionary<string, List<Sprite>> SpriteListLookup;

    private AtlasManager am;
    private bool dirty;

    public override void OnInspectorGUI()
    {
        am = target as AtlasManager;
        serializedObject.Update();

        dirty = false;

        // TODO: Reflection plz :(

        if (am.lefthandSprites == null) {
            dirty = true;
            am.lefthandSprites = new List<Sprite>();
        }
        if (am.righthandSprites == null) {
            dirty = true;
            am.righthandSprites = new List<Sprite>();
        }
        if (am.earsSprites == null) {
            dirty = true;
            am.earsSprites = new List<Sprite>();
        }
        if (am.noseSprites == null) {
            dirty = true;
            am.noseSprites = new List<Sprite>();
        }
        if (am.eyesSprites == null) {
            dirty = true;
            am.eyesSprites = new List<Sprite>();
        }
        if (am.bodySprites == null) {
            dirty = true;
            am.bodySprites = new List<Sprite>();
        }
        if (am.facialhairSprites == null) {
            dirty = true;
            am.facialhairSprites = new List<Sprite>();
        }
        if (am.hairSprites == null) {
            dirty = true;
            am.hairSprites = new List<Sprite>();
        }
        if (am.armsSprites == null) {
            dirty = true;
            am.armsSprites = new List<Sprite>();
        }
        if (am.backSprites == null) {
            dirty = true;
            am.backSprites = new List<Sprite>();
        }
        if (am.back2Sprites == null) {
            dirty = true;
            am.back2Sprites = new List<Sprite>();
        }
        if (am.chestSprites == null) {
            dirty = true;
            am.chestSprites = new List<Sprite>();
        }
        if (am.feetSprites == null) {
            dirty = true;
            am.feetSprites = new List<Sprite>();
        }
        if (am.headSprites == null) {
            dirty = true;
            am.headSprites = new List<Sprite>();
        }
        if (am.handsSprites == null) {
            dirty = true;
            am.handsSprites = new List<Sprite>();
        }
        if (am.legsSprites == null) {
            dirty = true;
            am.legsSprites = new List<Sprite>();
        }
        if (am.neckSprites == null) {
            dirty = true;
            am.neckSprites = new List<Sprite>();
        }
        if (am.shoulderSprites == null) {
            dirty = true;
            am.shoulderSprites = new List<Sprite>();
        }
        if (am.waistSprites == null) {
            dirty = true;
            am.waistSprites = new List<Sprite>();
        }
        if (am.wristsSprites == null) {
            dirty = true;
            am.wristsSprites = new List<Sprite>();
        }

        if (GUILayout.Button("Load"))
        {
            am.ModelsLoaded = 0;
            am.ModelsTotal = 0;
            am.modelList.Clear();
            string[] characterSprites = Directory.GetFiles(Application.dataPath + pathToCharacterTextures, "*", SearchOption.AllDirectories);
            string[] equipmentSprites = Directory.GetFiles(Application.dataPath + pathToEquipmentTextures, "*", SearchOption.AllDirectories);

            LoadFiles(characterSprites);
            LoadFiles(equipmentSprites);
        }

        if (GUILayout.Button("Unload")) {
            // TODO: Reflection plz :(
            am.bodySprites = new List<Sprite>();
            am.facialhairSprites = new List<Sprite>();
            am.hairSprites = new List<Sprite>();
            am.armsSprites = new List<Sprite>();
            am.backSprites = new List<Sprite>();
            am.back2Sprites = new List<Sprite>();
            am.chestSprites = new List<Sprite>();
            am.feetSprites = new List<Sprite>();
            am.headSprites = new List<Sprite>();
            am.handsSprites = new List<Sprite>();
            am.legsSprites = new List<Sprite>();
            am.neckSprites = new List<Sprite>();
            am.shoulderSprites = new List<Sprite>();
            am.waistSprites = new List<Sprite>();
            am.wristsSprites = new List<Sprite>();
            am.lefthandSprites = new List<Sprite>();
            am.righthandSprites = new List<Sprite>();
            am.eyesSprites = new List<Sprite>();
            am.noseSprites = new List<Sprite>();
            am.earsSprites = new List<Sprite>();

            am.ModelsLoaded = 0;
            am.ModelsTotal = 0;
            am.modelList.Clear();
        }

        if (dirty)
        {
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
            
            dirty = true;
            var items = AssetDatabase.LoadAllAssetsAtPath("Assets" + filepath);

            var collectionKey = filepath.Split('/')[3];

            List<Sprite> collection;

            // TODO: Reflection plz :(
            if (collectionKey == "body") {
                collection = am.bodySprites;
            } else if (collectionKey == "facialhair") {
                collection = am.facialhairSprites;
            } else if (collectionKey == "hair") {
                collection = am.hairSprites;
            } else if (collectionKey == "arms") {
                collection = am.armsSprites;
            } else if (collectionKey == "back") {
                collection = am.backSprites;
            } else if (collectionKey == "back2") {
                collection = am.back2Sprites;
            } else if (collectionKey == "chest") {
                collection = am.chestSprites;
            } else if (collectionKey == "feet") {
                collection = am.feetSprites;
            } else if (collectionKey == "head") {
                collection = am.headSprites;
            } else if (collectionKey == "hands") {
                collection = am.handsSprites;
            } else if (collectionKey == "legs") {
                collection = am.legsSprites;
            } else if (collectionKey == "neck") {
                collection = am.neckSprites;
            } else if (collectionKey == "shoulder") {
                collection = am.shoulderSprites;
            } else if (collectionKey == "waist") {
                collection = am.waistSprites;
            } else if (collectionKey == "lefthand") {
                collection = am.lefthandSprites;
            } else if (collectionKey == "wrists") {
                collection = am.wristsSprites;
            } else if (collectionKey == "righthand") {
                collection = am.righthandSprites;
            } else if (collectionKey == "ears") {
                collection = am.earsSprites;
            } else if (collectionKey == "eyes") {
                collection = am.eyesSprites;
            } else if (collectionKey == "nose") {
                collection = am.noseSprites;
            } else {
                Debug.Log("New spritefolder detected! Add it to the sprite type list.");
                collection = new List<Sprite>();
                throw new Exception("ERROR: Unknown sprite detected!");
            }

            string[] path_branch = filepath.Split('/');
            string prefix = "";
            for (int i = 3; i < path_branch.Length-1; i++) {
                string node = path_branch[i];
                string[] split_node = node.Split('.');

                prefix += string.Format("{0}_", split_node[0]);
            }
            prefix += string.Format("{0}", path_branch[path_branch.Length-1]);
            prefix = prefix.Replace(".png", "");

            am.modelList.Add(prefix);
            am.ModelsTotal++;

            foreach (object o in items) {
                if (o is Sprite) {
                    Sprite s = o as Sprite;
                    collection.Add(s);
                }
            }
        }
    }
}