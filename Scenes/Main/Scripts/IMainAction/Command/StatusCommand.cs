﻿using System.Collections;
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

    int _CommandIndex;
    CommandStruct _CommandDataValue;
    CommandStruct _CommandData
    {
        get
        {
            // エディタ時は毎回取得(ホットリロード用)
            if(Application.platform == RuntimePlatform.OSXEditor)
            {
                var config = ConfigManager.GetCommandConfig();
                return config.Commands[this._CommandIndex];
            }

            if(this._CommandDataValue == null)
            {
                var config = ConfigManager.GetCommandConfig();
                this._CommandDataValue = config.Commands[this._CommandIndex];
            }
            return this._CommandDataValue;
        }
    }

    MainController _Main;

    public StatusCommand(int index, MainController main)
    {
        this._CommandIndex = index;
        this._Main = main;
    }

    string ICommand.Name()
    {
        return this._CommandData.Name;
    }

    string ICommand.IconPath()
    {
        return this._CommandData.IconPath;
    }

    void ICommand.Play(System.Action onComplete)
    {
        // 成功判断
        int sucessRate = this._CommandData.SuccessRate;
        bool isSucess = Random.Range(1, 101) <= sucessRate;

        var player = DataManager.GetPlayerChara();

        // 新鮮度
        var freshnessConfig = ConfigManager.GetFreshnessConfig();
        var decFreshness = 0;
        var month = DataManager.GetDate().Month;
        foreach(var config in freshnessConfig.CustomDecValue)
        {
            bool isTarget = config.TargetManths.Any(value => value == month);
            if(isTarget)
            {
                switch(this._CommandData.Spot)
                {
                    // 室内
                    case ActivitySpot.Indoor:
                        decFreshness = config.DecValueForIndoors;
                        break;
                    // 室外
                    case ActivitySpot.Outdoor:
                        decFreshness = config.DecValueForOutdoors;
                        break;                    
                }
            }
        }
        // TODO エステだけ新鮮度を変動させない
        // TODO エステは特殊コマンドとして実装する？
        player.Freshness.Add(decFreshness);

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
        this._Main.ShowCommandEffect(this._CommandData.Name, isSucess, this._CommandData, decFreshness, onComplete);
    }
}
