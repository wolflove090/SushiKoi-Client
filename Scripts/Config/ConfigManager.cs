using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager
{
    static ConfigManager _Singleton;

    CommandConfig _CommandConfig;
    FreshnessConfig _FreshnessConfig;

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
        ConfigManager.LoadFreshnessConfig();
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
    // 鮮度コンフィグ
    // --------------------
    public static FreshnessConfig GetFreshnessConfig()
    {
        if(_Singleton == null)
            throw new System.Exception("シングルトン未作成");

        return _Singleton._FreshnessConfig;
    }

    // --------------------
    // コマンドコンフィグロード
    // --------------------
    public static void LoadCommandConfig()
    {
        if(_Singleton == null)
            throw new System.Exception("シングルトン未作成");

        _Singleton._CommandConfig = Resources.Load<CommandConfig>("Config/CommandConfig");
    }

    // --------------------
    // 鮮度コンフィグロード
    // --------------------
    public static void LoadFreshnessConfig()
    {
        if(_Singleton == null)
            throw new System.Exception("シングルトン未作成");

        _Singleton._FreshnessConfig = Resources.Load<FreshnessConfig>("Config/FreshnessConfig");
    }
}
