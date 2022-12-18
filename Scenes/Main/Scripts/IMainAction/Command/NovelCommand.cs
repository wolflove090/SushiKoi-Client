using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovelCommand : ICommand
{
    string _Name;
    string _IconPath;
    string _NovelName;

    public NovelCommand(string name, string iconPath, string novelName)
    {
        this._Name = name;
        this._IconPath = iconPath;
        this._NovelName = novelName;
    }

    string ICommand.Name()
    {
        return this._Name;
    }

    string ICommand.IconPath()
    {
        return this._IconPath;
    }

    void ICommand.Play(System.Action onComplete)
    {
        NovelUtil.StartNovel(this._NovelName, onComplete);
    }

}
