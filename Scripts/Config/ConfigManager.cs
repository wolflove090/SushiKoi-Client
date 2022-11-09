using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager
{
    static ConfigManager _Singleton;

    CommandConfig _CommandConfig;

    // --------------------
    // 初期化処理
    // --------------------
    public static void Init()
    {
        if(_Singleton != null)
            return;

        _Singleton = new ConfigManager();

        // コンフィグデータの読み込み
        ConfigManager.LoadCommandConfig();
    }

    // --------------------
    // コマンドコンフィグ
    // --------------------
    public static CommandConfig GetCommandConfig()
    {
        if(_Singleton == null)
            throw new System.Exception("シングルトン未作成");

        return _Singleton._CommandConfig;
    }

    // --------------------
    // コマンドデータロード
    // --------------------
    public static void LoadCommandConfig()
    {
        if(_Singleton == null)
            throw new System.Exception("シングルトン未作成");

        _Singleton._CommandConfig = Resources.Load<CommandConfig>("CommandConfig");
    }
}
