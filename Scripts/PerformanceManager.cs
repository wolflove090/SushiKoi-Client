using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PerformanceManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _FpsValue;

    [SerializeField]
    TextMeshProUGUI _MemoryValue;

    /* ------------------------------ 
     * Init
     * ------------------------------ */
     public static void Init()
     {
        const string prefabPath = "Prefabs/PerformanceManager";
        var prefab = Resources.Load<GameObject>(prefabPath);
        var obj = GameObject.Instantiate(prefab);
        GameObject.DontDestroyOnLoad(obj);
     }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    int _FrameCount;
    float _Time;

    // Update is called once per frame
    void Update()
    {
        this._FrameCount++;
        this._Time += Time.unscaledDeltaTime;

        if(this._Time >= 0.5f)
        {
            var fps = Mathf.Floor(this._FrameCount / this._Time);
            this._FpsValue.text = fps.ToString();

            this._Time = 0;
            this._FrameCount = 0;
        }
    }
}
