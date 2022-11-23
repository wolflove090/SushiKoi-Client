using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CommandConfig))]
public class CommandDataEditor : Editor
{
    CommandProperty _Rest;
    CommandProperty _Study;
    CommandProperty _Club;
    CommandProperty _Job;

    CommandProperty _GoOut;
    CommandProperty _Esthetic;
    CommandProperty _CharmUp;

    class CommandProperty
    {
        public bool IsShow;
        public SerializedProperty Property;
    }

    void OnEnable() 
    {
        this._Rest = new CommandProperty()
        {
            Property = serializedObject.FindProperty("Rest"),
        };

        this._Study = new CommandProperty()
        {
            Property = serializedObject.FindProperty("Study"),
        };

        this._Club = new CommandProperty()
        {
            Property = serializedObject.FindProperty("Club"),
        };

        this._Job = new CommandProperty()
        {
            Property = serializedObject.FindProperty("Job"),
        };  

        this._GoOut = new CommandProperty()
        {
            Property = serializedObject.FindProperty("GoOut"),
        };

        this._Esthetic = new CommandProperty()
        {
            Property = serializedObject.FindProperty("Esthetic"),
        };

        this._CharmUp = new CommandProperty()
        {
            Property = serializedObject.FindProperty("CharmUp"),
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // ========== 休むコマンド
        this._Rest.IsShow = EditorGUILayout.Foldout(this._Rest.IsShow, "休むコマンド", true);
        if(this._Rest.IsShow)
            this._ShowCommandStruct(this._Rest.Property);

        // ========== 勉強コマンド
        this._Study.IsShow = EditorGUILayout.Foldout(this._Study.IsShow, "勉強コマンド", true);
        if(this._Study.IsShow)
            this._ShowCommandStruct(this._Study.Property);

        // ========== 部活コマンド
        this._Club.IsShow = EditorGUILayout.Foldout(this._Club.IsShow, "部活コマンド", true);
        if(this._Club.IsShow)        
            this._ShowCommandStruct(this._Club.Property);

        // ========== バイトコマンド
        this._Job.IsShow = EditorGUILayout.Foldout(this._Job.IsShow, "バイトコマンド", true);
        if(this._Job.IsShow)  
            this._ShowCommandStruct(this._Job.Property);

        // ========== おでかけコマンド
        this._GoOut.IsShow = EditorGUILayout.Foldout(this._GoOut.IsShow, "おでかけコマンド", true);
        if(this._GoOut.IsShow)  
            this._ShowCommandStruct(this._GoOut.Property);

        // ========== エステコマンド
        this._Esthetic.IsShow = EditorGUILayout.Foldout(this._Esthetic.IsShow, "エステコマンド", true);
        if(this._Esthetic.IsShow)  
            this._ShowCommandStruct(this._Esthetic.Property);

        // ========== 魅力コマンド
        this._CharmUp.IsShow = EditorGUILayout.Foldout(this._CharmUp.IsShow, "魅力コマンド", true);
        if(this._CharmUp.IsShow)  
            this._ShowCommandStruct(this._CharmUp.Property);        

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

                // 初期化
                var newValue = addValues.GetArrayElementAtIndex(addValues.arraySize - 1);
                var type = newValue.FindPropertyRelative("TargetType");
                var value = newValue.FindPropertyRelative("Value");
                type.enumValueIndex = 0;
                value.intValue = 0;
            }
            GUI.skin.button.normal.textColor = buttonColor;
        }
    }
}