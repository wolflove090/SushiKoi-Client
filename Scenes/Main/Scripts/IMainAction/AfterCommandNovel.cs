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
    MagrouEpisodeNovelSchema[] _MagrouNovelMaster;

    public AfterCommandNovel()
    {
        // ノベルマスタの取得
        this._NovelMaster = MasterUtil.LoadAll<AfterCommandNovelData>("MasterData/after_command_novel");
        this._MagrouNovelMaster = MasterUtil.LoadAll<MagrouEpisodeNovelSchema>("MasterData/magrou_episode_novel");
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

        for(int index = 0; index < this._MagrouNovelMaster.Length; index++)
        {
            var novel = this._MagrouNovelMaster[index];

            // 対象の進行度ではなかった場合
            int epiNum = index + 1;
            if(epiNum != target.ProgressEpiNum + 1)
                continue;

            // 解放日を超えていない場合
            bool isOverMonth = date.Month >= novel.Month;
            bool isOverWeek = date.Month > novel.Month || (date.Month == novel.Month && date.Week >= novel.Week);
            if(!isOverMonth && !isOverWeek)
                    continue;

            // 好感度判定
            if(target.Likability.Value < novel.Likability)
                continue;

            // 学力判定
            if(player.Edu.Value < novel.Edu)
                continue;

            // 運動力判定
            if(player.Str.Value < novel.Str)
                continue;

            if(player.RicePower.Value < novel.RicePower)
                continue;

            return novel.NovelName;
        }

        return "";
    }
}