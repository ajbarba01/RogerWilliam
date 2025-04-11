using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCube_proj : MonoBehaviour
{
    protected GameObject gameHandler;
    protected GameObject player;
    public float damage = 15f;
    //public GameObject hitEffectAnim;  When there is an animation implemented
    public float lifetime = 5f;
    public float freezetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {   
            //taking damage
            gameHandler = GameObject.FindWithTag("GameController");
            if (gameHandler != null) {
                gameHandler.GetComponent<Health>().TakeDamage(damage);
            }
            if (player != null) {
                player.GetComponent<Player>().ApplyFreeze(freezetime);
            }

            Destroy(gameObject); //destroy after hitting
        }
    }
}
