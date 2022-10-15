using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------------------
// コマンド後ADV
// --------------------
public class AfterCommandNovel : IMainAction
{
    void IMainAction.Play(System.Action onComplete)
    {
        string novelName = this._GetAfterNovelName();

        // コマンド後の特別なADVがなければ、エピソードADVの抽選に入る
        if (string.IsNullOrEmpty(novelName))
            novelName = this._GetEpisodeNovelName();


        // 再生するADVがなければ抜ける
        if (string.IsNullOrEmpty(novelName))
        {
            onComplete();
            return;
        }

        NovelUtil.StartNovel(novelName, () =>
        {
            onComplete();
        });
    }

    // コマンド後ADVの取得
    string _GetAfterNovelName()
    {
        var date = DataManager.GetDate();

        switch (date.Month)
        {
            case 4:
                return this._GetAprilNovelName(date.Week);
            default:
                return "";
        }
    }

    // --------------------
    // 4月のADV
    // --------------------
    string _GetAprilNovelName(int week)
    {
        switch (week)
        {
            case 2:
                return "After0402";
            default:
                return "";
        }
    }

    // --------------------
    // 8月のADV
    // --------------------
    string _GetAugustNovelName(int week)
    {
        switch(week)
        {
            case 2:
                return "After0802";
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
            case 4:
                return "After1204";
            default:
                return "";
        }
    }

    // 特定条件で再生するADV
    string _GetEpisodeNovelName()
    {
        var player = DataManager.GetPlayerChara();
        var target = DataManager.GetTargetCharaData();
        var date = DataManager.GetDate();

        // めんどくさいからdicのkeyをシナリオ名とする
        Dictionary<string, IEpisodeConditions> magrouEpis = new Dictionary<string, IEpisodeConditions>();
        magrouEpis.Add("Magrou01", new Magrou1());
        magrouEpis.Add("Magrou02", new Magrou2());
        magrouEpis.Add("Magrou03", new Magrou3());
        magrouEpis.Add("Magrou04", new Magrou4());
        magrouEpis.Add("Magrou05", new Magrou5());

        foreach (var epi in magrouEpis)
        {
            if (epi.Value.IsFulfill())
                return epi.Key;
        }
        return "";
    }
}