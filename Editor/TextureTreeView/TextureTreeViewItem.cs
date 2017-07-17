using UnityEditor.IMGUI.Controls;

public class TextureTreeViewItem : TreeViewItem
{
    public TextureElement element { get; set; }

    public TextureTreeViewItem(int id, string texturePath) : base(id)
    {
        element = new TextureElement(texturePath);
    }
}