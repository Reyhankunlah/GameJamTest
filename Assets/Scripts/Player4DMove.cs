using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player4DMove : MonoBehaviour
{

    public Animator animator;
    public float moveSpeed;
    public Rigidbody2D rb;

    public float x;
    public float y;

    public Vector2 input;
    public bool moving;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
    }

    private void GetInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        input = new Vector2(x, y);
        input.Normalize();
    }

    private void Animate()
    {
        if (input.magnitude > 0.1f || input.magnitude < -0.1f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (moving)
        {
            animator.SetFloat("X", x);
            animator.SetFloat("Y", y);
        }

        animator.SetBool("Moving", moving);
    }
}