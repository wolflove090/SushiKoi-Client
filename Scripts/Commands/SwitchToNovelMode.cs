using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;

[CommandAlias("toNovel")]
public class SwitchToNovelMode : Command
{
    public StringParameter ScriptName;
    public StringParameter Label;

    public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        if(!Command.Assigned(this.ScriptName))
            throw new System.Exception("スクリプト名を指定してください");

        // カメラの有効化
        Engine.GetService<ICameraManager>().Camera.enabled = true;
        Engine.GetService<ICameraManager>().Camera.clearFlags = CameraClearFlags.Depth;
        Engine.RootObject.gameObject.active = true;

        // Naninovel入力の有効化
        var inputManager = Engine.GetService<IInputManager>();
        inputManager.ProcessInput = true;

        // スクリプトの開始
        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        scriptPlayer.PreloadAndPlayAsync(this.ScriptName, label:this.Label).Forget();
    }
}
