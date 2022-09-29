using Assets.Editor.Funkhouse;
using Assets.Scripts.Animation;
using Assets.Scripts.Editor;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

public class LPCAnimationManagerEditorWindow : FunkyEditorWindow {

    private static LPCGlobalAnimationConfiguration data;

    private const string _assetPath = "Assets/LPC/GlobalAnimationConfiguration.asset";

    Vector2 scrollPosition = Vector2.zero;

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
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        GUILayout.Label($"Loaded animations: {data.Definitions.Count}");

        RenderProperties(data);

        if (GUILayout.Button("Load"))
            LoadAnimations();

        if (GUILayout.Button("Unload"))
            UnloadAnimations();

        GUILayout.EndScrollView();
    }

    private void UnloadAnimations() {
        Debug.Log("Unloading LPC animations.");
        data.Types.Clear();
    }

    private void LoadAnimations() {
        Debug.Log("Loading LPC animations.");

        if (data.Types.Count > 0) {
            Debug.LogError("Existing LPC animations must be unloaded first.");
            return;
        }

        foreach (var sheetDefinition in data.Definitions) {
            dynamic definition = JsonConvert.DeserializeObject(sheetDefinition.text);
            
            string typeName = definition.type_name;
            if (!data.Types.Contains(typeName)) {
                data.Types.Add(typeName);
            }

            foreach (string variant in definition.variants)
                data.Types.Add(variant);
        }
    }
}
