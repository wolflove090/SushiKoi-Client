using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
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
        this._Time += Time.deltaTime;

        if(this._Time >= 0.5f)
        {
            // FPS更新
            var fps = Mathf.Floor(this._FrameCount / this._Time);
            this._FpsValue.text = fps.ToString();

            this._Time = 0;
            this._FrameCount = 0;

            // メモリ使用量更新
            // UnityのProfiler経由なので正確ではないことに注意
            this._MemoryValue.text = ConvertMemorySizeToBestUnit(Profiler.GetTotalAllocatedMemoryLong());
        }
    }

    string ConvertMemorySizeToBestUnit(long memory)
    {
        int count = 0;
        while(memory > 1024)
        {
            memory = memory / 1024;
            count++;
        }

        string unit = "";
        switch(count)
        {
            case 0:
                unit = "B";
                break;
            case 1:
                unit = "KB";
                break;
            case 2:
                unit = "MB";
                break;
            case 3:
                unit = "GB";
                break;
        }

        return $"{memory}{unit}";
    }
}
