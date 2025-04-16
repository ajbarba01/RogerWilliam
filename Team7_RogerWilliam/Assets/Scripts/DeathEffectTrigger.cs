using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DeathEffectTrigger : MonoBehaviour
{
    [SerializeField] private GameObject art;
    [SerializeField] private GameObject deathEffect;

    private void Awake() {
        GetComponent<Health>().beforeDeath.AddListener(BeforeDeath);
    }

    private void BeforeDeath() {
        GameObject death = Instantiate(deathEffect, art.transform.position, art.transform.rotation);
        death.transform.localScale = art.transform.localScale;
        death.GetComponent<DeathEffect>().Run(art.GetComponent<SpriteRenderer>().sprite);
    }
}
