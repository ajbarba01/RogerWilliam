using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField] protected float maxHealth;

    public UnityEvent onDeath;

    protected float health;

    void Start(){
        ResetHealth();
    }

    public float GetHealth() {
        return health;
    }

    public float GetMaxHealth() {
        return maxHealth;
    }

    public float GetPercentage() {
        return health / maxHealth;
    }

    public virtual void SetHealth(float newHealth) {
        health = newHealth;
    }

    public void ResetHealth() {
        SetHealth(maxHealth);
    }

    public void TakeDamage(float damage) {
        SetHealth(health - damage);
        if (health <= 0) {
            SetHealth(0);
            Die();
        }
    }

    public void Heal(float value) {
        SetHealth(health + value);
        if (health > maxHealth) {
            ResetHealth();
        }
    }

    void Die() {
        onDeath.Invoke();
    }
}
