using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------------------
// エピソード条件判定用インターフェース
// --------------------
public interface IEpisodeConditions
{
    // 条件達成しているか
    bool IsFulfill();
}

// --------------------
// まぐろう エピソード1
// --------------------
public class Magrou1 : IEpisodeConditions
{
    const int _Month = 5;
    const int _Likability = 10;
    const int _Edu = 10;
    const int _Str = 10;
    const int _RicePower = 10;

    bool IEpisodeConditions.IsFulfill()
    {
        var player = DataManager.GetPlayerChara();
        var target = DataManager.GetTargetCharaData();
        var date = DataManager.GetDate();

        // エピソード進行判定
        if (target.ProgressEpiNum != 0)
            return false;

        // 月判定
        if (date.Month < _Month)
            return false;

        // 好感度判定
        if (target.Likability.Value < _Likability)
            return false;

        // 学力判定
        if (player.Edu.Value < _Edu)
            return false;

        // 運動力判定
        if (player.Str.Value < _Str)
            return false;

        // シャリ力判定
        if (player.RicePower.Value < _RicePower)
            return false;

        return true;
    }
}

// --------------------
// まぐろう エピソード2
// --------------------
public class Magrou2 : IEpisodeConditions
{
    const int _Month = 7;
    const int _Likability = 30;
    const int _Edu = 20;
    const int _Str = 20;
    const int _RicePower = 30;

    bool IEpisodeConditions.IsFulfill()
    {
        var player = DataManager.GetPlayerChara();
        var target = DataManager.GetTargetCharaData();
        var date = DataManager.GetDate();

        // エピソード進行判定
        if (target.ProgressEpiNum != 1)
            return false;

        // 月判定
        if (date.Month < _Month)
            return false;

        // 好感度判定
        if (target.Likability.Value < _Likability)
            return false;

        // 学力判定
        if (player.Edu.Value < _Edu)
            return false;

        // 運動力判定
        if (player.Str.Value < _Str)
            return false;

        // シャリ力判定
        if (player.RicePower.Value < _RicePower)
            return false;

        return true;
    }
}

// --------------------
// まぐろう エピソード3
// --------------------
public class Magrou3 : IEpisodeConditions
{
    const int _Month = 9;
    const int _Likability = 60;
    const int _Edu = 40;
    const int _Str = 40;
    const int _RicePower = 50;

    bool IEpisodeConditions.IsFulfill()
    {
        var player = DataManager.GetPlayerChara();
        var target = DataManager.GetTargetCharaData();
        var date = DataManager.GetDate();

        // エピソード進行判定
        if (target.ProgressEpiNum != 2)
            return false;

        // 月判定
        if (date.Month < _Month)
            return false;

        // 好感度判定
        if (target.Likability.Value < _Likability)
            return false;

        // 学力判定
        if (player.Edu.Value < _Edu)
            return false;

        // 運動力判定
        if (player.Str.Value < _Str)
            return false;

        // シャリ力判定
        if (player.RicePower.Value < _RicePower)
            return false;

        return true;
    }
}

// --------------------
// まぐろう エピソード4
// --------------------
public class Magrou4 : IEpisodeConditions
{
    const int _Month = 11;
    const int _Likability = 100;
    const int _Edu = 60;
    const int _Str = 60;
    const int _RicePower = 80;

    bool IEpisodeConditions.IsFulfill()
    {
        var player = DataManager.GetPlayerChara();
        var target = DataManager.GetTargetCharaData();
        var date = DataManager.GetDate();


        // エピソード進行判定
        if (target.ProgressEpiNum != 3)
            return false;

        // 月判定
        if (date.Month < _Month)
            return false;

        // 好感度判定
        if (target.Likability.Value < _Likability)
            return false;

        // 学力判定
        if (player.Edu.Value < _Edu)
            return false;

        // 運動力判定
        if (player.Str.Value < _Str)
            return false;

        // シャリ力判定
        if (player.RicePower.Value < _RicePower)
            return false;

        return true;
    }
}

// --------------------
// まぐろう エピソード5
// --------------------
public class Magrou5 : IEpisodeConditions
{
    // TODO 条件不明
    const int _Month = 11;
    const int _Likability = 100;
    const int _Edu = 60;
    const int _Str = 60;
    const int _RicePower = 80;

    bool IEpisodeConditions.IsFulfill()
    {
        var player = DataManager.GetPlayerChara();
        var target = DataManager.GetTargetCharaData();
        var date = DataManager.GetDate();

        // エピソード進行判定
        if (target.ProgressEpiNum != 4)
            return false;

        // 月判定
        if (date.Month < _Month)
            return false;

        // 好感度判定
        if (target.Likability.Value < _Likability)
            return false;

        // 学力判定
        if (player.Edu.Value < _Edu)
            return false;

        // 運動力判定
        if (player.Str.Value < _Str)
            return false;

        // シャリ力判定
        if (player.RicePower.Value < _RicePower)
            return false;

        return true;
    }
}