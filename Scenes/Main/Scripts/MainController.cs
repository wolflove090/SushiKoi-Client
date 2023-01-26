using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;

public class MainController : ControllerBase<MainViewModel>
{
    // アクションリスト
    List<IMainAction> _MainActionList = new List<IMainAction>();

    StatusUpdate _StatusUpdate;

    // --------------------
    // _OnStart
    // --------------------
    protected override void _OnStart()
    {
        // メインフローのアクションを設定
        // 設定した順番で繰り返す
        this._MainActionList.Add(new ChoiceMakeUp());
        this._MainActionList.Add(new BeforeCommandNovel());
        this._StatusUpdate = new StatusUpdate(this._ViewModel.StatusContent.transform.Find("Root").gameObject, this._ViewModel.Status);
        this._MainActionList.Add(this._StatusUpdate);
        this._MainActionList.Add(new CommandAction(this._ViewModel.CommandButton, this._ViewModel.ComanndList.transform, this));
        this._MainActionList.Add(new AfterCommandNovel());
        this._MainActionList.Add(new OverTheDateEffect(this._ViewModel.DateLabel, this._ViewModel.DayUpdate));
        this._MainActionList.Add(new UpdatePlayerData());
        this._MainActionList.Add(this._StatusUpdate);
        this._MainActionList.Add(new LikabilityUpdate(this._ViewModel.LikabilityStatus));
        this._MainActionList.Add(new AutoSave());

        this._ViewModel.CommandAction.Init();
        this._ValidTap();

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
    // コマンド演出
    // --------------------
    public void ShowCommandEffect(string name, bool isClear, CommandStruct command, int decFreshness, System.Action onComplete)
    {
        this._InvalidTap();
        
        this._ViewModel.CommandAction.ExternalStart(new CommandActionLinker()
        {
            ActionName = name,
            IsClear = isClear,
            OnComplete = () => 
            {
                System.Action complete = () => 
                {
                    onComplete?.Invoke();
                    this._ValidTap();
                };

                // 増減アニメーション後に完了コールバックを叩く
                this._StatusUpdate.PlayUpdateAnim(complete, command, decFreshness);

            }
        });
    }

    // --------------------
    // タップ無効化
    // --------------------
    void _InvalidTap()
    {
        this._ViewModel.TapBlock.SetActive(true);
    }

    // --------------------
    // タップ有効化
    // --------------------
    void _ValidTap()
    {
        this._ViewModel.TapBlock.SetActive(false);
    }

    // --------------------
    // ログ用
    // --------------------
    static void _Log(string message)
    {
        Debug.Log($"[Main]{message}");
    }
}