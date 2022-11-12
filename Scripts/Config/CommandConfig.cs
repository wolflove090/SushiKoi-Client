using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------------------
// コマンドコンフィグ
// --------------------
[CreateAssetMenu(fileName = "CommandConfig", menuName = "すし恋/CommandConfig")]
public class CommandConfig : ScriptableObject
{
    public CommandStruct[] Commands;

    public CommandStruct Rest;
    public CommandStruct Study;
    public CommandStruct Club;
    public CommandStruct Job;
}

// --------------------
// コマンド構造体
// --------------------
[System.Serializable]
public class CommandStruct
{
    public int NeedHp;
    public CommandValue[] AddValue;
}

// --------------------
// コマンドの変動数値情報
// --------------------
[System.Serializable]
public class CommandValue
{
    public PlayerCharaData.ParamType TargetType;
    public int Value;
}


