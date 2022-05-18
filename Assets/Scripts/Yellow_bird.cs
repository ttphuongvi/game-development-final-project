using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow_bird : MonoBehaviour
{
    // Start is called before the first frame update
    private bool flag;
    void Start()
    {
        flag = true;
    }

    void Update()
    { 
        if(GetComponent<Bird>().State == Bird.BirdState.Thrown && Input.GetMouseButton(0) && flag)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * 2;
            flag = false;
        }
    }
}
