using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Health : MonoBehaviour {

    //Animator anim;
    [SerializeField] protected float maxHealth;

    public static DamageSFX damageSFX;

    public UnityEvent beforeDeath;

    // Should only be used to destroy object
    public UnityEvent onDeath;
    public UnityEvent onDamage;
    public UnityEvent<float> tookDamage;

    protected float health;

    void Start(){
        //anim = GetComponentInChildren<Animator>();\
        damageSFX = GetComponentInChildren<DamageSFX>();
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
        onDamage.Invoke();
        tookDamage.Invoke(damage);
        
        SetHealth(health - damage);

        if (damageSFX != null) {
            damageSFX.PlayDamage();
        }
        //anim.SetTrigger("GetHit");
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
        beforeDeath.Invoke();
        onDeath.Invoke();
    }
}
