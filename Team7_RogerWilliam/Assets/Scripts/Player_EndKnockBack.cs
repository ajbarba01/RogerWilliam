using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_EndKnockBack: MonoBehaviour
{
    private Rigidbody2D playerRB;

       public void EndKnockBack(){
              Player playerScript = gameObject.GetComponent<Player>();
              // playerScript.isKnockbackActive = false;
              playerRB = gameObject.GetComponent<Rigidbody2D>();
              StartCoroutine(StopKnockBack(playerRB));
       }

       IEnumerator StopKnockBack(Rigidbody2D myRB){
              yield return new WaitForSeconds(0.2f);
              Player playerScript = gameObject.GetComponent<Player>();
              // playerScript.isKnockbackActive = false;
              // myRB.velocity= new Vector3(0,0,0);
       }
}
