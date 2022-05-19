using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 velocity = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-0.2f, 5.0f), 0.0f);
        GetComponent<Rigidbody2D>().velocity = velocity;
        GetComponent<Rigidbody2D>().angularVelocity = Mathf.Pow(-1, Random.Range(0, 2)) * Random.Range(250.0f, 480.0f);
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
