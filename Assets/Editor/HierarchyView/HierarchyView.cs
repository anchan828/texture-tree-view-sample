using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.IMGUI.Controls;
public class HierarchyView : TreeView
{
    public HierarchyView(TreeViewState state) : base(state)
    {
    }

    protected override TreeViewItem BuildRoot()
    {
        // Root の depth は -1
        var root = new TreeViewItem { id = 0, depth = -1 };

		// AddChild メソッド、または children プロパティで開閉で表示可能な子要素を追加
        root.AddChild(
            new TreeViewItem
            {
                id = 1,
                depth = 0,
                displayName = "Animals",
                children = new List<TreeViewItem> {
                    new TreeViewItem {id = 2, depth = 1, displayName = "Mammals"},
                }
            }
        );
        // 以下でも同様の実装が行うことが出来る
        // var rows = new List<TreeViewItem> {
        //     new TreeViewItem {id = 1, depth = 0, displayName = "Animals"},
        //     new TreeViewItem {id = 2, depth = 1, displayName = "Mammals"},
        // };
        // Listに追加した順番とdepthによって親子関係を構築できる便利メソッド
        // SetupParentsAndChildrenFromDepths(root, rows);

        return root;
    }
}
