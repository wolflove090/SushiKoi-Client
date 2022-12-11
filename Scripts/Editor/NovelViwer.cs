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

    NovelData _BeforeNovelData;
    NovelData _AfterNovelData;
    EpisodeNovelData _MagrouNovelData;

    void OnGUI()
    {
        // 初期化処理
        if(this._BeforeNovelData == null)
        {
            // コマンド前 ノベルマスタの取得
            var mstBeforeNovels = MasterUtil.LoadAll<BeforeCommandNovelData>();
            
            this._BeforeNovelData = new NovelData();
            this._BeforeNovelData.NovelViewDatas = new NovelViewData[mstBeforeNovels.Length];
            for(int i = 0; i < mstBeforeNovels.Length; i++)
            {
                var data = mstBeforeNovels[i];
                this._BeforeNovelData.NovelViewDatas[i] = new NovelViewData(data);
            }

            // コマンド後 ノベルマスタの取得
            var mstAfterNovels = MasterUtil.LoadAll<AfterCommandNovelData>();
            this._AfterNovelData = new NovelData();
            this._AfterNovelData.NovelViewDatas = new NovelViewData[mstAfterNovels.Length];
            for(int i = 0; i < mstAfterNovels.Length; i++)
            {
                var data = mstAfterNovels[i];
                this._AfterNovelData.NovelViewDatas[i] = new NovelViewData(data);
            }

            // コマンド後 ノベルマスタの取得
            var mstEpisodeNovels = MasterUtil.LoadAll<MagrouEpisodeNovelSchema>();
            this._MagrouNovelData = new EpisodeNovelData();
            this._MagrouNovelData.NovelViewDatas = new EpisodeNovelViewData[mstEpisodeNovels.Length];
            for(int i = 0; i < mstEpisodeNovels.Length; i++)
            {
                var data = mstEpisodeNovels[i];
                this._MagrouNovelData.NovelViewDatas[i] = new EpisodeNovelViewData(data);
            }
        }

        using (var scroll = new EditorGUILayout.ScrollViewScope(this._ScrollPos, "box"))
        {
            this._ScrollPos = scroll.scrollPosition;

            // --------------------
            // コマンド前ADV
            // --------------------
            var color = GUI.color;
            GUI.color = Color.cyan;
            this._BeforeNovelData.ShowNovels = EditorGUILayout.Foldout(this._BeforeNovelData.ShowNovels, "コマンド前ノベル", true);
            GUI.color = color;

            if(this._BeforeNovelData.ShowNovels)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField("再生タイミング", GUILayout.Width(100));
                    EditorGUILayout.LabelField("説明", GUILayout.Width(200));
                    EditorGUILayout.LabelField("ファイル", GUILayout.Width(200));
                }

                // ノベルデータ表示
                for(int i = 0; i < this._BeforeNovelData.NovelViewDatas.Length; i++)
                {
                    using(new EditorGUILayout.HorizontalScope("box"))
                    {
                        var novel = this._BeforeNovelData.NovelViewDatas[i];
                        EditorGUILayout.LabelField($"{novel.Month}月 / {novel.Week}週", GUILayout.Width(100));
                        EditorGUILayout.LabelField(novel.Description, GUILayout.Width(200));
                        EditorGUILayout.ObjectField(novel.NovelFile, typeof(TextAsset), false, GUILayout.Width(200));
                    }
                }
            }

            // --------------------
            // コマンド後ADV
            // --------------------
            color = GUI.color;
            GUI.color = Color.cyan;
            this._AfterNovelData.ShowNovels = EditorGUILayout.Foldout(this._AfterNovelData.ShowNovels, "コマンド後ノベル", true);
            GUI.color = color;

            if(this._AfterNovelData.ShowNovels)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField("再生タイミング", GUILayout.Width(100));
                    EditorGUILayout.LabelField("説明", GUILayout.Width(200));
                    EditorGUILayout.LabelField("ファイル", GUILayout.Width(200));
                }

                // ノベルデータ表示
                for(int i = 0; i < this._AfterNovelData.NovelViewDatas.Length; i++)
                {
                    using(new EditorGUILayout.HorizontalScope("box"))
                    {
                        var novel = this._AfterNovelData.NovelViewDatas[i];
                        EditorGUILayout.LabelField($"{novel.Month}月 / {novel.Week}週", GUILayout.Width(100));
                        EditorGUILayout.LabelField(novel.Description, GUILayout.Width(200));
                        EditorGUILayout.ObjectField(novel.NovelFile, typeof(TextAsset), false, GUILayout.Width(200));
                    }
                }
            }

            // --------------------
            // まぐろうエピソード
            // --------------------
            color = GUI.color;
            GUI.color = Color.cyan;
            this._MagrouNovelData.ShowNovels = EditorGUILayout.Foldout(this._MagrouNovelData.ShowNovels, "まぐろうエピソード", true);
            GUI.color = color;

            if(this._MagrouNovelData.ShowNovels)
            {
                this._MagrouNovelData.ScrollPos = scroll.scrollPosition;

                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField("再生タイミング", GUILayout.Width(100));
                    EditorGUILayout.LabelField("説明", GUILayout.Width(200));
                    EditorGUILayout.LabelField("ファイル", GUILayout.Width(200));
                }

                // ノベルデータ表示
                for(int i = 0; i < this._MagrouNovelData.NovelViewDatas.Length; i++)
                {
                    var novel = this._MagrouNovelData.NovelViewDatas[i];

                    using(new EditorGUILayout.VerticalScope("box"))
                    {
                        using(new EditorGUILayout.HorizontalScope())
                        {
                            EditorGUILayout.LabelField($"{novel.Month}月 / {novel.Week}週", GUILayout.Width(100));
                            EditorGUILayout.LabelField(novel.Description, GUILayout.Width(200));
                            EditorGUILayout.ObjectField(novel.NovelFile, typeof(TextAsset), false, GUILayout.Width(200));
                        }

                        using (new EditorGUILayout.HorizontalScope())
                        {
                            EditorGUILayout.LabelField($"信頼度：{novel.Likability}", GUILayout.Width(80));
                            EditorGUILayout.LabelField($"学力：{novel.Edu}", GUILayout.Width(80));
                            EditorGUILayout.LabelField($"運動力：{novel.Str}", GUILayout.Width(80));
                            EditorGUILayout.LabelField($"シャリ力：{novel.RicePower}", GUILayout.Width(80));
                        }
                    }
                    
                    EditorGUILayout.Space();
                }
            }
        }
    }

    // --------------------
    // ノベル表示用の構造体クラス
    // --------------------
    class NovelData
    {
        public NovelViewData[] NovelViewDatas;
        public bool ShowNovels;
    }

    // --------------------
    // 表示用ノベルデータ
    // --------------------
    class NovelViewData
    {
        public int Month;
        public int Week;
        public UnityEngine.Object NovelFile;
        public string Description;

        public NovelViewData(BeforeCommandNovelData data)
        {
            this.Month = data.Month;
            this.Week = data.Week;
            this.Description = data.Description;

            this.NovelFile = Resources.Load($"NovelScripts/BeforeCommandNovel/{data.NovelName}");
        }

        public NovelViewData(AfterCommandNovelData data)
        {
            this.Month = data.Month;
            this.Week = data.Week;
            this.Description = data.Description;

            this.NovelFile = Resources.Load($"NovelScripts/AfterCommandNovel/{data.NovelName}");
        }
    }

    // --------------------
    // ノベル表示用の構造体クラス
    // --------------------
    class EpisodeNovelData
    {
        public EpisodeNovelViewData[] NovelViewDatas;
        public Vector2 ScrollPos;
        public bool ShowNovels;
    }

    // --------------------
    // 表示用ノベルデータ
    // --------------------
    class EpisodeNovelViewData
    {
        public int Month;
        public int Week;
        public int Likability;
        public int Edu;
        public int Str;
        public int RicePower;

        public UnityEngine.Object NovelFile;
        public string Description;

        public EpisodeNovelViewData(MagrouEpisodeNovelSchema data)
        {
            this.Month = data.Month;
            this.Week = data.Week;
            this.Likability = data.Likability;
            this.Edu = data.Edu;
            this.Str = data.Str;
            this.RicePower = data.RicePower;
            this.Description = data.Description;

            this.NovelFile = Resources.Load($"NovelScripts/EpisodeMagrou/{data.NovelName}");
        }
    }
}
