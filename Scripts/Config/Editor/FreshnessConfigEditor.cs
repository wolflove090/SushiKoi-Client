using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FreshnessConfig))]
public class FreshnessConfigEditor : Editor
{
    SerializedProperty _DefaultValue;
    SerializedProperty _CustomValue;

    void OnEnable() 
    {
        this._DefaultValue = serializedObject.FindProperty("DefaultDecValue");
        this._CustomValue = serializedObject.FindProperty("CustomDecValue");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        Color buttonColor = GUI.skin.button.normal.textColor;

        // ========== 通常の減算値
        EditorGUILayout.LabelField("通常の減算値");
        EditorGUILayout.PropertyField(this._DefaultValue);

        EditorGUILayout.Space();

        // ========== カスタム減算値
        EditorGUILayout.LabelField("カスタム減算値");
        EditorGUILayout.HelpBox("月ごとに鮮度の減算値を設定できます。", MessageType.Info);

        for(int i = 0; i < this._CustomValue.arraySize; i++)
        {
            var customValue = this._CustomValue.GetArrayElementAtIndex(i);
            var month = customValue.FindPropertyRelative("Month");
            var decValue = customValue.FindPropertyRelative("DecValue");

            EditorGUILayout.PropertyField(month);
            EditorGUILayout.PropertyField(decValue);

            GUI.skin.button.normal.textColor = Color.red;
            if(GUILayout.Button("項目削除"))
            {
                this._CustomValue.DeleteArrayElementAtIndex(i);
            }
            GUI.skin.button.normal.textColor = buttonColor;
        }

        GUI.skin.button.normal.textColor = Color.green;

        if(GUILayout.Button("項目追加"))
        {
            this._CustomValue.InsertArrayElementAtIndex(this._CustomValue.arraySize);
        }
        GUI.skin.button.normal.textColor = buttonColor;

        serializedObject.ApplyModifiedProperties();
    }

}
