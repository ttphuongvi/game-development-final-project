using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public enum BirdState
    {
        BeforeThrown,
        Thrown
    }

    public BirdState State
    {
        get;
        private set;
    }

    // bird can move
    public bool canMove = false;
    public Vector2 limitMove;
    public float MinVelocity = 0.05f;

    [HideInInspector]
    public Animator animator;


    void Start()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
        State = BirdState.BeforeThrown;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (State == BirdState.Thrown && GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= MinVelocity)
        {
            //destroy the bird after 2 seconds
            // StartCoroutine(DestroyAfter(2));
        }
    }

    public void Thrown()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        animator.SetInteger("State", 1);
        State = BirdState.Thrown;
    }

    IEnumerator DestroyAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
