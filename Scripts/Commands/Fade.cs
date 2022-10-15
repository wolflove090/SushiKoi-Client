using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;
using Naninovel.UI;

[CommandAlias("fadeOut")]
public class FadeOut : Command
{
    public override async UniTask ExecuteAsync (AsyncToken asyncToken = default)
    {
        Debug.Log("暗転");

        var blackoutUI = Engine.GetService<IUIManager>().GetUI<IFadeUI>();
        await blackoutUI.Show(true);
    }
}

[CommandAlias("fadeIn")]
public class FadeIn : Command
{
    public override async UniTask ExecuteAsync (AsyncToken asyncToken = default)
    {
        Debug.Log("暗転");

        var blackoutUI = Engine.GetService<IUIManager>().GetUI<IFadeUI>();
        await blackoutUI.Show(false);
        blackoutUI.Hide();
    }
}