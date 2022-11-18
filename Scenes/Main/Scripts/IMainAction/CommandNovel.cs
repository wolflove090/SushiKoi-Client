using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;
using UnityEngine.UI;

// --------------------
// コマンド選択ADV
// --------------------
public class CommandNovel : IMainAction
{
    // 精鋭したコマンドリスト
    List<GameObject> _CommandObjList = new List<GameObject>();

    ButtonBase _CommandButton;
    Transform _CommandButtonRoot;

    CommandActionController _CommandAction;

    public CommandNovel(ButtonBase commandButton, Transform commandButtonRoot, CommandActionController commandAction)
    {
        this._CommandButton = commandButton;
        this._CommandButtonRoot = commandButtonRoot;
        this._CommandAction = commandAction;

        this._CommandAction.Init();
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
                command.Action(onComplete, this._CommandAction);
            };
            button.Label = command.CommandName;

            // コマンド背景
            var buttonBack = button.transform.Find("Pos/Back");
            var backImage = buttonBack.GetComponent<Image>();
            backImage.sprite = Resources.Load<Sprite>(command.BackPath);

            this._CommandObjList.Add(obj.gameObject);
        }
        this._CommandButton.gameObject.SetActive(false);
    }

    CommandAction[] _GetCommands()
    {
        var date = DataManager.GetDate();

        // 初登校
        if(date.Month == 4 && date.Week == 1)
            return CommandAction.CreateActionsForFirstSchool();
        
        return CommandAction.CreateActions();
    }
}

// --------------------
// 各コマンドの定義作成
// --------------------
public class CommandAction
{
    public string CommandName;
    public string BackPath;
    public System.Action<System.Action, CommandActionController> Action;

    public CommandActionController ActionController;

    // コマンドを生成
    public static CommandAction[] CreateActions()
    {
        var result = new List<CommandAction>();


        // 休む
        result.Add(new CommandAction()
        {
            CommandName = "休む",
            BackPath = "Images/CommandButton/btn_cmd_rest_off",
            Action = (onComplete, actionController) => 
            {
                var player = DataManager.GetPlayerChara();

                // 設定値を加算
                var config = ConfigManager.GetCommandConfig();
                foreach(var add in config.Rest.AddValue)
                {
                    player.AddParam(add.TargetType, add.Value);
                }

                // アクション演出の実行  
                actionController.ExternalStart(new CommandActionLinker()
                {
                    ActionName = "休む",
                    IsClear = false,
                    OnComplete = onComplete,
                });
            },
        });

        // 勉強
        result.Add(new CommandAction()
        {
            CommandName = "勉強",
            BackPath = "Images/CommandButton/btn_cmd_study_off",
            Action = (onComplete,actionController) => 
            {
                var config = ConfigManager.GetCommandConfig();
                var command = config.Study;

                // 成功判断
                int sucessRate = command.SuccessRate;
                bool isSucess = Random.Range(1, 101) <= sucessRate;

                var player = DataManager.GetPlayerChara();
                player.Hp.Add(-command.NeedHp);

                // 成功した時だけ上昇
                if(isSucess)
                {
                    // 設定値を加算
                    foreach(var add in command.AddValue)
                    {
                        player.AddParam(add.TargetType, add.Value);
                    }
                }

                // アクション演出の実行  
                actionController.ExternalStart(new CommandActionLinker()
                {
                    ActionName = "勉強",
                    IsClear = isSucess,
                    OnComplete = onComplete,
                });
            },
        });

        // 部活
        result.Add(new CommandAction()
        {
            CommandName = "部活",
            BackPath = "Images/CommandButton/btn_cmd_activity_off",
            Action = (onComplete, actionController) => 
            {
                var config = ConfigManager.GetCommandConfig();
                var command = config.Club;

                // 成功判断
                int sucessRate = command.SuccessRate;
                bool isSucess = Random.Range(1, 101) <= sucessRate;

                var player = DataManager.GetPlayerChara();
                player.Hp.Add(-command.NeedHp);

                // 成功した時だけ上昇
                if(isSucess)
                {
                    // 設定値を加算
                    foreach(var add in command.AddValue)
                    {
                        player.AddParam(add.TargetType, add.Value);
                    }
                }

                // アクション演出の実行  
                actionController.ExternalStart(new CommandActionLinker()
                {
                    ActionName = "部活動",
                    IsClear = isSucess,
                    OnComplete = onComplete,
                });
            },
        });

        // バイト
        result.Add(new CommandAction()
        {
            CommandName = "バイト",
            BackPath = "Images/CommandButton/btn_cmd_job_off",
            Action = (onComplete, actionController) => 
            {
                var config = ConfigManager.GetCommandConfig();
                var command = config.Job;

                // 成功判断
                int sucessRate = command.SuccessRate;
                bool isSucess = Random.Range(1, 101) <= sucessRate;

                var player = DataManager.GetPlayerChara();
                player.Hp.Add(- command.NeedHp);

                // 成功した時だけ上昇
                if(isSucess)
                {
                    // 設定値を加算
                    foreach(var add in command.AddValue)
                    {
                        player.AddParam(add.TargetType, add.Value);
                    }
                }

                // アクション演出の実行  
                actionController.ExternalStart(new CommandActionLinker()
                {
                    ActionName = "アルバイト",
                    IsClear = isSucess,
                    OnComplete = onComplete,
                });
            },            

        });

        // デート
        result.Add(new CommandAction()
        {
            CommandName = "デート",
            BackPath = "Images/CommandButton/btn_cmd_date_off",
            Action = (onComplete, actionController) => 
            {
                // 成功判断
                int sucessRate = 100;
                bool isSucess = Random.Range(1, 101) <= sucessRate;

                // 成功した時だけ上昇
                if(isSucess)
                {
                    var hoge = new DataSetForLikability()
                    {
                        Amount = 10,
                    };
                    hoge.ExecuteAsync().Forget();
                }   

                // アクション演出の実行  
                actionController.ExternalStart(new CommandActionLinker()
                {
                    ActionName = "デート",
                    IsClear = isSucess,
                    OnComplete = onComplete,
                });
            },            

        });

        return result.ToArray();
    }

    // 初登校コマンドを生成
    public static CommandAction[] CreateActionsForFirstSchool()
    {
        var result = new List<CommandAction>();

        // 登校
        result.Add(new CommandAction()
        {
            CommandName = "登校",
            BackPath = "Images/CommandButton/btn_cmd_rest_off",
            Action = (onComplete, actionController) => NovelUtil.StartNovel("FirstSchool", onComplete),
        });

        return result.ToArray();
    }
}