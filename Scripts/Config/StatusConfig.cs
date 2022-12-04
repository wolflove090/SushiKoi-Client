using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusConfig", menuName = "すし恋/StatusConfig")]
public class StatusConfig : ScriptableObject
{
    public StatusStruct Hp;
    public StatusStruct Edu;
    public StatusStruct Str;
    public StatusStruct Freshness;
    public StatusStruct Money;
    public StatusStruct Fahionable;
}

// --------------------
// ステータス構造体
// --------------------
[System.Serializable]
public class StatusStruct
{
    public int InitValue;
    public int MaxValue;
}