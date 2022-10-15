using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// --------------------
// 日付変更演出
// --------------------
public class OverTheDateEffect : IMainAction
{
    TextMeshProUGUI _DateLabel;
    DayUpdateController _MotionController;

    public OverTheDateEffect(TextMeshProUGUI label, DayUpdateController controller)
    {
        this._DateLabel = label;
        this._MotionController = controller;
        this._Init();
        this._MotionController.Init();
    }

    void IMainAction.Play(System.Action onComplete)
    {
        Debug.Log("日付更新演出");

        var date = DataManager.GetDate();
        date.Add();

        // 切り替わりアニメーション再生
        this._MotionController.ExternalStart(new MotionControllerLinker()
        {
            OnComplete = () => 
            {
                this._Init();
                onComplete();
            }
        });
    }

    void _Init()
    {
        var date = DataManager.GetDate();
        this._DateLabel.text = $"{date.Month}月{date.Week}週";
    }
}

