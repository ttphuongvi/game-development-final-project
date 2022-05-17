using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class UIIngame : EditorWindow
{
    [MenuItem("Window/UI Toolkit/UIIngame")]
    public static void ShowExample()
    {
        Debug.Log("Hello");
        UIIngame wnd = GetWindow<UIIngame>();
        wnd.titleContent = new GUIContent("UIIngame");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI Toolkit/UIIngame.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);
    }
}