using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------------------
// 新鮮度の回復ADV
// --------------------
public class ChoiceMakeUp : IMainAction
{
    void IMainAction.Play(System.Action onComplete)
    {
        var date = DataManager.GetDate();
        bool showChoice = date.Month != 4; // 4月は出さない
        showChoice &= date.Week == 1; // 月初に出す

        // 選択肢を出さない時は抜ける
        if(!showChoice)
        {
            onComplete();
            return;
        }

        string novelName = "ChoiceMakeUp";
        NovelUtil.StartNovel(novelName, () => {
            onComplete();
        });
    }
}
