using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceController : ChildController<ChoiceViewModel, ChoiceLinker>
{
    protected override void _OnStart()
    {
        Debug.Log("OnStart");

        foreach(var choice in this._Linker.ChoiceButtons)
        {
            var obj = GameObject.Instantiate(this._ViewModel.ChoiceHandlerButton, Vector3.zero, Quaternion.identity, this._ViewModel.ContentPanel.transform);
            
            // ラベル設定
            var label = obj.transform.Find("Text").GetComponent<Text>();
            label.text = choice.ButtonLabel;

            // ボタンアクション設定
            var button = obj.GetComponent<Button>();
            button.onClick.AddListener(() => 
            {
                choice.OnClick();
            });
        }
    }
}

// --------------------
// ボタン作成用構造体クラス
// --------------------
public class ChoiceButton
{
    public string ButtonLabel;
    public System.Action OnClick;
}
