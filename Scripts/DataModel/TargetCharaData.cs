using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------------------
// 攻略対象のキャラデータ
// --------------------
public class TargetCharaData
{
    public IParam Likability = new Likability(); // 好感度
    public int ProgressEpiNum = 0; // 進行エピソード
}

// --------------------
// 好感度
// --------------------
public class Likability : IParam
{
    public int Value
    {
        get
        {
            return this._Value;
        }
    }
    int _Value = 0;

    // 加算
    void IParam.Add(int amount)
    {
        this._Value += amount;
    }
}