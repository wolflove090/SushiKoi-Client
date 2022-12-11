using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// --------------------
// コマンド前ADV
// --------------------
public class BeforeCommandNovel : IMainAction
{
    BeforeCommandNovelData[] _NovelMaster;

    public BeforeCommandNovel()
    {
        // ノベルマスタの取得
        this._NovelMaster = MasterUtil.LoadAll<BeforeCommandNovelData>();
    }

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

        var novel = this._NovelMaster.FirstOrDefault(item => item.Month == date.Month && item.Week == date.Week);
        if(novel == null)
            return "";

        return novel.NovelName;
    }
}
