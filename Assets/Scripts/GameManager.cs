using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public VisualElement root;
    public List<Level> listLevel;
    public int numLevel;

    public VisualTreeAsset templateButton;
    public Button btnReturn;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement selectLevel = root.Q<VisualElement>("selectLevel");
        btnReturn = root.Q<Button>("btnReturn");
        btnReturn.clicked += OnReturnClicked;


        listLevel = new List<Level>();
        for (int i = 0; i < numLevel; i++)
        {
            listLevel.Add(new Level());
            selectLevel.Add(templateButton.Instantiate());
            selectLevel[i].Q<Button>("Button").clicked += delegate{OnLevelClicked(i);};
            // selectLevel[i].Q<Button>("Button").clicked += OnLevelClicked;
            selectLevel[i].Q<Button>("Button").text = "Level " + (i + 1);
            Debug.Log("Level " + i + " created");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLevelClicked(int index) {
        SceneManager.LoadScene("Level" + index);
    }

    void OnReturnClicked() {
        SceneManager.LoadScene("MenuStart");
    }
}
