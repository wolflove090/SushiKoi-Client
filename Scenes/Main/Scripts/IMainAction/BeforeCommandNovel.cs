using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------------------
// コマンド前ADV
// --------------------
public class BeforeCommandNovel : IMainAction
{
    void IMainAction.Play(System.Action onComplete)
    {
        string novelName = this._GetNovelName();
        if(string.IsNullOrEmpty(novelName))
        {
            onComplete();
            return;
        }
        
        NovelUtil.StartNovel(novelName, () => {
            onComplete();
        });
    }

    // --------------------
    // コマンド前ADV取得
    // --------------------
    string _GetNovelName()
    {
        var date = DataManager.GetDate();
        
        switch(date.Month)
        {
            case 4:
                return this._GetAprilNovelName(date.Week);
            case 5:
                return this._GetMayNovelName(date.Week);
            case 6:
                return this._GetJuneNovelName(date.Week);
            case 7:
                return this._GetJulyNovelName(date.Week);
            case 8:
                return this._GetAugustNovelName(date.Week);
            case 9:
                return this._GetSeptemberNovelName(date.Week);
            case 10:
                return this._GetOctoberNovelName(date.Week);
            case 11:
                return this._GetNovemberNovelName(date.Week);
            case 12:
                return this._GetDecemberNovelName(date.Week);
            case 1:
                return this._GetJanuaryNovelName(date.Week);
            case 2:
                return this._GetFebruaryNovelName(date.Week);
            case 3:
                return this._GetMarchNovelName(date.Week);

            default:
                return "";
        }
    }

    // --------------------
    // 4月のADV
    // --------------------
    string _GetAprilNovelName(int week)
    {
        switch(week)
        {
            case 1:
                return "Opening";
            case 2:
                return "Before0402";
            case 4:
                return "Before0404";
            default:
                return "";
        }
    }

    // --------------------
    // 5月のADV
    // --------------------
    string _GetMayNovelName(int week)
    {
        switch(week)
        {
            case 2:
                return "Before0502";
            case 4:
                return "Before0504";
            default:
                return "";
        }
    }


    // --------------------
    // 6月のADV
    // --------------------
    string _GetJuneNovelName(int week)
    {
        switch(week)
        {
            case 1:
                return "Before0603";
            default:
                return "";
        }
    }


    // --------------------
    // 7月のADV
    // --------------------
    string _GetJulyNovelName(int week)
    {
        switch(week)
        {
            case 2:
                return "Before0702";
            case 4:
                return "Before0704";
            default:
                return "";
        }
    }


    // --------------------
    // 8月のADV
    // --------------------
    string _GetAugustNovelName(int week)
    {
        return "";

    }


    // --------------------
    // 9月のADV
    // --------------------
    string _GetSeptemberNovelName(int week)
    {
        switch(week)
        {
            case 2:
                return "Before0902";
            default:
                return "";
        }
    }

    // --------------------
    // 10月のADV
    // --------------------
    string _GetOctoberNovelName(int week)
    {
        switch(week)
        {
            case 1:
                return "Before1001";
            case 4:
                return "Before1004";
            default:
                return "";
        }
    }

    // --------------------
    // 11月のADV
    // --------------------
    string _GetNovemberNovelName(int week)
    {
        switch(week)
        {
            case 3:
                return "Before1103";
            default:
                return "";
        }
    }

    // --------------------
    // 12月のADV
    // --------------------
    string _GetDecemberNovelName(int week)
    {
        switch(week)
        {
            case 2:
                return "Before1202";
            case 4:
                return "Before1204";
            default:
                return "";
        }
    }


    // --------------------
    // 1月のADV
    // --------------------
    string _GetJanuaryNovelName(int week)
    {
        switch(week)
        {
            case 1:
                return "Before0101";
            default:
                return "";
        }
    }


    // --------------------
    // 2月のADV
    // --------------------
    string _GetFebruaryNovelName(int week)
    {
        switch(week)
        {
            case 2:
                return "Before0202";
            default:
                return "";
        }
    }


    // --------------------
    // 3月のADV
    // --------------------
    string _GetMarchNovelName(int week)
    {
        switch(week)
        {
            case 2:
                return "Ending";
            default:
                return "";
        }
    }
      
}
