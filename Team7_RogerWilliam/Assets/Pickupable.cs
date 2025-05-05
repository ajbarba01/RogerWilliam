using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Pickupable : MonoBehaviour
{
    [SerializeField] private string pickupLayerString;
    [SerializeField] private GameObject pickupVFX;
    public UnityEvent onPickup;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.tag);
        if (other.CompareTag(pickupLayerString)) {
            onPickup.Invoke();
            OnPickup();
        }
    }

    private void OnPickup() {
        if (pickupVFX != null) {
            Instantiate(pickupVFX, transform.position, Quaternion.identity);
        }

        Pickup();
        Destroy(gameObject);
    }

    protected virtual void Pickup() { }
}
