using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AutoPlayWindow : EditorWindow
{
    [MenuItem("すし恋/AutoPlayWindow")]
    static void _ShowWindow()
    {
        var window = EditorWindow.GetWindow<AutoPlayWindow>();
        window.maxSize = new Vector2(300,500);
        window.Show();
    }

    DateData _Date = new DateData();

    void OnGUI()
    {
        // --------------------
        // 日付
        // --------------------
        using (new EditorGUILayout.VerticalScope("box"))
        {
            EditorGUILayout.LabelField("日付設定");

            using(new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("月", GUILayout.Width(20));
                var month = EditorGUILayout.IntField(this._Date.Month);
                this._Date.SetMonth(month);

                EditorGUILayout.Space();
                
                EditorGUILayout.LabelField("週", GUILayout.Width(20));
                var week = EditorGUILayout.IntField(this._Date.Week);
                this._Date.SetWeek(week);
            }
        }

        EditorGUILayout.Space();

        if(GUILayout.Button("上記設定でゲーム実行"))
        {
            this.PlayContinue();
        }
    }

    void PlayContinue()
    {
        List<IDataModel> dataModelList = new List<IDataModel>();
        var newData = new SaveData();

        // 日付データの変更
        dataModelList.Add(this._Date);

        // 各データをJsonに変換
        for(int i = 0; i < dataModelList.Count; i++)
        {
            IDataModel dateData = dataModelList[i];
            dateData.SetSaveData(newData);
        }

        // 続きからのデータへ上書き
        string json = JsonUtility.ToJson(newData);
        DataManager.SaveEdit(json);

        // 続きからプレイ
        PlayerPrefs.SetInt(AutoPlay.KEY, 2);
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }

}
