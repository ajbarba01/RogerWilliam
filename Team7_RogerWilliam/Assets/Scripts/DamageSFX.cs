using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSFX : MonoBehaviour
{

    [SerializeField] private AudioSource DamageSource;
    [SerializeField] private AudioClip DamageSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDamage() {
        DamageSource.clip = DamageSound;
        DamageSource.Play();
    }
}
