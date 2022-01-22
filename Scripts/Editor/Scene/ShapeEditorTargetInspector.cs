﻿#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace AeternumGames.ShapeEditor
{
    /// <summary>
    /// Represents a mesh target that, when selected, can receive meshes created by the 2D Shape Editor.
    /// </summary>
    [CustomEditor(typeof(ShapeEditorTarget))]
    public class ShapeEditorTargetInspector : Editor
    {
        private SerializedProperty spTargetMode => serializedObject.FindProperty(nameof(ShapeEditorTarget.targetMode));
        private SerializedProperty spFixedExtrudeDistance => serializedObject.FindProperty(nameof(ShapeEditorTarget.fixedExtrudeDistance));
        private SerializedProperty spSplineExtrudePrecision => serializedObject.FindProperty(nameof(ShapeEditorTarget.splineExtrudePrecision));
        private SerializedProperty spRevolveExtrudePrecision => serializedObject.FindProperty(nameof(ShapeEditorTarget.revolveExtrudePrecision));
        private SerializedProperty spRevolveExtrudeDegrees => serializedObject.FindProperty(nameof(ShapeEditorTarget.revolveExtrudeDegrees));
        private SerializedProperty spRevolveExtrudeRadius => serializedObject.FindProperty(nameof(ShapeEditorTarget.revolveExtrudeRadius));

        public override void OnInspectorGUI()
        {
            ShapeEditorMenu_OnGUI();

            var shapeEditorTarget = (ShapeEditorTarget)target;

            bool rebuild = GUILayout.Button("Rebuild");

            EditorGUILayout.PropertyField(spTargetMode);

            switch ((ShapeEditorTargetMode)spTargetMode.enumValueIndex)
            {
                case ShapeEditorTargetMode.Polygon:
                    Polygon_OnGUI();
                    break;

                case ShapeEditorTargetMode.FixedExtrude:
                    FixedExtrude_OnGUI();
                    break;

                case ShapeEditorTargetMode.SplineExtrude:
                    SplineExtrude_OnGUI();
                    break;

                case ShapeEditorTargetMode.RevolveExtrude:
                    RevolveExtrude_OnGUI();
                    break;
            }

            if (serializedObject.ApplyModifiedProperties() || rebuild)
            {
                shapeEditorTarget.Rebuild();
            }
        }

        private void Polygon_OnGUI()
        {
        }

        private void FixedExtrude_OnGUI()
        {
            EditorGUILayout.PropertyField(spFixedExtrudeDistance);
        }

        private void SplineExtrude_OnGUI()
        {
            EditorGUILayout.PropertyField(spSplineExtrudePrecision);
        }

        private void RevolveExtrude_OnGUI()
        {
            EditorGUILayout.PropertyField(spRevolveExtrudePrecision);
            EditorGUILayout.PropertyField(spRevolveExtrudeDegrees);
            EditorGUILayout.PropertyField(spRevolveExtrudeRadius);
        }

        private void ShapeEditorMenu_OnGUI()
        {
            var shapeEditorTarget = (ShapeEditorTarget)target;

            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUIStyle createBrushStyle = ShapeEditorResources.toolbarButtonStyle;

            if (GUILayout.Button(new GUIContent(" Shape Editor", ShapeEditorResources.Instance.shapeEditorIcon, "Show 2D Shape Editor"), createBrushStyle))
            {
                // display the 2d shape editor.
                ShapeEditorWindow.Init();
            }

            if (GUILayout.Button(new GUIContent(" Restore Project", ShapeEditorResources.Instance.shapeEditorRestore, "Load Embedded Project Into 2D Shape Editor"), createBrushStyle))
            {
                // display the 2d shape editor.
                ShapeEditorWindow window = ShapeEditorWindow.InitAndGetHandle();
                // load a copy of the embedded project into the editor.
                window.OpenProject(shapeEditorTarget.project.Clone());
            }
            GUILayout.EndHorizontal();
        }
    }
}

#endif