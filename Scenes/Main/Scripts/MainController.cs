using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;

public class MainController : ControllerBase<MainViewModel>
{
    // アクションリスト
    List<IMainAction> _MainActionList = new List<IMainAction>();

    // --------------------
    // _OnStart
    // --------------------
    protected override void _OnStart()
    {
        // ノベルモード切り替え
        //var toGameCommand = new SwitchToGameMode();
        //toGameCommand.ExecuteAsync().Forget();

        // データマネージャー初期化
        //DataManager.Init();

        // メインフローのアクションを設定
        // 設定した順番で繰り返す
        this._MainActionList.Add(new BeforeCommandNovel());
        this._MainActionList.Add(new CommandNovel(this._ViewModel.CommandButton, this._ViewModel.ComanndList.transform, this._ViewModel.CommandAction));
        this._MainActionList.Add(new AfterCommandNovel());
        this._MainActionList.Add(new OverTheDateEffect(this._ViewModel.DateLabel, this._ViewModel.DayUpdate));
        this._MainActionList.Add(new UpdatePlayerData());
        this._MainActionList.Add(new StatusUpdate(this._ViewModel.StatusContent, this._ViewModel.Status));
        this._MainActionList.Add(new LikabilityUpdate(this._ViewModel.LikabilityStatus));
        this._MainActionList.Add(new AutoSave());

        // アクション開始
        this._PlayAction(0);
    }

    // --------------------
    // 各アクションを順番にプレイ
    // --------------------
    void _PlayAction(int index)
    {
        this._MainActionList[index].Play(() => 
        {
            index = (index + 1) % this._MainActionList.Count;
            this._PlayAction(index);
        });
    }

    // --------------------
    // ログ用
    // --------------------
    static void _Log(string message)
    {
        Debug.Log($"[Main]{message}");
    }
}