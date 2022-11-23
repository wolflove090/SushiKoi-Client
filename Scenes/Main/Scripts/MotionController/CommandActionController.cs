using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------------------
// コマンドアニメーション再生コントローラー
// --------------------
public class CommandActionController : ChildController<CommandActionViewModel, CommandActionLinker>
{
    public void Init() 
    {
        // 初期は非表示
        this._ViewModel.Root.SetActive(false);
    }

    protected override void _OnStart()
    {
        this._ViewModel.Root.SetActive(true);

        this._ViewModel.ActionName.text = this._Linker.ActionName;
        this._ViewModel.ClearStamp.SetActive(this._Linker.IsClear);

        // 演出再生
        var animation = this.GetComponent<Animation>();
        animation.Play("command_action", () => 
        {
            this._Linker.OnComplete();
            this._ViewModel.Root.SetActive(false);
        });
    }
}
