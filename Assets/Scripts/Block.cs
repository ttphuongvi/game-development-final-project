using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float maxHeath;
    [HideInInspector]
    public float health;
    public Sprite[] spriteArray;

    public void changeSprite(int index)
    {
        GetComponent<SpriteRenderer>().sprite = spriteArray[index];
    }

    void OnCollisionEnter2D(Collision2D col) {

        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        // Tính damage, detroy nếu hết máu
        float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
        health -= damage;
        if (health <= 0) Destroy(this.gameObject);
        Debug.Log(damage);

        // Check máu chuyển đổi Sprite
        if (health >= maxHeath * 0.75f)
        {
            changeSprite(0);
        }
        else if (health >= maxHeath * 0.5f)
        {
            changeSprite(1);
        }
        else if (health >= maxHeath * 0.25f)
        {
            changeSprite(2);
        }
        else
        {
            changeSprite(3);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
