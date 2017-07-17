using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class TextureTreeViewWindow : EditorWindow
{
    private const string searchStringStateKey = "TextureTreeViewWindow_SearchString";
    
    [MenuItem("TextureTreeView/TextureTreeView")]
    static void Open()
    {
        GetWindow<TextureTreeViewWindow>();
    }

    private SearchField searchField;
    private TextureTreeView textureTreeView;

    void OnEnable()
    {
        var state = new TreeViewState();
        var header = new TextureTreeViewHeader(null);
        textureTreeView = new TextureTreeView(state, header);
        textureTreeView.searchString = SessionState.GetString(searchStringStateKey, "");
        searchField = new SearchField();
        searchField.downOrUpArrowKeyPressed += textureTreeView.SetFocusAndEnsureSelectedItem;
    }

    void OnGUI()
    {
        
        EditorGUI.BeginChangeCheck();
        var searchString = searchField.OnGUI(textureTreeView.searchString);

        if (EditorGUI.EndChangeCheck())
        {
            SessionState.SetString(searchStringStateKey, searchString);
            textureTreeView.searchString = searchString;
        }
        
        textureTreeView?.OnGUI(new Rect(0, 20, position.width, position.height - 20));
    }
}