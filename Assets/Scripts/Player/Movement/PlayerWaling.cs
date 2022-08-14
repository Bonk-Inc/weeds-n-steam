using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerWaling : MonoBehaviour
{


    [SerializeField]
    private float speed;

    [SerializeField]
    private Animator animator;

    private Rigidbody2D rb;

    public Vector2 LastDirection { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
            transform.up = -Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
            transform.up = -Vector2.down;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            transform.up = -Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            transform.up = -Vector2.right;
        }
        direction.Normalize();
        LastDirection = direction;


        animator.SetBool("Walking", direction != Vector2.zero);

        rb.velocity = direction * speed;
    }

}
