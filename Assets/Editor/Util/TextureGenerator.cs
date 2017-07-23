using System.IO;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class TextureGenerator
{
    [MenuItem("UNIBOOK8/Util/RandomGenerator")]
    public static void Run()
    {
        var directoryPath = "Assets/Textures";
        Directory.CreateDirectory(directoryPath);
        RandomGenerator(directoryPath, 5000);
    }

    public static void RandomGenerator(string directoryPath, int num)
    {
        for (var i = 0; i < num; i++)
        {
            Generate(directoryPath, GUID.Generate().ToString(), Random.ColorHSV());
        }
        
        AssetDatabase.Refresh();
    }

    private static void Generate(string directoryPath, string name, Color32 color)
    {
        var texture = new Texture2D(1, 1);
        texture.SetPixels32(new[] {color});
        texture.Apply();

        string extension;
        var bytes = new byte[0];
        if (Random.value > 0.5f)
        {
            extension = ".jpg";
            bytes = texture.EncodeToJPG();
        }
        else
        {
            extension = ".png";
            bytes = texture.EncodeToPNG();
        }


        File.WriteAllBytes(Path.Combine(directoryPath, name + extension), bytes);
    }
}