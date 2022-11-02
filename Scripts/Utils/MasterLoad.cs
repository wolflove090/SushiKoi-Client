using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class MasterUtil
{
    // --------------------
    // マスタをCSVから取得
    // --------------------
    public static T[] LoadAll<T>(string masterPath) 
        where T : class, new()
    {
        var csvFile = Resources.Load<TextAsset>(masterPath);

        // テキストデータを配列に変換
        var lines = csvFile.text.Split(new []{Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

        // 改行コードを置換
        for(int i = 0; i < lines.Length; i++)
        {
            lines[i] = lines[i].Replace("\n", "");
            lines[i] = lines[i].Replace("\r", "");
            lines[i] = lines[i].Replace("\r\n", "");
        }

        Dictionary<string, int> columnIndex = new Dictionary<string, int>();
        int lineIndex = 0;

        // カラムのインデックスを検索
        var fieldInfos = typeof(T).GetFields();
        foreach(var field in fieldInfos)
        {
            var columnName = field.GetCustomAttribute<CsvColumnAtrribute>().Name;
            for(int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var colums = line.Split(',');
                for(int columIndex = 0; columIndex < colums.Length; columIndex++)
                {
                    if(columnName == colums[columIndex])
                    {
                        // TODO カラム名が重複してないことが前提
                        columnIndex.Add(columnName, columIndex);
                        lineIndex = i;
                        continue;
                    }
                }
            }
        }

        // データをモデルに変換
        List<T> convertData = new List<T>();

        // カラム名の次の行がデータになる前提
        for(int i = lineIndex + 1; i < lines.Length; i++)
        {
            var line = lines[i];
            var colums = line.Split(',');

            if(colums.Length == 0)
                continue;

            // 各データを変数に入れ込み
            var instance = new T();
            foreach(var field in fieldInfos)
            {
                Type fieldType = field.FieldType;
                var columnName = field.GetCustomAttribute<CsvColumnAtrribute>().Name;
                int index = columnIndex[columnName];
                var value = colums[index];

                if(fieldType == typeof(int))
                {
                    field.SetValue(instance, int.Parse(value));
                }
                else if(fieldType == typeof(string))
                {
                    field.SetValue(instance, value);
                }else
                {
                    throw new Exception($"未定義のタイプ ---> {fieldType}");
                }

            }

            convertData.Add(instance);
        }

        return convertData.ToArray();
    }
}

// --------------------
// CSVのカラム名を指定するための属性
// Name に対象のカラム名を指定する
// --------------------
public class CsvColumnAtrribute : System.Attribute
{
    public string Name;

    public CsvColumnAtrribute(string name)
    {
        this.Name = name;
    }
}

// テスト用、いずれ消す
public class TestCsv
{
    [CsvColumnAtrribute("id")]
    public string Id;

    [CsvColumnAtrribute("name")]
    public string Name;

    [CsvColumnAtrribute("description")]
    public string Descrip;
}