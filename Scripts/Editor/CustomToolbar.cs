using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// --------------------
// カスタムツールバー
// --------------------
public static class CustomToolbar
{
    private static readonly Type         TOOLBAR_TYPE                   = typeof(EditorGUI).Assembly.GetType("UnityEditor.Toolbar");
    private static readonly FieldInfo    TOOLBAR_GET                    = TOOLBAR_TYPE.GetField("get");
    private static readonly Type         GUI_VIEW_TYPE                  = typeof(EditorGUI).Assembly.GetType("UnityEditor.GUIView");
    private static readonly BindingFlags BINDING_ATTR                   = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
    private static readonly PropertyInfo GUI_VIEW_IMGUI_CONTAINER       = GUI_VIEW_TYPE.GetProperty("imguiContainer", BINDING_ATTR);
    private static readonly FieldInfo    IMGUI_CONTAINER_ON_GUI_HANDLER = typeof(IMGUIContainer).GetField("m_OnGUIHandler", BINDING_ATTR);
    private static readonly GUIContent ICON_REFRESH = EditorGUIUtility.IconContent("d_Refresh");


    [InitializeOnLoadMethod]
    private static void InitializeOnLoad()
    {
        EditorApplication.update += OnUpdate;
    }

    private static void OnUpdate()
    {
        var toolbar = TOOLBAR_GET.GetValue(null);
        if(toolbar == null) return;
        EditorApplication.update -= OnUpdate;
        AddHandler(toolbar);
    }

    private static void AddHandler(object toolbar)
    {
        var container = GUI_VIEW_IMGUI_CONTAINER.GetValue(toolbar, null) as IMGUIContainer;
        var handler = IMGUI_CONTAINER_ON_GUI_HANDLER.GetValue(container) as Action;

        handler += OnGUI;
        IMGUI_CONTAINER_ON_GUI_HANDLER.SetValue(container, handler);
    }

    private static void OnGUI()
    {
        // ========== リフレッシュボタン
        bool isAutoRefresh = EditorPrefs.GetBool("kAutoRefresh");
        if(isAutoRefresh)
            return;

        var rect = new Rect(580, 4, 30,  24);

        if(GUI.Button(rect, ICON_REFRESH))
        {
            AssetDatabase.Refresh();
        }
    }
}
