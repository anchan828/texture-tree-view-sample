using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class IdColumn : MultiColumnHeaderState.Column, IHasColumnIndex
{
    public ColumnIndex index { get; } = ColumnIndex.Id;

    public IdColumn()
    {
        width = minWidth = maxWidth = 32;
        headerContent = new GUIContent("ID");
    }
}