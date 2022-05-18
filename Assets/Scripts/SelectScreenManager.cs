using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;

public class SelectScreenManager : MonoBehaviour
{
    [HideInInspector]
    public VisualElement root;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");
        root = objs[0].GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.Flex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
