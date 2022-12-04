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
        public int FashionableValue;
    }

    public enum ParamType
    {
        Hp = 1,
        Edu = 2,
        Str = 3,
        RicePower = 4,
        Money = 5,
        Freshness = 6,
        Fahionable = 7,
    }

    public IParam Hp = new Hp(); // 体力,ストレス
    public IParam Edu = new Edu(); // 学力
    public IParam Str = new Str(); // 筋力,運動
    public IParam RicePower = new RicePower(); // シャリ力

    public IParam Money = new Money(); // お金

    public IParam Freshness = new Freshness(); // 鮮度
    public IParam Fahinoable = new Fahionable(); // おしゃれ

    // --------------------
    // enumから値の加算
    // --------------------
    public void AddParam(PlayerCharaData.ParamType type, int value)
    {
        switch(type)
        {
            case PlayerCharaData.ParamType.Hp:
            {
                this.Hp.Add(value);
                break;
            }   

            case PlayerCharaData.ParamType.Edu:
            {
                this.Edu.Add(value);
                break;
            }   
                        
            case PlayerCharaData.ParamType.Str:
            {
                this.Str.Add(value);
                break;
            }   
            case PlayerCharaData.ParamType.RicePower:
            {
                this.RicePower.Add(value);
                break;
            }   
            
            case PlayerCharaData.ParamType.Money:
            {
                this.Money.Add(value);
                break;
            }   
            
            case PlayerCharaData.ParamType.Freshness:
            {
                this.Freshness.Add(value);
                break;
            }
            case PlayerCharaData.ParamType.Fahionable:
            {
                this.Fahinoable.Add(value);
                break;
            }      
        }
    }

    // --------------------
    // 増減値プレイヤーにとって有利かどうか
    // --------------------
    public static bool IsPositiveChange(CommandValue changeValue)
    {
        // TODO Switch文は嫌
        bool isAddPositive = false; // 上昇したほうが有利
        switch(changeValue.TargetType)
        {
            case PlayerCharaData.ParamType.Hp:
            case PlayerCharaData.ParamType.Freshness:
            {
                isAddPositive = false;
                break;
            }   

            case PlayerCharaData.ParamType.Edu:
            case PlayerCharaData.ParamType.Str:
            case PlayerCharaData.ParamType.RicePower:
            case PlayerCharaData.ParamType.Money:
            case PlayerCharaData.ParamType.Fahionable:
            {
                isAddPositive = true;
                break;
            }   
        }   

        bool isAddValue = changeValue.Value >= 0;

        return isAddValue == isAddPositive;     
    }


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
            FashionableValue = this.Fahinoable.Value,
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

public class PlayerParameter
{
    protected StatusStruct _Config;
}

// --------------------
// 体力、ストレス
// --------------------
public class Hp : PlayerParameter, IParam
{
    public int Value
    {
        get
        {
            return this._Value;
        }
    }
    int _Value = 100;

    public Hp()
    {
        var statusConfig = ConfigManager.GetStatusConfig();
        if(statusConfig == null)
            return;

        this._Config = statusConfig.Hp;
        this._Value = this._Config.InitValue;
    }

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
public class Edu : PlayerParameter, IParam
{
    public int Value
    {
        get
        {
            return this._Value;
        }
    }
    int _Value = 0;

    public Edu()
    {
        var statusConfig = ConfigManager.GetStatusConfig();
        if(statusConfig == null)
            return;

        this._Config = statusConfig.Edu;

        this._Value = this._Config.InitValue;
    }

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
public class Str : PlayerParameter, IParam
{
    public int Value
    {
        get
        {
            return this._Value;
        }
    }
    int _Value = 0;

    public Str()
    {
        var statusConfig = ConfigManager.GetStatusConfig();
        if(statusConfig == null)
            return;

        this._Config = statusConfig.Str;

        this._Value = this._Config.InitValue;
    }

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
public class RicePower : PlayerParameter, IParam
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
public class Money : PlayerParameter, IParam
{
    public int Value
    {
        get
        {
            return this._Value;
        }
    }
    int _Value = 500;

    public Money()
    {
        var statusConfig = ConfigManager.GetStatusConfig();
        if(statusConfig == null)
            return;

        this._Config = statusConfig.Money;

        this._Value = this._Config.InitValue;
    }

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
public class Freshness : PlayerParameter, IParam
{
    public int Value
    {
        get
        {
            return this._Value;
        }
    }
    int _Value = 100;

    public Freshness()
    {
        var statusConfig = ConfigManager.GetStatusConfig();
        if(statusConfig == null)
            return;

        this._Config = statusConfig.Freshness;

        this._Value = this._Config.InitValue;
    }

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

// --------------------
// おしゃれ
// --------------------
public class Fahionable : PlayerParameter, IParam
{
    public int Value
    {
        get
        {
            return this._Value;
        }
    }
    int _Value = 100;

    public Fahionable()
    {
        var statusConfig = ConfigManager.GetStatusConfig();
        if(statusConfig == null)
            return;

        this._Config = statusConfig.Fahionable;
        this._Value = this._Config.InitValue;
    }

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

        this._Value = data.FashionableValue;
    }
}