using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;
using UnityEngine.UI;
using System.Linq;

// --------------------
// コマンド選択ADV
// --------------------
public class CommandNovel : IMainAction
{
    // 精鋭したコマンドリスト
    List<GameObject> _CommandObjList = new List<GameObject>();

    ButtonBase _CommandButton;
    Transform _CommandButtonRoot;

    CommandDelegateManager _ActionManager;

    public CommandNovel(ButtonBase commandButton, Transform commandButtonRoot, CommandDelegateManager actionManager)
    {
        this._CommandButton = commandButton;
        this._CommandButtonRoot = commandButtonRoot;
        this._ActionManager = actionManager;

        this._ActionManager.CommandActionController.Init();
    }

    void IMainAction.Play(System.Action onComplete)
    {
        // すでに生成済みのコマンドを破棄
        foreach(var obj in this._CommandObjList)
        {
            GameObject.Destroy(obj);
        }
        this._CommandObjList.Clear();

        // コマンド生成
        var commands = this._GetCommands();
        foreach(var command in commands)
        {
            var obj = GameObject.Instantiate(this._CommandButton, Vector3.zero, Quaternion.identity, this._CommandButtonRoot);
            obj.gameObject.SetActive(true);
            var button = obj.GetComponent<ButtonBase>();
            button.OnClick = () => 
            {
                command.Play(onComplete);
            };
            button.Label = command.Name();

            // コマンド背景
            var buttonBack = button.transform.Find("Pos/Back");
            var backImage = buttonBack.GetComponent<Image>();
            backImage.sprite = Resources.Load<Sprite>(command.BackPath());

            this._CommandObjList.Add(obj.gameObject);
        }
        this._CommandButton.gameObject.SetActive(false);    
    }

    ICommand[] _GetCommands()
    {
        var result = new List<ICommand>();
        var config = ConfigManager.GetCommandConfig();

        // 休む
        result.Add(new DefaultCommand(config.Rest, this._ActionManager.StatusUpdate, this._ActionManager.CommandActionController,"休む", "Images/CommandButton/btn_cmd_rest_off"));

        // 勉強
        result.Add(new DefaultCommand(config.Rest, this._ActionManager.StatusUpdate, this._ActionManager.CommandActionController,"勉強", "Images/CommandButton/btn_cmd_study_off"));

        return result.ToArray();
    }
}

// --------------------
// コマンドに使用するアクション定義
// --------------------
public class CommandDelegateManager
{
    // コマンドアニメーション再生用
    public CommandActionController CommandActionController;

    // ステータス更新アニメーション用
    public StatusUpdate StatusUpdate;

    // コマンド処理終わったら叩かれる
    public System.Action OnComplete;

}

// --------------------
// 各コマンドの定義作成
// --------------------
public class CommandAction
{
    public string CommandName;
    public string BackPath;
    public int Spot; // 室内 = 1, 室外 = 2 TODO：Define化させる
    public System.Action<CommandDelegateManager> Action;

    public CommandActionController ActionController;

    // 初登校コマンドを生成
    public static CommandAction[] CreateActionsForFirstSchool()
    {
        var result = new List<CommandAction>();

        // 登校
        result.Add(new CommandAction()
        {
            CommandName = "登校",
            BackPath = "Images/CommandButton/btn_cmd_rest_off",
            Action = (actionManager) => NovelUtil.StartNovel("FirstSchool", actionManager.OnComplete),
        });

        return result.ToArray();
    }
}