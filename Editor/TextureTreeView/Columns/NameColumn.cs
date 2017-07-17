using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class NameColumn: MultiColumnHeaderState.Column, IHasColumnIndex
{
    public ColumnIndex index { get; } = ColumnIndex.Name;

    public NameColumn()
    {
        width = 200;
        headerContent = new GUIContent("Name");
    }
}