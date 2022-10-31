using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------------------
// 攻略対象のキャラデータ
// --------------------
public class TargetCharaData : IDataModel
{
    // データ保存用クラス
    public class TargetSaveData
    {
        public int LikabilityValue;
        public int ProgressEpiNum;
    }

    public IParam Likability = new Likability(); // 好感度
    public int ProgressEpiNum = 0; // 進行エピソード


    // --------------------
    // Jsonに変換
    // --------------------
    SaveData IDataModel.SetSaveData(SaveData saveData)
    {
        // セーブデータに値を挿入
        var data = new TargetSaveData()
        {
            LikabilityValue = this.Likability.Value,
            ProgressEpiNum = this.ProgressEpiNum,
        };
        saveData.TargetJson = JsonUtility.ToJson(data);

        return saveData;
    }

    // --------------------
    // ロード
    // --------------------
    void IDataModel.Load(SaveData saveData)
    {
        var data = JsonUtility.FromJson<TargetCharaData.TargetSaveData>(saveData.TargetJson);

        // データがなければ抜ける
        if(data == null)
            return;


        this.Likability.LoadData(saveData);
        this.ProgressEpiNum = data.ProgressEpiNum;
    }
}

// --------------------
// 好感度
// --------------------
public class Likability : IParam
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
        var data = JsonUtility.FromJson<TargetCharaData.TargetSaveData>(saveData.TargetJson);

        // データがなければ抜ける
        if(data == null)
            return;

        this._Value = data.LikabilityValue;
    }
}