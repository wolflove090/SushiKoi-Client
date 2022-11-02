using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// --------------------
// 続きからプレイ
// --------------------
public class ContinueStart : EditorWindow
{
    [MenuItem("すし恋/ContinueStart")]
    static void _ShowWindow()
    {
        var window = EditorWindow.GetWindow<ContinueStart>();
        window.maxSize = new Vector2(300,500);
        window.Show();
    }

    DateData _Date;
    PlayerCharaData _Player;
    TargetCharaData _Target;

    List<IDataModel> _DataModelList
    {
        get
        {
            var list = new List<IDataModel>();
            list.Add(this._Date);
            list.Add(this._Player);
            list.Add(this._Target);

            return list;
        }
    }

    void OnGUI()
    {
        // --------------------
        // セーブデータを読み取る
        // --------------------
        if(this._Date == null)
        {
            var saveData = DataManager.GetSaveData();

            this._Date = new DateData();
            this._Player = new PlayerCharaData();
            this._Target = new TargetCharaData();

            // データのロード
            foreach(var iData in this._DataModelList)
            {
                iData.Load(saveData);
            }
        }

        EditorGUILayout.Space();

        // --------------------
        // リセットボタン
        // --------------------
        using(new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.Space();

            var icon = EditorGUIUtility.IconContent("d_Refresh");
            if(GUILayout.Button(icon, GUILayout.Width(50)))
            {
                this._Date = null;
            }
        }

        EditorGUILayout.Space();

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

        // --------------------
        // ステータス
        // --------------------
        using (new EditorGUILayout.VerticalScope("box"))
        {
            EditorGUILayout.LabelField("ステータス設定");

            using(new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("ストレス", GUILayout.Width(50));
                var value = EditorGUILayout.IntField(this._Player.Hp.Value);
                this._Player.Hp.Set(value);
            }

            EditorGUILayout.Space();
            
            using(new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("学力", GUILayout.Width(50));
                var value = EditorGUILayout.IntField(this._Player.Edu.Value);
                this._Player.Edu.Set(value);
            }

            using(new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("運動", GUILayout.Width(50));
                var value = EditorGUILayout.IntField(this._Player.Str.Value);
                this._Player.Str.Set(value);
            }

            
            using(new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("シャリ力", GUILayout.Width(50));
                var value = EditorGUILayout.IntField(this._Player.RicePower.Value);
                this._Player.RicePower.Set(value);
            }

            using(new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("お金", GUILayout.Width(50));
                var value = EditorGUILayout.IntField(this._Player.Money.Value);
                this._Player.Money.Set(value);
            }

            using(new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("鮮度", GUILayout.Width(50));
                var value = EditorGUILayout.IntField(this._Player.Freshness.Value);
                this._Player.Freshness.Set(value);
            }
        }

        EditorGUILayout.Space();

        // --------------------
        // 攻略対象
        // --------------------
        using (new EditorGUILayout.VerticalScope("box"))
        {
            EditorGUILayout.LabelField("まぐろう");

            using(new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("好感度", GUILayout.Width(100));
                var value = EditorGUILayout.IntField(this._Target.Likability.Value);
                this._Target.Likability.Set(value);
            }

            using(new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("進行エピソード", GUILayout.Width(100));
                var value = EditorGUILayout.IntField(this._Target.ProgressEpiNum);
                this._Target.ProgressEpiNum = value;
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
        foreach(var iData in this._DataModelList)
        {
            iData.SetSaveData(newData);
        }

        // 続きからのデータへ上書き
        string json = JsonUtility.ToJson(newData);
        DataManager.SaveEdit(json);

        // 続きからプレイ
        PlayerPrefs.SetInt(AutoPlay.KEY, 2);
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }
}
