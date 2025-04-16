using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballProjectile : MonoBehaviour
{
    // Start is called before the first frame update

       public float attackDamage = 5f;
       public float speed = 10f;
       private Transform playerTrans;
       private Vector2 target;
       public GameObject hitEffectAnim;
       public float SelfDestructTime = 5.0f;
       private Rigidbody2D rb;
       
       void Start() {
             //NOTE: transform gets location, but we need Vector2 for direction, so we can use MoveTowards.
             playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
             target = new Vector2(playerTrans.position.x, playerTrans.position.y);
              rb = GetComponent<Rigidbody2D>();

             StartCoroutine(selfDestruct());

             //code for trajectory (moves target way beyond player position):
             Vector2 startPos = transform.position;
             float distance = Vector2.Distance(startPos, target);
             distance = distance * (10f);
             Vector2 difference = target - startPos;
             difference = difference.normalized * distance;
             target = (startPos + difference);

             transform.rotation = Util.QuaternionOfVector3(Player.GetPosition() - transform.position);
       }

       void FixedUpdate() {
              rb.MovePosition(Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime));
       }


       //if the bullet hits a collider, play the explosion animation, then destroy the effect and the bullet
       void OnTriggerEnter2D(Collider2D collision){
              if (collision.gameObject.tag == "Player") {
                     Debug.Log("Hit!");
                     Player.health.TakeDamage(attackDamage);
                     GameObject animEffect = Instantiate (hitEffectAnim, transform.position, Quaternion.identity);
                     Destroy (animEffect, 0.5f);
                     Destroy (gameObject);
              }
              if (collision.gameObject.tag != "Enemy") {
                     GameObject animEffect = Instantiate (hitEffectAnim, transform.position, Quaternion.identity);
                     Destroy (animEffect, 0.5f);
                     Destroy (gameObject);
              }
       }

       IEnumerator selfDestruct(){
              yield return new WaitForSeconds(SelfDestructTime);
              Destroy (gameObject);
       }
}
