using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class ExtensionColumn : MultiColumnHeaderState.Column, IHasColumnIndex
{
    public ColumnIndex index { get; } = ColumnIndex.Extension;

    public ExtensionColumn()
    {
        width = 80;
        headerContent = new GUIContent("Extension");
    }
}