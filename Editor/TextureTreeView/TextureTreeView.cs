using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class TextureTreeView : TreeView
{
    private const string sortedColumnIndexStateKey = "TextureTreeViewWindow_sortedColumnIndex";


    public TextureTreeView(TreeViewState state, MultiColumnHeader multiColumnHeader) : base(state, multiColumnHeader)
    {
        rowHeight = 32;
        showAlternatingRowBackgrounds = true;
        multiColumnHeader.sortingChanged += SortItems;
        
        multiColumnHeader.ResizeToFit();
        Reload();
        // リロードしてTreeViewItemのビルドが終わったあとにソート関連のセットアップを行う。
        multiColumnHeader.sortedColumnIndex = SessionState.GetInt(sortedColumnIndexStateKey, -1);
    }

    protected override TreeViewItem BuildRoot()
    {
        var rootItem = new TreeViewItem
        {
            depth = -1
        };
        var textureExtensions = new[] {".jpg", ".png"};
        var texturePaths = AssetDatabase.GetAllAssetPaths()
            .Where(path => path.StartsWith("Assets"))
            .Where(path => textureExtensions.Contains(Path.GetExtension(path)));

        var index = 1;
        foreach (var texturePath in texturePaths)
        {
            rootItem.AddChild(new TextureTreeViewItem(index++, texturePath));
        }

        return rootItem;
    }

    protected override void RowGUI(RowGUIArgs args)
    {
        var textureTreeViewItem = (TextureTreeViewItem) args.item;

        for (var visibleColumnIndex = 0; visibleColumnIndex < args.GetNumVisibleColumns(); visibleColumnIndex++)
        {
            var rect = args.GetCellRect(visibleColumnIndex);
            var columnIndex = (ColumnIndex) args.GetColumn(visibleColumnIndex);

            var labelStyle = args.selected ? EditorStyles.whiteLabel : EditorStyles.label;
            labelStyle.alignment = TextAnchor.MiddleLeft;
            switch (columnIndex)
            {
                case ColumnIndex.Id:
                    EditorGUI.LabelField(rect, args.item.id.ToString(), labelStyle);
                    break;
                case ColumnIndex.Icon:
                    EditorGUI.DrawPreviewTexture(rect, textureTreeViewItem.element.icon, null, ScaleMode.ScaleAndCrop);
                    break;
                case ColumnIndex.Name:
                    EditorGUI.LabelField(rect, textureTreeViewItem.element.name, labelStyle);
                    break;
                case ColumnIndex.Extension:
                    EditorGUI.LabelField(rect, textureTreeViewItem.element.extension, labelStyle);
                    break;
                case ColumnIndex.Bytes:
                    EditorGUI.LabelField(rect, EditorUtility.FormatBytes(textureTreeViewItem.element.bytes),
                        labelStyle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, null);
            }
        }
    }

    public void SortItems(MultiColumnHeader multiColumnHeader)
    {
        SessionState.SetInt(sortedColumnIndexStateKey,  multiColumnHeader.sortedColumnIndex);
        var index = (ColumnIndex) multiColumnHeader.sortedColumnIndex;
        var ascending = multiColumnHeader.IsSortedAscending(multiColumnHeader.sortedColumnIndex);

        var items = rootItem.children.Cast<TextureTreeViewItem>();

        IOrderedEnumerable<TextureTreeViewItem> orderedEnumerable;

        switch (index)
        {
            case ColumnIndex.Id:
                orderedEnumerable = items.OrderBy(item => item.id);
                break;
            case ColumnIndex.Name:
                orderedEnumerable = items.OrderBy(item => item.element.name);
                break;
            case ColumnIndex.Extension:
                orderedEnumerable = items.OrderBy(item => item.element.extension);
                break;
            case ColumnIndex.Bytes:
                orderedEnumerable = items.OrderBy(item => item.element.bytes);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
        }

        items = orderedEnumerable.AsEnumerable();

        if (!ascending)
        {
            items = items.Reverse();
        }

        rootItem.children = items.Cast<TreeViewItem>().ToList();
        BuildRows(rootItem);
    }

    protected override void DoubleClickedItem(int id)
    {
        var textureTreeViewItem = (TextureTreeViewItem) FindItem(id, rootItem);
        EditorGUIUtility.PingObject(textureTreeViewItem?.element.icon);
    }

    protected override bool DoesItemMatchSearch(TreeViewItem item, string search)
    {
        var textureTreeViewItem = (TextureTreeViewItem) item;
        return textureTreeViewItem.element.name.Contains(search)
               | textureTreeViewItem.element.extension.Contains(search);
    }
}
