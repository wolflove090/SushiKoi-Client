using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Naninovel;

[InitializeOnLoadAttribute]
public class AutoPlay : Editor
{
    // 保存キー
    public const string KEY = "AutoPlay";

    public enum PlayType
    {
        None = 0,
        Beginning = 1,
        Continue = 2,
    }

    [MenuItem("すし恋/AutoPlay")]
    static void _Exec()
    {
        PlayerPrefs.SetInt(KEY, 1);
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }
}

// --------------------
// オートプレイ実行用のBehaviour
// --------------------
public class AutoPlayExec : MonoBehaviour
{
    // ゲーム起動最初に実行
    [RuntimeInitializeOnLoadMethod]
    static void Init()
    {
// デバックモードのみ
#if !SUSHI_DEBUG  
        return;      
#endif        
        var autoPlayType = PlayerPrefs.GetInt(AutoPlay.KEY);
        if(autoPlayType == 0)
            return;

        SceneManager.LoadScene("Title");

        // オートプレイ用のオブジェクトを生成
        var obj = new GameObject("AutoPlay");
        obj.AddComponent<AutoPlayExec>();
        GameObject.DontDestroyOnLoad(obj);
    }


    // スタートのタイミングでオートプレイ実行    
    void Start() 
    {
        var autoPlayType = (AutoPlay.PlayType)PlayerPrefs.GetInt(AutoPlay.KEY);
        if(autoPlayType == AutoPlay.PlayType.None)
            return;

        PlayerPrefs.SetInt(AutoPlay.KEY, 0);

        switch(autoPlayType)
        {
            case AutoPlay.PlayType.Beginning:
                this.ExecAutoPlayBeginning().Forget();
                break;
            case AutoPlay.PlayType.Continue:
                this.ExecAutoPlayContinue().Forget();
                break;

        }
    }

    // --------------------
    // オートプレイの実行(仮)
    // シナリオを用意できるようにする
    // --------------------
    async UniTask ExecAutoPlayBeginning()
    {
        // 初期化待機用
        await UniTask.Delay(1000);
        this.Click("StartButton");
    }

    // --------------------
    // オートプレイの実行(続きから)
    // --------------------
    async UniTask ExecAutoPlayContinue()
    {
        // 初期化待機用
        await UniTask.Delay(1000);
        this.Click("ContinueButton");
    }


    // --------------------
    // オブジェクト名から対象のボタンをタップ
    // 一旦ボタンベースのみ対応中
    // --------------------
    void Click(string objectName)
    {
        IPointerClickHandler button = null;

        var obj = GameObject.Find(objectName);
        if(obj != null)
            button = obj.GetComponent<ButtonBase>();

        if(button == null)
             throw new System.Exception("ボタンが取得できませんでした");

        button.OnPointerClick(null);
    }
}
