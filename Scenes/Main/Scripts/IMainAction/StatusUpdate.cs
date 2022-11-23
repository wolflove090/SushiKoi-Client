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

            var icon = obj.transform.Find("Icon");
            status.Icon = icon.gameObject;
            status.Icon.SetActive(false);

            var anim = obj.GetComponent<Animation>();
            status.Anim = anim;

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

    // ステータス更新アニメーション
    public void PlayUpdateAnim(System.Action onComplete, CommandStruct command)
    {
        // すべてのアニメーション完了時にonCompleteを叩く
        int count = 1;
        System.Action complete = () => 
        {
            count--;

            if(count <= 0)
            {
                this._Update();
                onComplete();
            }
        };

        foreach(var status in this._StatusList)
        {
            count++;
            status.PlayUpdateAnim(complete, command);
        }

        complete();
    }
}

// --------------------
// ステータス定義
// --------------------
public class StatusLabel
{
    public string Name;
    public TextMeshProUGUI ValueLabel;
    public PlayerCharaData.ParamType ParamType;
    public GameObject Icon;
    public Animation Anim;

    //public IParam TargetParam;


    System.Func<int> _GetValue;

    public void Update() 
    {
        this.ValueLabel.text = this._GetValue().ToString();
    }

    // ステータス更新アニメーション
    public void PlayUpdateAnim(System.Action onComplete, CommandStruct command)
    {
        var up = this.Icon.transform.Find("UpValue").GetComponent<TextMeshProUGUI>();
        var down = this.Icon.transform.Find("DownValue").GetComponent<TextMeshProUGUI>();

        up.gameObject.SetActive(false);
        down.gameObject.SetActive(false);

        bool isTarget = false;
        foreach(var value in command.AddValue)
        {
            if(value.TargetType == this.ParamType)
            {
                isTarget = true;
                if(PlayerCharaData.IsPositiveChange(value))
                {
                    up.gameObject.SetActive(true);
                    up.text = value.Value.ToString();
                }
                else
                {
                    down.gameObject.SetActive(true);
                    down.text = value.Value.ToString();
                }
            }
        }

        this.Icon.SetActive(isTarget);

        this.Anim.Play("status_update", 
        () => 
        {
            this.Icon.SetActive(false);
            onComplete();
        });
    }

    // --------------------
    // ステータスラベルのオブジェクト生成
    // --------------------
    public static StatusLabel[] CreateStatusLabel()
    {
        var result = new List<StatusLabel>();

        result.Add(new StatusLabel() 
        {
            Name = "ストレス",
            ParamType = PlayerCharaData.ParamType.Hp,
            _GetValue = () => 
            {
                var player = DataManager.GetPlayerChara();
                return player.Hp.Value;
            }
        });

        result.Add(new StatusLabel() 
        {
            Name = "学力",
            ParamType = PlayerCharaData.ParamType.Edu,
            _GetValue = () => 
            {
                var player = DataManager.GetPlayerChara();
                return player.Edu.Value;
            }
        });

        result.Add(new StatusLabel() 
        {
            Name = "運動",
            ParamType = PlayerCharaData.ParamType.Str,
            _GetValue = () => 
            {
                var player = DataManager.GetPlayerChara();
                return player.Str.Value;
            }            
        });

        /*
        result.Add(new StatusLabel() 
        {
            Name = "しゃり力",
            _GetValue = () => 
            {
                var player = DataManager.GetPlayerChara();
                return player.RicePower.Value;
            }        
        });*/

        result.Add(new StatusLabel() 
        {
            Name = "鮮度",
            ParamType = PlayerCharaData.ParamType.Freshness,
            _GetValue = () => 
            {
                var player = DataManager.GetPlayerChara();
                return player.Freshness.Value;
            }        
        });

        result.Add(new StatusLabel() 
        {
            Name = "所持金",
            ParamType = PlayerCharaData.ParamType.Money,
            _GetValue = () => 
            {
                var player = DataManager.GetPlayerChara();
                return player.Money.Value;
            }
        });

        result.Add(new StatusLabel() 
        {
            Name = "おしゃれ",
            ParamType = PlayerCharaData.ParamType.Fahionable,
            _GetValue = () => 
            {
                var player = DataManager.GetPlayerChara();
                return player.Fahinoable.Value;
            }
        });

        return result.ToArray();
    }
}
