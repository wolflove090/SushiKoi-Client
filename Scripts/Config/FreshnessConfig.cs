using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------------------
// 鮮度コンフィグ
// --------------------
[CreateAssetMenu(fileName = "FreshnessConfig", menuName = "すし恋/FreshnessConfig")]
public class FreshnessConfig : ScriptableObject
{
    public int DefaultDecValue;

    public CustomDecFreshness[] CustomDecValue;
}

// --------------------
// CustomDecFreshness
// --------------------
[System.Serializable]
public class CustomDecFreshness
{
    public int Month;
    public int DecValue;
}
