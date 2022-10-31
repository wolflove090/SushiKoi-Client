using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager
{
    const string KEY = "SaveData";
    const int SAVE_NUM = 1; // 複数セーブ実装用に一旦数字を指定

    static string SaveKey
    {
        get
        {
            return $"{KEY}_{SAVE_NUM}";
        }
    }

    static DataManager _Singleton;

    DateData _Date = new DateData();
    ClubData _Club = new ClubData();
    PlayerCharaData _Player = new PlayerCharaData();
    TargetCharaData _Magrou = new TargetCharaData();

    // --------------------
    // 初期化処理
    // --------------------
    public static void Init()
    {
        if(_Singleton != null)
            return;

        _Singleton = new DataManager();
    }

    // --------------------
    // データセーブ
    // --------------------
    public static void Save()
    {
        Debug.Log("セーブ");

        var saveData = new SaveData();

        // DataManagerが持っているDataModelのプロパティのセーブを実行
        var type = typeof(DataManager);
        var fields = type.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        foreach(var field in fields)
        {
            var fieldType = field.FieldType;
            if(fieldType.GetInterfaces().Contains(typeof(IDataModel)))
            {
                // セーブデータ挿入
                var value = field.GetValue(_Singleton);
                var dataModel = value as IDataModel;
                saveData = dataModel.SetSaveData(saveData);
            }
        }

        // セーブ実行
        var json = JsonUtility.ToJson(saveData);
        Debug.Log($"{json}");
        PlayerPrefs.SetString(SaveKey, json);
    }

    // --------------------
    // データセーブ
    // jsonを受け取って上書きセーブ
    // --------------------
    public static void SaveEdit(string json)
    {
        // セーブ実行
        Debug.Log($"{json}");
        PlayerPrefs.SetString(SaveKey, json);
    }

    // --------------------
    // データロード
    // --------------------
    public static void Load()
    {
        Debug.Log("ロード");

        bool exisData = PlayerPrefs.HasKey(SaveKey);
        if(!exisData)
        {
            Debug.LogWarning("セーブデータが存在しません");
            return;
        }
        string json = PlayerPrefs.GetString(SaveKey);
        var saveData = JsonUtility.FromJson<SaveData>(json);

        // DataManagerが持っているDataModelのプロパティのセーブを実行
        var type = typeof(DataManager);
        var fields = type.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        foreach(var field in fields)
        {
            var fieldType = field.FieldType;
            if(fieldType.GetInterfaces().Contains(typeof(IDataModel)))
            {
                // ロード実行
                var value = field.GetValue(_Singleton);
                var dataModel = value as IDataModel;
                dataModel.Load(saveData);
            }
        }
    }

    // --------------------
    // ロードデータ取得
    // --------------------
    public static SaveData GetSaveData()
    {
        bool exisData = PlayerPrefs.HasKey(SaveKey);
        if(!exisData)
        {
            Debug.LogWarning("セーブデータが存在しません");
            return new SaveData();
        }
        string json = PlayerPrefs.GetString(SaveKey);
        var saveData = JsonUtility.FromJson<SaveData>(json);

        return saveData;
    }

    // --------------------
    // リセット
    // --------------------
    public static void Reset()
    {
        Debug.Log("データのリセット");
        PlayerPrefs.DeleteKey(SaveKey);
    }

    // --------------------
    // 日付データ取得
    // --------------------
    public static DateData GetDate()
    {
        if(_Singleton == null)
            throw new System.Exception("シングルトン未作成");

        return _Singleton._Date;
    }

    // --------------------
    // 部活データ取得
    // --------------------
    public static ClubData GetClub()
    {
        if(_Singleton == null)
            throw new System.Exception("シングルトン未作成");

        return _Singleton._Club;
    }

    // --------------------
    // プレイヤーキャラデータ取得
    // --------------------
    public static PlayerCharaData GetPlayerChara()
    {
        if(_Singleton == null)
            throw new System.Exception("シングルトン未作成");

        return _Singleton._Player;
    }

    // --------------------
    // 攻略対象キャラデータ取得
    // --------------------
    public static TargetCharaData GetTargetCharaData()
    {
        if(_Singleton == null)
            throw new System.Exception("シングルトン未作成");

        // TODO 一旦まぐろうのみだけ
        return _Singleton._Magrou;
    }
}

// --------------------
// セーブ用データクラス
// --------------------
public class SaveData
{
    public string DateJson;
    public string ClubJson;
    public string PlayerJson;
    public string TargetJson; // 今はまぐろうのみ。今後は別々のデータとして保存する
}