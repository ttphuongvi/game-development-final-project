using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float maxHeath = 100;
    
    [ShowOnly] public float health;

    [HideInInspector]
    public Animator animator;
    public enum PigState
    {
        Idle,
        Injured,
        SeriousInjured,
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 100;
        health -= damage;
        if (health <= 0) {
            // animator.SetInteger("State", 2);
            this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(this.gameObject, 2f);
        }

        if (health <= maxHeath * 0.3f) {
            animator.SetInteger("State", 2);
        }
        else if (health <= maxHeath * 0.6f) {
            animator.SetInteger("State", 1);
        } else {
            animator.SetInteger("State", 0);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        health = maxHeath;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
