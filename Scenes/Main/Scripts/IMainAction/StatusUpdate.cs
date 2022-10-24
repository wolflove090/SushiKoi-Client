using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// --------------------
// ステータス更新
// --------------------
public class StatusUpdate : IMainAction
{
    GameObject _StatusRoot;
    GameObject _Status;

    StatusLabel[] _StatusList;

    public StatusUpdate(GameObject root, GameObject status)
    {
        this._StatusRoot = root;
        this._Status = status;

        this._Create();
    }

    void IMainAction.Play(System.Action onComplete)
    {
        Debug.Log("ステータス更新");

        this._Update();
        onComplete();
    }

    void _Create()
    {
        this._StatusList = StatusLabel.CreateStatusLabel();

        foreach(var status in this._StatusList)
        {
            var obj = GameObject.Instantiate(this._Status, Vector3.zero, Quaternion.identity, this._StatusRoot.transform);

            var label = obj.transform.Find("Label").GetComponent<TextMeshProUGUI>();
            label.text = status.Name;

            var value = obj.transform.Find("Value").GetComponent<TextMeshProUGUI>();
            value.text = "0";

            status.ValueLabel = value;

            // 描画更新
            status.Update();

            obj.SetActive(true);
        }
        this._Status.SetActive(false);
    }

    // 描画更新
    void _Update()
    {
        foreach(var status in this._StatusList)
        {
            status.Update();
        }

    }
}

// --------------------
// ステータス定義
// --------------------
public class StatusLabel
{
    public string Name;
    public TextMeshProUGUI ValueLabel;

    public IParam TargetParam;

    System.Func<int> _GetValue;

    public void Update() 
    {
        this.ValueLabel.text = this._GetValue().ToString();
    }

    public static StatusLabel[] CreateStatusLabel()
    {
        var result = new List<StatusLabel>();

        result.Add(new StatusLabel() 
        {
            Name = "ストレス",
            _GetValue = () => 
            {
                var player = DataManager.GetPlayerChara();
                return player.Hp.Value;
            }
        });

        result.Add(new StatusLabel() 
        {
            Name = "親愛度",
            _GetValue = () => 
            {
                return 0;
            }
        });

        result.Add(new StatusLabel() 
        {
            Name = "学力",
            _GetValue = () => 
            {
                var player = DataManager.GetPlayerChara();
                return player.Edu.Value;
            }
        });

        result.Add(new StatusLabel() 
        {
            Name = "運動",
            _GetValue = () => 
            {
                var player = DataManager.GetPlayerChara();
                return player.Str.Value;
            }            
        });

        result.Add(new StatusLabel() 
        {
            Name = "しゃり力",
            _GetValue = () => 
            {
                var player = DataManager.GetPlayerChara();
                return player.RicePower.Value;
            }        
        });


        result.Add(new StatusLabel() 
        {
            Name = "鮮度",
            _GetValue = () => 
            {
                var player = DataManager.GetPlayerChara();
                return player.Freshness.Value;
            }        
        });

        return result.ToArray();
    }
}
