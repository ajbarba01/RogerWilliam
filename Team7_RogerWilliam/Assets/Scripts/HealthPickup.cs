using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public GameObject gameHandlerObj;
       public int damage = 10;
       //public Transform backToStart; //uncomment this line for "auto-death," to zap the Player back to start

       void Start(){
            if (GameObject.FindWithTag("GameController") != null){
               gameHandlerObj = GameObject.FindWithTag("GameController");
            }
       }

       public void OnCollisionEnter2D(Collision2D other) {
              if (other.gameObject.tag == "Player") {
                    {
                        gameHandlerObj.GetComponent<Health>().Heal(damage);
                    }
              }
              Destroy(gameObject);
       }
}
