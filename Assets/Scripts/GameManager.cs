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
        // RenderSelectScreen();
        root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement selectLevel = root.Q<VisualElement>("selectLevel");
        btnReturn = root.Q<Button>("btnReturn");
        btnReturn.clicked += OnReturnClicked;


        listLevel = new List<Level>();
        for (int i = 0; i < numLevel; ++i)
            listLevel.Add(new Level(i + 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLevelClicked(int index) {
        Debug.Log(index);
        SceneManager.LoadScene("Level" + index);
        GetComponent<UIDocument>().enabled = false;
        // root.style.display = DisplayStyle.None;
    }

    void OnReturnClicked() {
        SceneManager.LoadScene("MenuStart");
    }
    
    public void RenderSelectScreen() {
        root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement selectLevel = root.Q<VisualElement>("selectLevel");


        // listLevel = new List<Level>();
        for (int i = 0; i < numLevel; ++i)
        {
            // listLevel.Add(new Level(i + 1));
            selectLevel.Add(templateButton.Instantiate());
            int x = i + 1;
            selectLevel[i].Q<VisualElement>("Lock").style.display = (listLevel[i].Unlocked) ? DisplayStyle.None : DisplayStyle.Flex;
            Button btn = selectLevel[i].Q<Button>("Button");
            
            if (listLevel[i].Unlocked) {
                btn.clicked += delegate{OnLevelClicked(x);};
            }

            if (i < numLevel - 1 && listLevel[i].Defeated)
                listLevel[i + 1].Unlocked = true;
                
            btn.Q<Label>("lbLevel").text = "Level " + (i + 1);
            btn.Q<Label>("lbHighScore").text = listLevel[i].HighScore.ToString();
        }
    }
}
