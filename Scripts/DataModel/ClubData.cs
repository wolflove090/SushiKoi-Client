using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------------------
// 部活情報
// --------------------
public class ClubData : IDataModel
{
    class ClubSaveData
    {
        public int ClubType;
    }

    public enum ClubType
    {
        None = 0, // 無所属
        Swimming = 1, // 水泳
        BasketManager = 2, // バスケマネージャー
        Tea = 3, // 茶道部
        Quiz = 4, // クイズ研究会
        LightClef = 5, // けいおん
    }

    // 所属
    public ClubType Affilication;
    public string AffilicationName
    {
        get
        {
            return this._GetClubName(this.Affilication);
        }
    }

    public ClubData()
    {
        // 初期は未所属
        this.Affilication = ClubType.None;
    }

    // --------------------
    // 部活参加
    // --------------------
    public void Attend(ClubType club)
    {
        this.Affilication = club;
    }

    // --------------------
    // 部活名取得
    // --------------------
    string _GetClubName(ClubType type)
    {
        switch(type)
        {
            case ClubType.None:
                return "未所属";
            case ClubType.Swimming:
                return "水泳部";
            case ClubType.BasketManager:
                return "バスケ部マネージャー";     
            case ClubType.Tea:
                return "茶道部";   
            case ClubType.Quiz:
                return "クイズ研究会";                    
            case ClubType.LightClef:
                return "軽音部";  
            default:
                throw new System.Exception("未定義");
        }
    }

    // --------------------
    // ロード
    // --------------------
    void IDataModel.Load(SaveData saveData)
    {
        var data = JsonUtility.FromJson<ClubSaveData>(saveData.ClubJson);

        // データがなければ抜ける
        if(data == null)
            return;

        this.Affilication = (ClubType)data.ClubType;
    }

    // --------------------
    // Jsonに変換
    // --------------------
    SaveData IDataModel.SetSaveData(SaveData saveData)
    {
        // セーブデータに値を挿入
        var data = new ClubSaveData()
        {
            ClubType = (int)this.Affilication,
        };
        saveData.ClubJson = JsonUtility.ToJson(data);

        return saveData;
    }
}
