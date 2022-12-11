using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

// --------------------
// メインフローアクション用インターフェース
// --------------------
public interface IMainAction
{
    void Play(System.Action onComplete);
}

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

// --------------------
// プレイヤーデータの定期更新
// --------------------
public class UpdatePlayerData : IMainAction
{
    void IMainAction.Play(System.Action onComplete)
    {
        Debug.Log("プレイヤーデータの定期更新");
        var player = DataManager.GetPlayerChara();
        var date = DataManager.GetDate();
        var config = ConfigManager.GetFreshnessConfig();

        /*
        int dec = config.DefaultDecValue;

        // カスタム変化量があればそっちを適用
        var custom = config.CustomDecValue.FirstOrDefault(value => value.Month == date.Month);
        if(custom != null)
            dec = custom.DecValue;
            */

        // TODO 各コマンドで減らしたいため一旦オミット
        //player.Freshness.Add(dec * -1);
        onComplete();
    }
}

// --------------------
// 好感度更新
// --------------------
public class LikabilityUpdate : IMainAction
{
    GameObject _Status;

    public LikabilityUpdate(GameObject status)
    {
        this._Status = status;
        this._Update();
    }

    void IMainAction.Play(System.Action onComplete)
    {
        Debug.Log("好感度更新");
        this._Update();
        onComplete();
    }

    void _Update()
    {
        var target = DataManager.GetTargetCharaData();

        var label = this._Status.transform.Find("Value").GetComponent<TextMeshProUGUI>();
        label.text = target.Likability.Value.ToString();

        var epiLabel = this._Status.transform.Find("EpiNum").GetComponent<TextMeshProUGUI>();
        epiLabel.text = target.ProgressEpiNum.ToString();
    }
}
