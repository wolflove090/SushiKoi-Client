using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// --------------------
// メインフローアクション用インターフェース
// --------------------
public interface IMainAction
{
    void Play(System.Action onComplete);
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

        int dec = 2;
        // 8月と12月は減少量が多くなる
        if(date.Month == 8 || date.Month == 12)
        {
            dec = 10;
        }

        player.Freshness.Add(dec * -1);
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
