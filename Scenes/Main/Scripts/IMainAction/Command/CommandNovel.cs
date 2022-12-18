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

    MainController _Main;

    public CommandNovel(ButtonBase commandButton, Transform commandButtonRoot, MainController main)
    {
        this._CommandButton = commandButton;
        this._CommandButtonRoot = commandButtonRoot;
        this._Main = main;
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
            backImage.sprite = Resources.Load<Sprite>(command.IconPath());

            this._CommandObjList.Add(obj.gameObject);
        }
        this._CommandButton.gameObject.SetActive(false);    
    }

    // --------------------
    // コマンド生成
    // --------------------
    ICommand[] _GetCommands()
    {
        var date = DataManager.GetDate();

        // 初登校
        if(date.Month == 4 && date.Week == 1)
            return this._CreateCommandsForFirstSchool();
        
        return this._CreateCommandsForStatus();

    }

    // --------------------
    // ステータスコマンド
    // --------------------
    ICommand[] _CreateCommandsForStatus()
    {
        var result = new List<ICommand>();
        var config = ConfigManager.GetCommandConfig();

        for(int i = 0; i < config.Commands.Length; i++)
        {
            var model = new StatusCommand(i, this._Main);

            result.Add(model);
        }
        return result.ToArray();
    }

    // --------------------
    // 初登校コマンド
    // --------------------
    ICommand[] _CreateCommandsForFirstSchool()
    {
        var result = new List<ICommand>();
        result.Add(new NovelCommand("登校","Images/CommandButton/btn_cmd_rest_off", "FirstSchool"));

        return result.ToArray();
    }
}