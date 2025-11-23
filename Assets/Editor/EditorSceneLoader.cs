using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
public class EditorSceneLoader : EditorWindow
{
    static DirectoryInfo dir;

    [MenuItem("Custom Window/Editor Scene Loader")]
    public static void Initialize()
    {
        EditorWindow.GetWindow(typeof(EditorSceneLoader));
        var scenesPath = Path.Combine(Application.dataPath, "Scenes");
        dir = new DirectoryInfo(scenesPath);
    }

    private void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        Label label = new Label("Available Scenes");
        root.Add(label);

    }
    private void Update()
    {
        ListView listview = new ListView();

    }
}
