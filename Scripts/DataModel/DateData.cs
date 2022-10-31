using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// --------------------
// 日付情報保持用クラス
// --------------------
public class DateData : IDataModel
{
    // データ保存用クラス
    class DateSaveData
    {
        public int Month;
        public int Week;
    }

    public int Week
    {
        set
        {
            this._Week = value;

            // 月更新
            if(this._Week >= 5)
            {
                this.Month += 1;
                this._Week = 1;
            } 
            
            if(this._Week <= 0)
            {
                this._Month -= 1;
                this._Week = 1;
            }
        }
        get
        {
            return this._Week;
        }
    }
    int _Week;

    public int Month
    {
        set
        {
            this._Month = value;

            // 年更新
            if(this._Month >= 13)
            {
                this._Month = 1;
            }
        }
        get
        {
            return this._Month;
        }
    }
    int _Month;

    public string DateLabel
    {
        get
        {
            return $"{this.Month}月{this.Week}週";
        }
    }

    public DateData()
    {
        // 4月1週から開始
        this.Month = 4;
        this.Week = 1;
    }

    // --------------------
    // 一週経過
    // --------------------
    public void Add()
    {
        this.Week += 1;
    }

    // --------------------
    // 一週戻す
    // --------------------
    public void Rewind()
    {
        this.Week -= 1;
    }

    // --------------------
    // 週設定
    // --------------------
    public void SetWeek(int week)
    {
        this.Week = week;
    }

    // --------------------
    // 月設定
    // --------------------
    public void SetMonth(int month)
    {
        this.Month = month;
    }

    // --------------------
    // ロード
    // --------------------
    void IDataModel.Load(SaveData saveData)
    {
        var data = JsonUtility.FromJson<DateSaveData>(saveData.DateJson);

        // データがなければ初期値を設定
        if(data == null)
        {
            // 4月1週から開始
            this.Month = 4;
            this.Week = 1;
            return;
        }

        this.Month = data.Month;
        this.Week = data.Week;
    }

    // --------------------
    // Jsonに変換
    // --------------------
    SaveData IDataModel.SetSaveData(SaveData saveData)
    {
        // セーブデータに値を挿入
        var data = new DateSaveData()
        {
            Month = this.Month,
            Week = this.Week,
        };
        saveData.DateJson = JsonUtility.ToJson(data);

        return saveData;
    }
}
