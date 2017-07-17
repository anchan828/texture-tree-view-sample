using System;
using System.IO;
using UnityEditor;
using UnityEngine;

[Serializable]
public class TextureElement
{
    public Texture2D icon { get; set; }
    public string name { get; set; }
    public string extension { get; set; }
    public long bytes { get; set; } = 0;

    public TextureElement(string path)
    {
        var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        var textureInfo = new FileInfo(path);
     
        icon = texture;
        name = Path.GetFileNameWithoutExtension(path);
        extension = Path.GetExtension(path);
        bytes = textureInfo.Length;
    }
}