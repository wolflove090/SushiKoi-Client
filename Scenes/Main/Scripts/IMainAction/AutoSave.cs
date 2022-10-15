using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------------------
// オートセーブアクション
// --------------------
public class AutoSave : IMainAction
{
    void IMainAction.Play(System.Action onComplete)
    {
        DataManager.Save();
        onComplete();
    }

}
