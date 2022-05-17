using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Level> listLevel;
    public int numLevel;

    // Start is called before the first frame update
    void Start()
    {
        listLevel = new List<Level>();
        for (int i = 0; i < numLevel; i++)
        {
            listLevel.Add(new Level());
            Debug.Log("Level " + i + " created");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
