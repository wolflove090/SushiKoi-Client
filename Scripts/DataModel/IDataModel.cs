using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataModel
{
    void Load(SaveData saveData);
    SaveData SetSaveData(SaveData saveData);
}