using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationExtension
{
    // コールバック付きでアニメーションを再生
    public static void Play(this Animation anim, string name, System.Action onComplete)
    {
        AnimationManager.Play(anim, name, onComplete);
    }
}

// コルーチン用にクラス化
public class AnimationManager
{
    public static void Play(Animation animation, string animName, System.Action onComplete)
    {
        var anim = new AnimationManager();
        CoroutineManager.Start(anim._PlayAnim(animation, animName, onComplete));
    }

    // コールバック付きでアニメーションを再生
    IEnumerator _PlayAnim(Animation animation, string animName, System.Action onComplete)
    {
        animation.Play(animName);

        while(animation.IsPlaying(animName))
        {
            yield return null;
        }

        onComplete();
    }
}
