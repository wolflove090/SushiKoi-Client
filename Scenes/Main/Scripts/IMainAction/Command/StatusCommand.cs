using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StatusCommand : ICommand
{
    public enum ActivitySpot
    {
        Indoor = 1,
        Outdoor = 2,
    }

    // TODO 動的に変更させたいため、indexをもらって発火時に参照するように変更
    // 毎回ロードするのはエディタ時のみでよい
    CommandStruct _CommandData;
    string _Name;
    string _Path;

    MainController _Main;

    // TODO 最終的にはコンフィグに集約させる
    public StatusCommand(CommandStruct command, MainController main, string name, string path)
    {
        this._CommandData = command;
        this._Name = name;
        this._Path = path;

        this._Main = main;
    }

    string ICommand.Name()
    {
        return this._Name;
    }

    string ICommand.BackPath()
    {
        return this._Path;
    }

    void ICommand.Play(System.Action onComplete)
    {
        // 成功判断
        int sucessRate = this._CommandData.SuccessRate;
        bool isSucess = Random.Range(1, 101) <= sucessRate;

        var player = DataManager.GetPlayerChara();
        player.Hp.Add(- this._CommandData.NeedHp);

        // 新鮮度
        var freshnessConfig = ConfigManager.GetFreshnessConfig();
        var decFreshness = 0;
        var month = DataManager.GetDate().Month;
        var spot = 1;// TODO 一旦固定。コンフィグ化
        foreach(var config in freshnessConfig.CustomDecValue)
        {
            bool isTarget = config.TargetManths.Any(value => value == month);
            if(isTarget)
            {
                switch(spot)
                {
                    // 室内
                    case 1:
                        decFreshness = config.DecValueForIndoors;
                        break;
                    // 室外
                    case 2:
                        decFreshness = config.DecValueForOutdoors;
                        break;                    
                }
            }
        }
        player.Freshness.Add(- decFreshness);

        // 成功した時だけ上昇
        if(isSucess)
        {
            // 設定値を加算
            foreach(var add in this._CommandData.AddValue)
            {
                player.AddParam(add.TargetType, add.Value);
            }
        }

        // アクション演出の実行  
        this._Main.ShowCommandEffect(this._Name, isSucess, this._CommandData, onComplete);
    }
}
