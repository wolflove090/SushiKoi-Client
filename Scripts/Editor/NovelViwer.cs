using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NovelViwer : EditorWindow
{
    [MenuItem("すし恋/NovelViwer")]
    static void _Show()
    {
        var window = EditorWindow.GetWindow<NovelViwer>();
        window.minSize = new Vector2(500,800);
        window.Show();
    }

    Vector2 _ScrollPos;
    bool _ShowBeforeNovel;

    NovelData[] _BeforeNovelDatas;

    void OnGUI()
    {
        // 初期化処理
        if(this._BeforeNovelDatas == null)
        {
            // ノベルマスタの取得
            var mstBeforeNovels = MasterUtil.LoadAll<BeforeCommandNovelData>("MasterData/before_command_novel");
            this._BeforeNovelDatas = new NovelData[mstBeforeNovels.Length];
            for(int i = 0; i < mstBeforeNovels.Length; i++)
            {
                var data = mstBeforeNovels[i];
                this._BeforeNovelDatas[i] = new NovelData(data);
            }
        }

        // --------------------
        // コマンド前ADV
        // --------------------
        var color = GUI.color;
        GUI.color = Color.cyan;
        this._ShowBeforeNovel = EditorGUILayout.Foldout(this._ShowBeforeNovel, "コマンド前ノベル", true);
        GUI.color = color;

        if(this._ShowBeforeNovel)
        {
            using (var scroll = new EditorGUILayout.ScrollViewScope(this._ScrollPos, "box"))
            {
                this._ScrollPos = scroll.scrollPosition;

                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField("再生タイミング", GUILayout.Width(100));
                    EditorGUILayout.LabelField("説明", GUILayout.Width(200));
                    EditorGUILayout.LabelField("ファイル", GUILayout.Width(200));
                }

                // ノベルデータ表示
                for(int i = 0; i < this._BeforeNovelDatas.Length; i++)
                {
                    using(new EditorGUILayout.HorizontalScope("box"))
                    {
                        var novel = this._BeforeNovelDatas[i];
                        EditorGUILayout.LabelField($"{novel.Month}月 / {novel.Week}週", GUILayout.Width(100));
                        EditorGUILayout.LabelField(novel.Description, GUILayout.Width(200));
                        EditorGUILayout.ObjectField(novel.NovelFile, typeof(TextAsset), GUILayout.Width(200));
                    }
                }
            }
        }
    }

    class NovelData
    {
        public int Month;
        public int Week;
        public UnityEngine.Object NovelFile;
        public string Description;

        public NovelData(BeforeCommandNovelData data)
        {
            this.Month = data.Month;
            this.Week = data.Week;
            this.Description = data.Description;

            this.NovelFile = Resources.Load($"NovelScripts/BeforeCommandNovel/{data.NovelName}");
        }
    }

}
