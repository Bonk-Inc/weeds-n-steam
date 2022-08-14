using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSmokeDanger : TileDanger
{
    private PlayerHealth playerHealth;

    [SerializeField]
    private SpriteRenderer rend;
    [SerializeField]
    private int damage = 1;

    public override void ToggleDanger(bool safe)
    {
        rend.enabled = !safe;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (safety.Safe || !other.CompareTag("Player")) return;
        if(playerHealth == null) playerHealth = other.GetComponent<PlayerHealth>();
        playerHealth.Damage(damage);
    }
}
