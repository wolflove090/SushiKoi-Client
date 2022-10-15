using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monoじゃなくてもコルーチンを発火できるように
public class CoroutineManager : MonoBehaviour
{
    static CoroutineManager _Singleton
    {
        get
        {
            if(_SingletonValue == null)
            {
                var obj = new GameObject("CoroutineManager");
                GameObject.DontDestroyOnLoad(obj);
                _SingletonValue = obj.AddComponent<CoroutineManager>();
            }

            return _SingletonValue;
        }

    }
    static CoroutineManager _SingletonValue;

    public static void Start(IEnumerator action)
    {
        _Singleton._Start(action);
    }

    void _Start(IEnumerator action)
    {
        this.StartCoroutine(action);
    }
}
