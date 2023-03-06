using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResoucePathDictionary
{
    // TODO 本実装時はコマンドタイプで切り分け
    public static string GetCommandAnimationPrefabPath(string commandName, bool isSuccess)
    {
        Debug.Log(commandName);
        string dirName = isSuccess ? "Success" : "Failure";

        switch(commandName)
        {
            case "勉強":
                return $"Prefabs/CommandAnimation/Study/{dirName}/CommandAnimation";
            case "部活":
                return $"Prefabs/CommandAnimation/Club/{dirName}/CommandAnimation";
            case "バイト":
                return $"Prefabs/CommandAnimation/Job/{dirName}/CommandAnimation";
            case "おでかけ":
                return $"Prefabs/CommandAnimation/Dating/{dirName}/CommandAnimation";
            case "エステ":
                return $"Prefabs/CommandAnimation/Esthetic/{dirName}/CommandAnimation";
            case "魅力":
                return $"Prefabs/CommandAnimation/Charm/{dirName}/CommandAnimation";
        }

        return $"Prefabs/CommandAnimation/Study/{dirName}/CommandAnimation";
    }


}
