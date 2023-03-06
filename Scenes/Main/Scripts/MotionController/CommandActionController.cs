using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------------------
// コマンドアニメーション再生コントローラー
// --------------------
public class CommandActionController : ChildController<ViewModelBase, CommandActionLinker>
{
    public void Init() 
    {

    }

    protected override void _OnStart()
    {
        Debug.Log("演出開始");

        var path = ResoucePathDictionary.GetCommandAnimationPrefabPath(this._Linker.ActionName, this._Linker.IsClear);
        var prefab = Resources.Load<GameObject>(path);
        var obj = GameObject.Instantiate(prefab, this.transform);

        var anim = obj.GetComponent<Animation>();
        anim.Play("anim", () => 
        {
            this._Linker.OnComplete();
            GameObject.Destroy(obj);
        });
    }
}
