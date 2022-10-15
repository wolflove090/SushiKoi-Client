using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandActionLinker : LinkerBase
{
    public string ActionName;
    public bool IsClear;

    public System.Action OnComplete;
}
