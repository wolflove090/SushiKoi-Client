using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;
using Naninovel.UI;

[CommandAlias("sepia")]
public class ScreenEffectSepia : Command
{
    public override UniTask ExecuteAsync (AsyncToken asyncToken = default)
    {
        var screenEffect = Engine.GetService<IUIManager>().GetUI<IScreenEffectUI>();

        screenEffect.ShowSepia();

        return UniTask.CompletedTask;
    }
}

[CommandAlias("screenRefresh")]
public class ScreenEffectRefresh : Command
{
    public override UniTask ExecuteAsync (AsyncToken asyncToken = default)
    {
        var screenEffect = Engine.GetService<IUIManager>().GetUI<IScreenEffectUI>();

        screenEffect.Refresh();

        return UniTask.CompletedTask;
    }
}
