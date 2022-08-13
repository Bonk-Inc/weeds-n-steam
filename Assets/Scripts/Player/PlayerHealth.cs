using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 3, initialHealth = 1;
    private int currentHealth = 1;

    public int CurrentHealth => currentHealth;

    public event Action<int> OnHealthChange;
    public event Action OnDeath; // F

    private void Awake() {
        currentHealth = initialHealth;
    }

    public void Heal(int amount) {
        amount = Math.Max(0, Math.Min(amount, currentHealth - maxHealth));
        ChangeHealth(amount);
    }

    public void Damage(int amount) {
        ChangeHealth(-amount);
        if(currentHealth <= 0) OnDeath?.Invoke();
    }

    private void ChangeHealth(int amount) {
        currentHealth += amount;
        OnHealthChange?.Invoke(currentHealth);
    }
}
