using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Naninovel;

public class TitleController : ControllerBase<TitleViewModel>
{
    protected override void _OnStart()
    {
        // ノベルモード切り替え
        var toGameCommand = new SwitchToGameMode();
        toGameCommand.ExecuteAsync().Forget();

        // データマネージャー初期化
        DataManager.Init();

        // コンフィグマネージャー初期化
        ConfigManager.Init();

        // サウンドマネージャー初期化
        SoundManager.Init();

        // パフォーマンスマネージャー
        PerformanceManager.Init();

#if SUSHI_DEBUG        
        // デバック機能初期化
        Debug.Log("デバックモードです");
        DebugController.Init();
#endif

        this._ViewModel.GameStart.OnClick = () => 
        {
            Debug.Log("ゲームスタート");

            SceneManager.LoadScene("Main");
            //NovelUtil.StartNovel("Before0502");
        };

        // 初めからボタン
        this._ViewModel.StartButton.OnClick = () => 
        {
            Debug.Log("初めから");
            SceneManager.LoadScene("Main");
        };

        // 続きからボタン
        this._ViewModel.ContinueButton.OnClick = () => 
        {
            Debug.Log("続きから");
            DataManager.Load();
            SceneManager.LoadScene("Main");
        };

        // データの初期化
        this._ViewModel.ResetButton.OnClick = () => 
        {
            Debug.Log("データリセット");
            DataManager.Reset();
        };

    }

}
