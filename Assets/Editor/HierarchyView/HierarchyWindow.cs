using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.IMGUI.Controls;
public class HierarchyWindow : EditorWindow
{

    [MenuItem("TreeViewSample/HierarchyWindow")]
    static void Open()
    {
        GetWindow<HierarchyWindow>();
    }
    private HierarchyView hierarchyView;

    void OnEnable()
    {
		// 開閉状態や、どのTreeViewItemを選択しているかなどの状態が格納されるstateオブジェクト
        var state = new TreeViewState();
		
        hierarchyView = new HierarchyView(state);

		// 初期化のために呼び出す
        hierarchyView.Reload();
    }

    void OnGUI()
    {
        hierarchyView.OnGUI(new Rect(Vector2.zero, position.size));
    }
}
