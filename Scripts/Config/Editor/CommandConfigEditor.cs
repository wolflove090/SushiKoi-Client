using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Supyrb;

[CustomEditor(typeof(CommandConfig))]
public class CommandDataEditor : Editor
{
    SerializedProperty _Rest;
    SerializedProperty _Study;
    SerializedProperty _Club;
    SerializedProperty _Job;

    bool _ShowRest = false;
    bool _ShowStudy = false;
    bool _ShowClub= false;
    bool _ShowJob = false;

    void OnEnable() 
    {
        _Rest = serializedObject.FindProperty("Rest");
        _Study = serializedObject.FindProperty("Study");
        _Club = serializedObject.FindProperty("Club");
        _Job = serializedObject.FindProperty("Job");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // ========== 休むコマンド
        this._ShowRest = EditorGUILayout.Foldout(this._ShowRest, "休むコマンド", true);
        if(this._ShowRest)
            this._ShowCommandStruct(this._Rest);

        // ========== 勉強コマンド
        this._ShowStudy = EditorGUILayout.Foldout(this._ShowStudy, "勉強コマンド", true);
        if(this._ShowStudy)
            this._ShowCommandStruct(this._Study);

        // ========== 部活コマンド
        this._ShowClub = EditorGUILayout.Foldout(this._ShowClub, "部活コマンド", true);
        if(this._ShowClub)        
            this._ShowCommandStruct(this._Club);

        // ========== バイトコマンド
        this._ShowJob = EditorGUILayout.Foldout(this._ShowJob, "バイトコマンド", true);
        if(this._ShowJob)  
            this._ShowCommandStruct(this._Job);

        // ========== ホットリロードボタン
        if(EditorApplication.isPlaying)
        {
            if(GUILayout.Button("ゲームに設定を反映"))
            {
                ConfigManager.LoadCommandConfig();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    void _ShowCommandStruct(SerializedProperty commandProperty)
    {
        var needHp = commandProperty.FindPropertyRelative("NeedHp");
        var successRate = commandProperty.FindPropertyRelative("SuccessRate");
        var addValues = commandProperty.FindPropertyRelative("AddValue");
        Color buttonColor = GUI.skin.button.normal.textColor;

        using (new EditorGUILayout.VerticalScope("box"))
        {
            // 必要HP
            EditorGUILayout.LabelField("必要ストレス値");
            EditorGUILayout.PropertyField(needHp);

            EditorGUILayout.Space();

            // 成功率
            EditorGUILayout.LabelField("成功率");
            EditorGUILayout.PropertyField(successRate);

            EditorGUILayout.Space();

            // 加算パラメータ
            EditorGUILayout.LabelField("加算パラメータ");

            // 加算パラメータ設定
            for(int i = 0; i < addValues.arraySize; i++)
            {
                using (new EditorGUILayout.VerticalScope("box"))
                {
                    var addValue = addValues.GetArrayElementAtIndex(i);

                    var type = addValue.FindPropertyRelative("TargetType");
                    var value = addValue.FindPropertyRelative("Value");

                    EditorGUILayout.PropertyField(type);
                    EditorGUILayout.PropertyField(value);
                    EditorGUILayout.Space();

                    using (new EditorGUILayout.HorizontalScope())
                    {
                        GUI.skin.button.normal.textColor = Color.red;
                        if(GUILayout.Button("パラメータ削除"))
                        {
                            addValues.DeleteArrayElementAtIndex(i);
                        }
                        GUI.skin.button.normal.textColor = buttonColor;
                    }
                }
            }

            GUI.skin.button.normal.textColor = Color.green;

            if(GUILayout.Button("パラメータ追加"))
            {
                addValues.InsertArrayElementAtIndex(addValues.arraySize);
            }
            GUI.skin.button.normal.textColor = buttonColor;
        }
    }
}