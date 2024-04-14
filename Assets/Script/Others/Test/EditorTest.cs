using System;
using UnityEditor;
using UnityEngine;

namespace Script
{
    [CustomEditor(typeof(TestInspector))]
    public class EditorTest : Editor
    {
        public SerializedProperty s_value;
        
        private void OnEnable()
        {
            s_value = serializedObject.FindProperty("value");
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(s_value, new GUIContent("Int Field"), GUILayout.Height(20));
            if (GUILayout.Button("执行函数"))
            {
                Test();
            }
            serializedObject.ApplyModifiedProperties();
        }
        
        public void Test()
        {
            Debug.Log("Execute");
        }
    }
}