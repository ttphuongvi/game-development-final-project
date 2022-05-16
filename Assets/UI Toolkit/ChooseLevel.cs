using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class ChooseLevel : EditorWindow
{
    [MenuItem("Window/UI Toolkit/ChooseLevel")]
    public static void ShowExample()
    {
        ChooseLevel wnd = GetWindow<ChooseLevel>();
        wnd.titleContent = new GUIContent("ChooseLevel");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scenes/ChooseLevel.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);
    }
}