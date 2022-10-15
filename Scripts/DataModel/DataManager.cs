using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager
{
    static DataManager _Singleton;

    DateData _Date = new DateData();
    ClubData _Club = new ClubData();
    PlayerCharaData _Player = new PlayerCharaData();
    TargetCharaData[] _TargetCharas = new TargetCharaData[]
    {
        new TargetCharaData(),
    };

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

        // DataManagerが持っているDataModelのプロパティのセーブを実行
        var type = typeof(DataManager);
        var fields = type.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        foreach(var field in fields)
        {
            var fieldType = field.FieldType;
            if(fieldType.GetInterfaces().Contains(typeof(IDataModel)))
            {
                // セーブ実行
                var value = field.GetValue(_Singleton);
                var dataModel = value as IDataModel;
                dataModel.Save();
            }
        }
    }

    // --------------------
    // リセット
    // --------------------
    public static void Reset()
    {
        Debug.Log("データのリセット");

        // DataManagerが持っているDataModelのプロパティのリセットを実行
        var type = typeof(DataManager);
        var fields = type.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        foreach(var field in fields)
        {
            var fieldType = field.FieldType;
            if(fieldType.GetInterfaces().Contains(typeof(IDataModel)))
            {
                // リセット実行
                var value = field.GetValue(_Singleton);
                var dataModel = value as IDataModel;
                dataModel.Reset();
            }
        }    
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

        // TODO 一旦一人だけ
        return _Singleton._TargetCharas.First();
    }
}
