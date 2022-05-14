using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float health;

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;
        float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
        health -= damage;
        if (health <= 0) Destroy(this.gameObject);
        Debug.Log(damage);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
