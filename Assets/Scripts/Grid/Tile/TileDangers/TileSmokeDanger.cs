using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSmokeDanger : TileDanger
{
    private PlayerHealth playerHealth;

    [SerializeField]
    private SpriteRenderer rend;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private int damage = 1;

    private const string tileSafety = "SmokeOut", tileDanger = "SmokIn";

    public override void ToggleDanger(bool safe)
    {
        if(!safe) {
            rend.enabled = true;
            animator.SetTrigger(tileDanger);
        }
        else animator.SetTrigger(tileSafety);
    }

    public void Dissapear() {        
        rend.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (safety.Safe || !other.CompareTag("Player")) return;
        if(playerHealth == null) playerHealth = other.GetComponent<PlayerHealth>();
        playerHealth.Damage(damage);
    }
}
