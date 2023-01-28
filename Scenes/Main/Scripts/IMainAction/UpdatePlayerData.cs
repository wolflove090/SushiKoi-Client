using UnityEngine;

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