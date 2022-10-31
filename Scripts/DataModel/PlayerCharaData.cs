using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// --------------------
// プレイヤーキャラデータ
// --------------------
public class PlayerCharaData : IDataModel
{
    // データセーブ用クラス
    public class PlayerSaveData
    {
        public int HpValue;
        public int EduValue;
        public int StrValue;
        public int RicePowerValue;
        public int MoneyValue;
        public int FreshnessValue;
    }

    public IParam Hp = new Hp(); // 体力,ストレス
    public IParam Edu = new Edu(); // 学力
    public IParam Str = new Str(); // 筋力,運動
    public IParam RicePower = new RicePower(); // シャリ力

    public IParam Money = new Money(); // お金

    public IParam Freshness = new Freshness(); // 鮮度


    // --------------------
    // Jsonに変換
    // --------------------
    SaveData IDataModel.SetSaveData(SaveData saveData)
    {
        // セーブデータに値を挿入
        // TODO リフレクションで実行できるように、パラメータをクラスとして持ちたい
        var data = new PlayerSaveData()
        {
            HpValue = this.Hp.Value,
            EduValue = this.Edu.Value,
            StrValue = this.Str.Value,
            RicePowerValue = this.RicePower.Value,
            MoneyValue = this.Money.Value,
            FreshnessValue = this.Freshness.Value,
            
        };
        saveData.PlayerJson = JsonUtility.ToJson(data);

        return saveData;
    }

    // --------------------
    // ロード
    // --------------------
    void IDataModel.Load(SaveData saveData)
    {
        // リフレクションで各パラメータデータをロード
        var type = typeof(PlayerCharaData);
        var fields = type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        foreach(var field in fields)
        {
            var fieldType = field.FieldType;
            Debug.Log(fieldType);
            if(fieldType == typeof(IParam))
            {
                Debug.Log("ロード実行");
                // パラメータのロード
                var value = field.GetValue(this);
                var param = value as IParam;
                param.LoadData(saveData);
            }
        }
    }
}

// --------------------
// パラメータ
// --------------------
public interface IParam
{
    int Value{get;}
    void Set(int value);
    void Add(int amount);
    void LoadData(SaveData saveData);
}

// TODO 継承を検討
// --------------------
// 体力、ストレス
// --------------------
public class Hp : IParam
{
    public int Value
    {
        get
        {
            return this._Value;
        }
    }
    int _Value = 100;

    void IParam.Set(int value)
    {
        this._Value = value;
    }

    // 加算
    void IParam.Add(int amount)
    {
        this._Value += amount;
    }

    // データロード
    void IParam.LoadData(SaveData saveData)
    {
        var data = JsonUtility.FromJson<PlayerCharaData.PlayerSaveData>(saveData.PlayerJson);

        // データがなければ抜ける
        if(data == null)
            return;

        this._Value = data.HpValue;
    }
}

// --------------------
// 学力
// --------------------
public class Edu : IParam
{
    public int Value
    {
        get
        {
            return this._Value;
        }
    }
    int _Value = 0;

    void IParam.Set(int value)
    {
        this._Value = value;
    }

    // 加算
    void IParam.Add(int amount)
    {
        this._Value += amount;
    }

    // データロード
    void IParam.LoadData(SaveData saveData)
    {
        var data = JsonUtility.FromJson<PlayerCharaData.PlayerSaveData>(saveData.PlayerJson);

        // データがなければ抜ける
        if(data == null)
            return;

        this._Value = data.EduValue;
    }
}

// --------------------
// 筋力、運動
// --------------------
public class Str : IParam
{
    public int Value
    {
        get
        {
            return this._Value;
        }
    }
    int _Value = 0;

    void IParam.Set(int value)
    {
        this._Value = value;
    }

    // 加算
    void IParam.Add(int amount)
    {
        this._Value += amount;
    }

    // データロード
    void IParam.LoadData(SaveData saveData)
    {
        var data = JsonUtility.FromJson<PlayerCharaData.PlayerSaveData>(saveData.PlayerJson);

        // データがなければ抜ける
        if(data == null)
            return;

        this._Value = data.StrValue;
    }
}

// --------------------
// シャリ力
// --------------------
public class RicePower : IParam
{
    public int Value
    {
        get
        {
            return this._Value;
        }
    }
    int _Value = 0;

    void IParam.Set(int value)
    {
        this._Value = value;
    }

    // 加算
    void IParam.Add(int amount)
    {
        this._Value += amount;
    }

    // データロード
    void IParam.LoadData(SaveData saveData)
    {
        var data = JsonUtility.FromJson<PlayerCharaData.PlayerSaveData>(saveData.PlayerJson);

        // データがなければ抜ける
        if(data == null)
            return;

        this._Value = data.RicePowerValue;
    }
}

// --------------------
// お金
// --------------------
public class Money : IParam
{
    public int Value
    {
        get
        {
            return this._Value;
        }
    }
    int _Value = 500;

    void IParam.Set(int value)
    {
        this._Value = value;
    }

    // 加算
    void IParam.Add(int amount)
    {
        this._Value += amount;
    }

    // データロード
    void IParam.LoadData(SaveData saveData)
    {
        var data = JsonUtility.FromJson<PlayerCharaData.PlayerSaveData>(saveData.PlayerJson);

        // データがなければ抜ける
        if(data == null)
            return;

        this._Value = data.MoneyValue;
    }
}


// --------------------
// 鮮度
// --------------------
public class Freshness : IParam
{
    public int Value
    {
        get
        {
            return this._Value;
        }
    }
    int _Value = 100;

    void IParam.Set(int value)
    {
        this._Value = value;
    }

    // 加算
    void IParam.Add(int amount)
    {
        this._Value += amount;
    }

    // データロード
    void IParam.LoadData(SaveData saveData)
    {
        var data = JsonUtility.FromJson<PlayerCharaData.PlayerSaveData>(saveData.PlayerJson);

        // データがなければ抜ける
        if(data == null)
            return;

        this._Value = data.FreshnessValue;
    }
}