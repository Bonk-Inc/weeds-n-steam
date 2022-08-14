using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDodge : MonoBehaviour
{

    [SerializeField]
    private PlayerWaling walk;

    [SerializeField]
    private float dodgeTime, dodgeSpeed, dodgeCooldown;

    [SerializeField]
    private Animator animator;

    private Rigidbody2D rb;
    private float cooldownTimeElapsed;

    private void Awake()
    {
        cooldownTimeElapsed = dodgeCooldown;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && walk.enabled && cooldownTimeElapsed > dodgeCooldown && walk.LastDirection != Vector2.zero)
        {
            StartCoroutine(Dodge());
        }
        cooldownTimeElapsed += Time.deltaTime;
    }

    private IEnumerator Dodge()
    {
        animator.SetBool("Dashing", true);
        walk.enabled = false;
        var time = 0f;
        while (time < dodgeTime)
        {
            rb.velocity = walk.LastDirection * dodgeSpeed;
            time += Time.deltaTime;
            yield return null;
        }
        cooldownTimeElapsed = 0;
        walk.enabled = true;
        animator.SetBool("Dashing", false);
    }
}
