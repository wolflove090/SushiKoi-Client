using Naninovel;

public class NovelUtil
{
    public static void StartNovel(string scriptName, string label, System.Action onComplete = null)
    {
        NovelUtil._StartNovel(scriptName, label, onComplete);
    }

    public static void StartNovel(string scriptName, System.Action onComplete = null)
    {
        NovelUtil._StartNovel(scriptName, "", onComplete);
    }

    static void _StartNovel(string scriptName, string label, System.Action onComplete)
    {
        // 完了コールバック設定
        SwitchToGameMode.CompleteNovel.RemoveAllListeners();
        if(onComplete != null) 
           SwitchToGameMode.CompleteNovel.AddListener(() => {onComplete();});

        var toNovelCommand = new SwitchToNovelMode()
        {
            ScriptName = scriptName,
            Label  = label,
        };
        toNovelCommand.ExecuteAsync().Forget();
    }
}
