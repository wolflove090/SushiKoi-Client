using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// --------------------
// 日付情報保持用クラス
// --------------------
public class DateData : IDataModel
{
    public string KEY_WEEK = "DateDataWeek";
    public string KEY_MONTH = "DateDataMonth";

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
    // セーブ
    // --------------------
    void IDataModel.Save()
    {
        Debug.Log("セーブする");
        PlayerPrefs.SetInt(KEY_MONTH, this.Month);
        PlayerPrefs.SetInt(KEY_WEEK, this.Week);
    }

    // --------------------
    // ロード
    // --------------------
    void IDataModel.Load()
    {
        Debug.Log("ロードする");
        bool existData = PlayerPrefs.HasKey(KEY_MONTH) && PlayerPrefs.HasKey(KEY_WEEK);

        // データがなければ初期値を設定
        if(!existData)
        {
            // 4月1週から開始
            this.Month = 4;
            this.Week = 1;
            return;
        }

        this.Month = PlayerPrefs.GetInt(KEY_MONTH);
        this.Week = PlayerPrefs.GetInt(KEY_WEEK);
    }


    // --------------------
    // リセット
    // --------------------
    void IDataModel.Reset()
    {
        Debug.Log("リセットする");
        PlayerPrefs.SetInt(KEY_MONTH, 4);
        PlayerPrefs.SetInt(KEY_WEEK, 1);
    }
}
