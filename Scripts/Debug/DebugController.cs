using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : ControllerBase<DebugViewModel>
{
    // 入力数値を取得
    int _InputValue
    {
        get
        {
            int value = 1;
            int.TryParse(this._ViewModel.InputVallue.text, out value);
            return value;
        }
    }

    public static void Init()
    {
#if !DEBUG        
        throw new Exception("デバック機能が無効です");
#endif
        const string prefabPath = "Prefabs/SushiKoiDebug";
        var prefab = Resources.Load<GameObject>(prefabPath);
        var obj = GameObject.Instantiate(prefab);
        GameObject.DontDestroyOnLoad(obj);
    }

    protected override void _OnStart()
    {
        // 初回非表示
        this._ViewModel.Root.SetActive(false);

        // 週加算
        this._ViewModel.AddWeek.OnClick = () => 
        {
            var date = DataManager.GetDate();
            date.Add();
        };

        // 週減算
        this._ViewModel.DelWeek.OnClick = () => 
        {
            var date = DataManager.GetDate();
            date.Rewind();
        };

        // ストレス加算
        this._ViewModel.AddHp.OnClick = () => 
        {
            var player = DataManager.GetPlayerChara();
            player.Hp.Add(this._InputValue);
        };

        // 学力加算
        this._ViewModel.AddEdu.OnClick = () => 
        {
            var player = DataManager.GetPlayerChara();
            player.Edu.Add(this._InputValue);
        };

        // 運動力加算
        this._ViewModel.AddStr.OnClick = () => 
        {
            var player = DataManager.GetPlayerChara();
            player.Str.Add(this._InputValue);
        };
    }

    protected override void _OnUpdate()
    {
        // キーが押されると表示切り替え
        if(Input.GetKeyDown(KeyCode.Q))
        {
            bool activeSwitch = !this._ViewModel.Root.activeSelf;
            this._ViewModel.Root.SetActive(activeSwitch);
        }

        this._UpdateView();
    }

    void _UpdateView()
    {
        var date = DataManager.GetDate();
        var player = DataManager.GetPlayerChara();

        this._ViewModel.WeekValue.text = date.DateLabel;
        this._ViewModel.HpValue.text = player.Hp.Value.ToString();
        this._ViewModel.EduValue.text = player.Edu.Value.ToString();
        this._ViewModel.StrValue.text = player.Str.Value.ToString();
    }
}
