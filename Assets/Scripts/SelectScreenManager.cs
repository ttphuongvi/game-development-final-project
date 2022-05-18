using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;

public class SelectScreenManager : MonoBehaviour
{
    [HideInInspector]
    public VisualElement root;
    [HideInInspector]
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        // GameObject[] objs = ;
        gameManager = GameObject.FindGameObjectsWithTag("GameController")[0];
        gameManager.GetComponent<UIDocument>().enabled = true;
        gameManager.GetComponent<GameManager>().RenderSelectScreen();
        
        
        // root = objs[0].GetComponent<UIDocument>().rootVisualElement;
        // root.style.display = DisplayStyle.Flex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
