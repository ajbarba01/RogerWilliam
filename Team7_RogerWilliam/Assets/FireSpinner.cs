using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpinner : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            other.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
