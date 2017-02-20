using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

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

        if (am.tileSprites == null)
        {
            dirty = true;
            am.tileSprites = new List<Sprite>();
        }

        Debug.Log("IM HERE LOL");
        if (GUILayout.Button("Load"))
        {
            var files = Directory.GetFiles(Application.dataPath + pathToCharacterTextures, "*", SearchOption.AllDirectories);
            am.tileSprites = new List<Sprite>();
            LoadFiles(files);

            files = Directory.GetFiles(Application.dataPath + pathToEquipmentTextures, "*", SearchOption.AllDirectories);
            LoadFiles(files);

            Debug.Log("sprites fount : " + am.tileSprites.Count);
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

            //List<string> mustEndWith = new List<string>();
            //mustEndWith.Add("body/female/light.png");
            //mustEndWith.Add("body/male/light.png");
            //mustEndWith.Add("body/male/orc.png");
            //mustEndWith.Add("white_sleeveless.png");
            //mustEndWith.Add("platemail.png");
            //mustEndWith.Add("spear_male.png");
            //mustEndWith.Add("tightdress_white.png");
            //mustEndWith.Add("wings.png");
            //mustEndWith.Add("spikes.png");

            //bool skipFile = true;
            //foreach (string endsWith in mustEndWith) {
            //    if (filepath.EndsWith(endsWith)) {
            //        skipFile = false;
            //    }
            //}
            //if (skipFile)
            //    continue;

            if (!filepath.EndsWith(".png")) {
                continue;
            }

            filepath = filepath.Replace(Application.dataPath, "");
            Debug.Log("LOADING ASSET FILE: " + filepath);

            dirty = true;
            var items = AssetDatabase.LoadAllAssetsAtPath("Assets" + filepath);
            Debug.Log("items count: " + items.Length);

            foreach (object o in items) {
                Debug.Log("ADDING OBJECT! ");
                if (o is Sprite) {
                    Sprite s = o as Sprite;
                    am.tileSprites.Add(s);
                }
            }
        }
    }
}