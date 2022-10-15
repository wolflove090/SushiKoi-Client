using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;

public class SampleController : ControllerBase<SampleViewModel>
{
    // Start
    protected override void _OnStart()
    {
        // this._DisableNovel();
        var toGameCommand = new SwitchToGameMode();
        toGameCommand.ExecuteAsync().Forget();

        // サンプルノベル再生
        this._ViewModel.StartButton.OnClick = () =>
        {
            var toNovelCommand = new SwitchToNovelMode()
            {
                ScriptName = "Test",
            };
            toNovelCommand.ExecuteAsync().Forget();
        };
    }
}
