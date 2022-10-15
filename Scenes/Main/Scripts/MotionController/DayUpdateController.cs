using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DayUpdateController : ChildController<DayUpdateViewModel, MotionControllerLinker>
{
    public void Init()
    {
        // アニメーションまで非表示
        this.transform.Find("Root").gameObject.SetActive(false);
    }

    protected override void _OnStart()
    {
        // 現在の時間でラベル表示
        var label = this.transform.Find("Root/DayLabel").GetComponent<TextMeshProUGUI>();
        var date = DataManager.GetDate();
        label.text = date.DateLabel;

        // 切り替わりアニメーション再生
        var anim = this.GetComponent<Animation>();
        anim.Play("day_update", () => 
        {
            this._Linker.OnComplete();
        });
    }
}
