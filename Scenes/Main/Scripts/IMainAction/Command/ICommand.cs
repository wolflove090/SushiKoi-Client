using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    string Name();
    string BackPath();
    void Play(System.Action onComplete);
}
