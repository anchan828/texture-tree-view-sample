using System.Collections.Generic;
using UnityEditor;


public class OldHierarchy : EditorWindow
{
    [MenuItem("UNIBOOK8/Old Hierarchy")]
    static void Open()
    {
        GetWindow<OldHierarchy>();
    }

    public Dictionary<int, bool> foldouts = new Dictionary<int, bool>();


    void OnEnable()
    {
        foldouts.Clear();
        foldouts.Add(1, true);
    }


    void OnGUI()
    {
        foldouts[1] = EditorGUILayout.Foldout(foldouts[1], "Parent");

        if (foldouts[1])
        {
            EditorGUI.indentLevel = 2;
            EditorGUILayout.LabelField("Child");
        }
    }
}