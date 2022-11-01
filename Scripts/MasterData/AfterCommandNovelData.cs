using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterCommandNovelData
{
    [CsvColumnAtrribute("Month")]
    public int Month;
    [CsvColumnAtrribute("Week")]
    public int Week;
    [CsvColumnAtrribute("NovelName")]
    public string NovelName;

    [CsvColumnAtrribute("Description")]
    public string Description;
}
