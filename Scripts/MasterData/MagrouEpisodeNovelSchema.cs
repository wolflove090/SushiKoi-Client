using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagrouEpisodeNovelSchema
{
    [CsvColumnAtrribute("Month")]
    public int Month;

    [CsvColumnAtrribute("Week")]
    public int Week;

    [CsvColumnAtrribute("Likability")]
    public int Likability;

    [CsvColumnAtrribute("Edu")]
    public int Edu;

    [CsvColumnAtrribute("Str")]
    public int Str;

    [CsvColumnAtrribute("RicePower")]
    public int RicePower;

    [CsvColumnAtrribute("NovelName")]
    public string NovelName;

    [CsvColumnAtrribute("Description")]
    public string Description;
}
