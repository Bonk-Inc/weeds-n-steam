using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerWaling : MonoBehaviour
{

    
    [SerializeField]
    private float speed;

    private Rigidbody2D rb;
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        var direction = Vector2.zero;
        if(Input.GetKey(KeyCode.W)){
            direction += Vector2.up;
        }
        else if(Input.GetKey(KeyCode.S)){
            direction += Vector2.down;
        }

        if(Input.GetKey(KeyCode.A)){
            direction += Vector2.left;
        }
        else if(Input.GetKey(KeyCode.D)){
            direction += Vector2.right;
        }
        direction.Normalize();

        rb.velocity = direction * speed;
    }

}
