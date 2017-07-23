using UnityEditor.IMGUI.Controls;

public class IconColumn : MultiColumnHeaderState.Column, IHasColumnIndex
{
    public ColumnIndex index { get; } = ColumnIndex.Icon;

    public IconColumn()
    {
        width = minWidth = maxWidth = 32;
        canSort = false;
        contextMenuText = "Icon";
    }
}