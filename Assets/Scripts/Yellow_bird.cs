using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow_bird : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isActiveSkill = false;
    [HideInInspector]
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (GetComponent<Bird>().State == Bird.BirdState.Thrown && Input.GetMouseButton(0) && !isActiveSkill)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * 2;
            animator.SetBool("Active", true);
            isActiveSkill = true;
        }
    }
}
