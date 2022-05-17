using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Btn_play : EditorWindow
{
    [MenuItem("Window/UI Toolkit/Btn_play")]
    public static void ShowExample()
    {
        // Btn_play wnd = GetWindow<Btn_play>();
        var wnd = GetWindow<Btn_play>();
        wnd.titleContent = new GUIContent("Btn_play");
    }

    public void CreateGUI()
    {
    //     // Each editor window contains a root VisualElement object
    //     VisualElement root = rootVisualElement;

    //     // VisualElements objects can contain other VisualElement following a tree hierarchy.
    //     VisualElement label = new Label("Hello World! From C#");
    //     root.Add(label);

    //     // Import UXML
    //     var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scenes/Btn_play.uxml");
    //     VisualElement labelFromUXML = visualTree.Instantiate();
    //     root.Add(labelFromUXML);
    // Create a few different colored boxes
        for (int i = 0; i < 4; i++)
        {
        // Create VisualElement with random background color
        var newBox = new VisualElement() { style = { flexGrow = 1, backgroundColor = GetRandomColor() } };
        rootVisualElement.Add(newBox);

        // Register a click event to the visual element to change the background color to a new color
        newBox.RegisterCallback<ClickEvent>(OnBoxClicked);
        }
    }
    private void OnBoxClicked(ClickEvent evt)
    {
        // Only perform this action at the target, not in a parent
        if (evt.propagationPhase != PropagationPhase.AtTarget)
        return;

        // Assign a random new color
        var targetBox = evt.target as VisualElement;
        targetBox.style.backgroundColor = GetRandomColor();
    }
    private Color GetRandomColor()
    {
        return new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }
}