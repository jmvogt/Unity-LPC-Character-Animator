using Assets.Scripts.Editor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Funkhouse {
    public class FunkyEditorWindow : EditorWindow {
        private static readonly List<EditorWindowPropertyDefinition> _propertiesToShow = new();

        protected static void LoadAsset<T>(ref T data, string assetPath) where T : ScriptableObject {
            // if no data exists yet create and reference a new instance
            if (data == null) {
                // as first option check if maybe there is an instance already
                data = AssetDatabase.LoadAssetAtPath<T>(assetPath);

                // if that was successful we are done
                if (data != null)
                    return;

                Directory.CreateDirectory(Path.GetDirectoryName(assetPath));

                // otherwise create and reference a new instance
                data = CreateInstance<T>();

                AssetDatabase.CreateAsset(data, assetPath);
                AssetDatabase.Refresh();
            }
        }

        protected void ShowPropertyInWindow(SerializedObject dataObject, EditorWindowPropertyDefinition definition) {
            bool previousEnableState = GUI.enabled;
            if (definition.ReadOnly)
                GUI.enabled = false;

            SerializedProperty property = dataObject.FindProperty(definition.PropertyName);
            EditorGUILayout.PropertyField(property, true);

            GUI.enabled = previousEnableState;
        }

        protected static void IdentifyProperties<T>() {
            var objectFields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public);
            for (int i = 0; i < objectFields.Length; i++) {
                var attribute = Attribute.GetCustomAttribute(objectFields[i], typeof(ShowInWindowAttribute));
                if (attribute != null) {
                    _propertiesToShow.Add(
                        new EditorWindowPropertyDefinition(
                            objectFields[i].Name,
                            attribute as ShowInWindowAttribute));
                }
            }
        }

        protected void RenderProperties(UnityEngine.Object data, string primaryTab = "", string secondaryTab = "") {
            var dataObject = new SerializedObject(data);
            foreach (var property in _propertiesToShow)
                if (primaryTab == "" || property.PrimaryTab == primaryTab && secondaryTab == "" || property.SecondaryTab == secondaryTab)
                    ShowPropertyInWindow(dataObject, property);
            dataObject.ApplyModifiedProperties();
        }

        protected void DrawGuiLine(int i_height = 1) {

            Rect rect = EditorGUILayout.GetControlRect(false, i_height);

            rect.height = i_height;

            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));

        }

    }
}
