using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class BytesColumn : MultiColumnHeaderState.Column, IHasColumnIndex
{
    public ColumnIndex index { get; } = ColumnIndex.Bytes;

    public BytesColumn()
    {
        width = 80;
        headerContent = new GUIContent("Bytes");
    }
}