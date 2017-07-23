using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class TextureTableWindow : EditorWindow
{
    private const string searchStringStateKey = "TextureTreeViewWindow_SearchString";

    [MenuItem("UNIBOOK8/TextureTreeView")]
    static void Open()
    {
        GetWindow<TextureTableWindow>();
    }

    private SearchField searchField;
    private TextureTreeView textureTreeView;

    void OnEnable()
    {
        var state = new TreeViewState();
        var header = new TextureTableHeader(null);
        textureTreeView = new TextureTreeView(state, header);
        textureTreeView.searchString = SessionState.GetString(searchStringStateKey, "");
        searchField = new SearchField();
        searchField.downOrUpArrowKeyPressed += textureTreeView.SetFocusAndEnsureSelectedItem;
    }

    void OnGUI()
    {
        using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
        {
            if (GUILayout.Button("Reload", EditorStyles.toolbarButton, GUILayout.Width(100)))
            {
                textureTreeView.Reload();
            }

            using (var checkScope = new EditorGUI.ChangeCheckScope())
            {
                var searchString = searchField.OnToolbarGUI(textureTreeView.searchString);

                if (checkScope.changed)
                {
                    SessionState.SetString(searchStringStateKey, searchString);
                    textureTreeView.searchString = searchString;
                }
            }
        }


        textureTreeView?.OnGUI(new Rect(0, EditorGUIUtility.singleLineHeight + 1, position.width,
            position.height - EditorGUIUtility.singleLineHeight - 1));
    }
}