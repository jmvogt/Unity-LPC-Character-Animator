using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

[CustomEditor(typeof(AtlasManager))]
public class AtlasManagerEditor : Editor
{
    const string pathToTextures = "/Sprites/";

    public override void OnInspectorGUI()
    {
        AtlasManager am = target as AtlasManager;
        serializedObject.Update();

        bool dirty = false;
        if (am.tileSprites == null)
        {
            dirty = true;
            am.tileSprites = new List<Sprite>();
        }

        Debug.Log("IM HERE LOL");
        if (GUILayout.Button("Load"))
        {
            var files = Directory.GetFiles(Application.dataPath + pathToTextures, "*", SearchOption.AllDirectories);
            am.tileSprites = new List<Sprite>();

            foreach (string file in files)
            {
                string filepath = file.Replace("\\", "/");
                
                if (!filepath.EndsWith("body/male/light.png") && !filepath.EndsWith("body/male/orc.png") && !filepath.EndsWith("chest_male.png") && !filepath.EndsWith("spear_male.png")) continue;
                filepath = filepath.Replace(Application.dataPath, "");
                Debug.Log("LOADING ASSET FILE: " + filepath);

                dirty = true;
                var items = AssetDatabase.LoadAllAssetsAtPath("Assets" + filepath);
                Debug.Log("items count: " + items.Length);

                foreach (object o in items)
                {
                    Debug.Log("ADDING OBJECT! ");
                    if (o is Sprite)
                    {
                        Sprite s = o as Sprite;
                        am.tileSprites.Add(s);
                    }
                }
            }
            Debug.Log("sprites fount : " + am.tileSprites.Count);
        }

        if (dirty)
        {
            EditorUtility.SetDirty(am.gameObject);
        }

        DrawDefaultInspector();
        serializedObject.ApplyModifiedProperties();
    }
}