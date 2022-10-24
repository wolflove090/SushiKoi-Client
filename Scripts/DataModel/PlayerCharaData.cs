using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------------------
// プレイヤーキャラデータ
// --------------------
public class PlayerCharaData
{
    public IParam Hp = new Hp(); // 体力,ストレス
    public IParam Edu = new Edu(); // 学力
    public IParam Str = new Str(); // 筋力,運動
    public IParam RicePower = new RicePower(); // シャリ力

    public IParam Freshness = new Freshness(); // 鮮度
}

// --------------------
// プレイヤーパラメータ
// --------------------
public interface IParam
{
    int Value{get;}
    void Add(int amount);
}

// TODO 継承を検討
// --------------------
// 体力、ストレス
// --------------------
public class Hp : IParam
{
    public int Value
    {
        get
        {
            return this._Value;
        }
    }
    int _Value = 100;

    // 加算
    void IParam.Add(int amount)
    {
        this._Value += amount;
    }
}

// --------------------
// 学力
// --------------------
public class Edu : IParam
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

// --------------------
// 筋力、運動
// --------------------
public class Str : IParam
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

// --------------------
// シャリ力
// --------------------
public class RicePower : IParam
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

// --------------------
// 鮮度
// --------------------
public class Freshness : IParam
{
    public int Value
    {
        get
        {
            return this._Value;
        }
    }
    int _Value = 100;

    // 加算
    void IParam.Add(int amount)
    {
        this._Value += amount;
    }
}