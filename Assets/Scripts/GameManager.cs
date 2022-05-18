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
        for (int i = 0; i < numLevel; ++i)
        {
            listLevel.Add(new Level(i + 1));
            selectLevel.Add(templateButton.Instantiate());
            int x = i + 1;
            selectLevel[i].Q<VisualElement>("Lock").style.display = (listLevel[i].Unlocked) ? DisplayStyle.None : DisplayStyle.Flex;
            selectLevel[i].Q<Button>("Button").clicked += delegate{OnLevelClicked(x);};
            selectLevel[i].Q<Button>("Button").Q<Label>("lbLevel").text = "Level " + (i + 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLevelClicked(int index) {
        Debug.Log(index);
        SceneManager.LoadScene("Level" + index);
        root.style.display = DisplayStyle.None;
    }

    void OnReturnClicked() {
        SceneManager.LoadScene("MenuStart");
    }
}
