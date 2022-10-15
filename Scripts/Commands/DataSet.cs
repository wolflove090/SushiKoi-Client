using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;

[CommandAlias("setClub")]
public class DataSetForClub : Command
{
    public IntegerParameter ClubType;

    public override async UniTask ExecuteAsync (AsyncToken asyncToken = default)
    {
        Debug.Log("部活情報セット");

        int clubTypeNum = this.ClubType;
        var clubType = (ClubData.ClubType)clubTypeNum;

        // 部活情報の更新
        var club = DataManager.GetClub();
        club.Attend(clubType);


        // 表示用にtmpへ挿入
        var valueManager = Engine.GetService<CustomVariableManager>();
        valueManager.SetVariableValue("tmp", club.AffilicationName);
    }
}

// 好感度変動
[CommandAlias("flucLikability")]
public class DataSetForLikability : Command
{
    public IntegerParameter Amount;

    public override async UniTask ExecuteAsync (AsyncToken asyncToken = default)
    {
        Debug.Log("好感度変動");

        // 対象キャラ
        var target = DataManager.GetTargetCharaData();
        target.Likability.Add(this.Amount); // TODO 一旦加算のみ、Addで減算もできたほうがいいかも

        // 表示用にtmpへ挿入
        var valueManager = Engine.GetService<CustomVariableManager>();
        valueManager.SetVariableValue("tmp", this.Amount.ToString());
    }
}

// エピソード進行
[CommandAlias("adMagrouEpi")]
public class DataSetForMagrouEpi : Command
{
    public override async UniTask ExecuteAsync (AsyncToken asyncToken = default)
    {
        Debug.Log("エピソード進行");

        // 対象キャラ
        var target = DataManager.GetTargetCharaData();
        target.ProgressEpiNum += 1;
        int epiNum = target.ProgressEpiNum;

        // 表示用にtmpへ挿入
        var valueManager = Engine.GetService<CustomVariableManager>();
        valueManager.SetVariableValue("tmp", epiNum.ToString());
    }
}