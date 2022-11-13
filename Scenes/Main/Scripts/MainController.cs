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
        // メインフローのアクションを設定
        // 設定した順番で繰り返す
        this._MainActionList.Add(new ChoiceMakeUp());
        this._MainActionList.Add(new BeforeCommandNovel());
        var statusUpdate = new StatusUpdate(this._ViewModel.StatusContent, this._ViewModel.Status);
        this._MainActionList.Add(statusUpdate);
        this._MainActionList.Add(new CommandNovel(this._ViewModel.CommandButton, this._ViewModel.ComanndList.transform, this._ViewModel.CommandAction));
        this._MainActionList.Add(new AfterCommandNovel());
        this._MainActionList.Add(new OverTheDateEffect(this._ViewModel.DateLabel, this._ViewModel.DayUpdate));
        this._MainActionList.Add(new UpdatePlayerData());
        this._MainActionList.Add(statusUpdate);
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
    // 選択肢を出す
    // 作ったけど使い所がまだないかも(使う場合は以下関数を調整して)
    // --------------------
    void _ShowChoice()
    {
        var choiceButtons = new ChoiceButton[5];
        for(int i = 0; i < choiceButtons.Length; i++)
        {
            int index = i;
            choiceButtons[index] = new ChoiceButton()
            {
                ButtonLabel = $"ボタン{index + 1}",
                OnClick = () => 
                {
                    Debug.Log($"ボタン{index + 1}");
                }
            };
        }
        this._ViewModel.ChoiceContent.ExternalStart(new ChoiceLinker() 
        {
            ChoiceButtons = choiceButtons,
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