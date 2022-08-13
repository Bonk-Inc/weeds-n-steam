using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSmokeDanger : TileDanger
{
    private PlayerHealth playerHealth;

    [SerializeField]
    private SpriteRenderer rend;
    [SerializeField]
    private int damage;

    public override void ToggleDanger(bool safe)
    {
        safetyOn = safe;
        rend.enabled = !safe;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (safetyOn || !other.CompareTag("Player")) return;
        if(playerHealth == null) playerHealth = other.GetComponent<PlayerHealth>();
        playerHealth.Damage(damage);
    }
}
