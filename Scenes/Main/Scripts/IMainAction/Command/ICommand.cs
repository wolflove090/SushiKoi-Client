using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    string Name();
    string IconPath();
    void Play(System.Action onComplete);
}
