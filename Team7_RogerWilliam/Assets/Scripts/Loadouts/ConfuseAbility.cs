using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ConfuseAbility : Ability{

    [SerializeField] private float attackRange;
    [SerializeField] private GameObject musicProjectile;
    [SerializeField] private float confuseDuration = 5f;
    [SerializeField] private float speed = 4f;
    [SerializeField] public LayerMask enemyLayers;
  


    protected override void OnActivate() {
        Vector3 dist;
        if(targetTag == "Enemy")
        {
            mover.FaceTowardsMouse(transform.position);
            dist = Util.DistToMouse(transform.position);
            if (dist.magnitude > attackRange) {
                dist = dist.normalized * attackRange;
            }

            anim.PlayOnce("Player_Punch");
        } else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 direction = (player.transform.position - transform.position).normalized;
            // Bossmover.FaceTowards(direction);
            dist = player.transform.position - transform.position;
            if (dist.magnitude > attackRange) {
                dist = dist.normalized * attackRange;
            }
        }
        Vector3 target = transform.position + dist;
        Quaternion rotation;
        if(targetTag == "Enemy")
        {
            rotation = Util.QuaternionTowardsMouse(transform.position);
        } else {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            rotation = Util.QuaternionOfVector3(directionToPlayer, 0f);
        }
        Vector3 initialPos = transform.position + dist.normalized * 0.5f;
        GameObject proj = Instantiate(musicProjectile, initialPos, rotation);
        TargetProjectile targetProj = proj.GetComponent<TargetProjectile>();
        targetProj.SetTarget(target);
        targetProj.SetSpeed(speed);

        targetProj.onHit.AddListener(ProjHit);
    }

    private void ProjHit(GameObject hitEnemy) {
        if(targetTag == "Enemy")
        {
            EnemyChase confusedEnemy = hitEnemy.GetComponent<EnemyChase>();
            if (confusedEnemy != null) {
                confusedEnemy.Confuse(confuseDuration); 
            }
        } else
        {
            if(hitEnemy.CompareTag("Player"))
            {
                Debug.Log("IN HERE");
                AgentMover confusedPlayer = hitEnemy.GetComponent<AgentMover>();
                confusedPlayer.ConfusePlayer(confuseDuration);
            }
        }
    }

    
}