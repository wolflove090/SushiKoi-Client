using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CsvFilePathAtrribute("MasterData/after_command_novel")]
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
