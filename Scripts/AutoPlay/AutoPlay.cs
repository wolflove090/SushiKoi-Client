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
        var isAutoPlay = PlayerPrefs.GetInt(AutoPlay.KEY);
        if(isAutoPlay != 1)
            return;

        SceneManager.LoadScene("Title");
        
        // オートプレイ用のオブジェクトを生成
        PlayerPrefs.SetInt(AutoPlay.KEY, 0);
        var obj = new GameObject("AutoPlay");
        obj.AddComponent<AutoPlayExec>();
        GameObject.DontDestroyOnLoad(obj);
    }


    // スタートのタイミングでオートプレイ実行    
    void Start() 
    {
        this.ExecAutoPlay();
    }

    // --------------------
    // オートプレイの実行(仮)
    // シナリオを用意できるようにする
    // --------------------
    async UniTask ExecAutoPlay()
    {
        // 初期化待機用
        await UniTask.Delay(1000);
        this.Click("StartButton");
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
