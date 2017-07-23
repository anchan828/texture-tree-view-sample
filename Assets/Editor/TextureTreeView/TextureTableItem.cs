using UnityEditor.IMGUI.Controls;

public class TextureTableItem : TreeViewItem
{
    public TextureElement element { get; set; }

    public TextureTableItem(int id, string texturePath) : base(id)
    {
        element = new TextureElement(texturePath);
    }
}