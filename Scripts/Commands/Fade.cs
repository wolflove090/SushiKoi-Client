using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;
using Naninovel.UI;

[CommandAlias("fadeOut")]
public class FadeOut : Command
{
    public DecimalParameter FadeTime;

    public override async UniTask ExecuteAsync (AsyncToken asyncToken = default)
    {
        Debug.Log("暗転");

        float time = 1f;
        if(Assigned(FadeTime))
        {
            time = FadeTime;
        }

        var blackoutUI = Engine.GetService<IUIManager>().GetUI<IFadeUI>();
        await blackoutUI.Show(true, time);
    }
}

[CommandAlias("fadeIn")]
public class FadeIn : Command
{
    public DecimalParameter FadeTime;

    public override async UniTask ExecuteAsync (AsyncToken asyncToken = default)
    {
        Debug.Log("暗転");

        float time = 1f;
        if(Assigned(FadeTime))
        {
            time = FadeTime;
        }

        var blackoutUI = Engine.GetService<IUIManager>().GetUI<IFadeUI>();
        await blackoutUI.Show(false, time);
        blackoutUI.Hide();
    }
}