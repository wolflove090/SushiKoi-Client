using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// --------------------
// コマンド後ADV
// --------------------
public class AfterCommandNovel : IMainAction
{
    AfterCommandNovelData[] _NovelMaster;

    public AfterCommandNovel()
    {
        // ノベルマスタの取得
        this._NovelMaster = MasterUtil.LoadAll<AfterCommandNovelData>("MasterData/after_command_novel");
    }

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

        var novel = this._NovelMaster.FirstOrDefault(item => item.Month == date.Month && item.Week == date.Week);
        if(novel == null)
            return "";

        return novel.NovelName;
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