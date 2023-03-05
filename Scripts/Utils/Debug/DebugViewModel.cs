using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugViewModel : ViewModelBase
{
    public GameObject Root;

    public TMP_InputField InputVallue;

    // デバックボタン
    public ButtonBase AddWeek;
    public ButtonBase DelWeek;
    public ButtonBase AddHp;
    public ButtonBase AddEdu;
    public ButtonBase AddStr;

    // ステータス表示
    public TextMeshProUGUI WeekValue;
    public TextMeshProUGUI HpValue;
    public TextMeshProUGUI EduValue;
    public TextMeshProUGUI StrValue;
}
