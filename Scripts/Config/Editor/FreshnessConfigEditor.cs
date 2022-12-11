using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FreshnessConfig))]
public class FreshnessConfigEditor : Editor
{
    SerializedProperty _CustomValue;

    void OnEnable() 
    {
        this._CustomValue = serializedObject.FindProperty("CustomDecValue");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        Color buttonColor = GUI.skin.button.normal.textColor;

        EditorGUILayout.Space();

        // ========== カスタム減算値
        using (new EditorGUILayout.VerticalScope("box"))
        {
            EditorGUILayout.LabelField("カスタム減算値");
            //EditorGUILayout.HelpBox("月ごとに鮮度の減算値を設定できます。", MessageType.Info);

            for(int i = 0; i < this._CustomValue.arraySize; i++)
            {
                using (new EditorGUILayout.VerticalScope("box"))
                {
                    EditorGUILayout.LabelField($"{i + 1}つめ");
                    var customValue = this._CustomValue.GetArrayElementAtIndex(i);

                    var targetMonths = customValue.FindPropertyRelative("TargetManths");
                    var indoorsValue = customValue.FindPropertyRelative("DecValueForIndoors");
                    var outdoorsValue = customValue.FindPropertyRelative("DecValueForOutdoors");

                    // 対象月の表示
                    for(int c = 0; c < targetMonths.arraySize; c++)
                    {
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            var month = targetMonths.GetArrayElementAtIndex(c);
                            month.intValue = EditorGUILayout.IntField("対象月", month.intValue);

                            GUI.skin.button.normal.textColor = Color.red;
                            if(GUILayout.Button("項目削除", GUILayout.Width(100)))
                            {
                                targetMonths.DeleteArrayElementAtIndex(c);
                            }
                            GUI.skin.button.normal.textColor = buttonColor;
                        }
                    }
                    // 削除ボタン
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.Space();

                        GUI.skin.button.normal.textColor = Color.green;
                        if(GUILayout.Button("対象月追加", GUILayout.Width(100)))
                        {
                            targetMonths.InsertArrayElementAtIndex(targetMonths.arraySize);
                        }
                        GUI.skin.button.normal.textColor = buttonColor;
                    }

                    EditorGUILayout.LabelField("室内で減少する値");
                    EditorGUILayout.PropertyField(indoorsValue);
                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("室外で減少する値");
                    EditorGUILayout.PropertyField(outdoorsValue);

                    GUI.skin.button.normal.textColor = Color.red;
                    if(GUILayout.Button("項目削除"))
                    {
                        this._CustomValue.DeleteArrayElementAtIndex(i);
                    }
                    GUI.skin.button.normal.textColor = buttonColor;

                    EditorGUILayout.Space();
                }

            }
        }


        GUI.skin.button.normal.textColor = Color.green;
        if(GUILayout.Button("項目追加"))
        {
            this._CustomValue.InsertArrayElementAtIndex(this._CustomValue.arraySize);
        }
        GUI.skin.button.normal.textColor = buttonColor;

        // ========== ホットリロードボタン
        if(EditorApplication.isPlaying)
        {
            if(GUILayout.Button("ゲームに設定を反映"))
            {
                ConfigManager.LoadFreshnessConfig();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

}
