using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Naninovel;

[CommandAlias("toGame")]
public class SwitchToGameMode : Command
{
    public static UnityEvent CompleteNovel = new UnityEvent();

    public override async UniTask ExecuteAsync (AsyncToken asyncToken = default)
    {
        // ノベルパートの入力を無効化
        var inputManager = Engine.GetService<IInputManager>();
        inputManager.ProcessInput = false;

        // スクリプトプレイヤーを停止
        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        scriptPlayer.Stop();

        // ステートをリセット
        var stateManager = Engine.GetService<IStateManager>();
        await stateManager.ResetStateAsync();

        // カメラ切り替え
        var naniCamera = Engine.GetService<ICameraManager>().Camera;
        naniCamera.enabled = false;

        // ルートを切り替え
        Engine.RootObject.gameObject.SetActive(false);

        // ノベル終了コールバック
        // TODO ノベルから → ゲームを判断できると良い
        CompleteNovel.Invoke();
    }
}