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

public class LPCAnimationManagerEditorWindow : EditorWindow {

    private static List<string> _propertiesToShow = new();

    private static LPCGlobalAnimationConfiguration data;

    private const string _assetPath = "Assets/LPC/GlobalAnimationConfiguration.asset";

    Vector2 scrollPosition = Vector2.zero;

    // This method will be called on load or recompile
    [InitializeOnLoadMethod]
    private static void OnLoad() {
        // if no data exists yet create and reference a new instance
        if (!data) {
            // as first option check if maybe there is an instance already
            data = AssetDatabase.LoadAssetAtPath<LPCGlobalAnimationConfiguration>(_assetPath);

            // if that was successful we are done
            if (data)
                return;

            // otherwise create and reference a new instance
            data = CreateInstance<LPCGlobalAnimationConfiguration>();

            AssetDatabase.CreateAsset(data, _assetPath);
            AssetDatabase.Refresh();
        }
    }

    [MenuItem("Window/Liberated Pixel Cup/Animations")]
    public static void ShowWindow() {
        GetWindow(typeof(LPCAnimationManagerEditorWindow)).Show();
    }

    private void ShowPropertyInWindow(SerializedObject dataObject, string name) {
        SerializedProperty stringsProperty = dataObject.FindProperty(name);

        EditorGUILayout.PropertyField(stringsProperty, true);
        dataObject.ApplyModifiedProperties();
    }

    private void RenderProperties() {
        if (_propertiesToShow.Count == 0) {
            var objectFields = typeof(LPCGlobalAnimationConfiguration).GetFields(BindingFlags.Instance | BindingFlags.Public);
            for (int i = 0; i < objectFields.Length; i++) {
                if (Attribute.GetCustomAttribute(objectFields[i], typeof(ShowInWindowAttribute)) != null)
                    _propertiesToShow.Add(objectFields[i].Name);
            }
        }

        var dataObject = new SerializedObject(data);
        foreach (var propertyName in _propertiesToShow)
            ShowPropertyInWindow(dataObject, propertyName);
    }

    private void OnGUI() {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        GUILayout.Label($"Loaded animations: {data.Definitions.Count}");

        RenderProperties();

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
