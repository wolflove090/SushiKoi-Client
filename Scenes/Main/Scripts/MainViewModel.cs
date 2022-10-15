using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainViewModel : ViewModelBase
{
    // コマンド表示
    public GameObject ComanndList;
    public ButtonBase CommandButton;

    // 週表示
    public TextMeshProUGUI DateLabel;

    // ステータス表示
    public GameObject StatusContent;
    public GameObject Status;

    // 好感度表示(プロト用)
    public GameObject LikabilityStatus;

    // コマンドアクション
    public CommandActionController CommandAction;

    // 日付更新演出
    public DayUpdateController DayUpdate;
}
