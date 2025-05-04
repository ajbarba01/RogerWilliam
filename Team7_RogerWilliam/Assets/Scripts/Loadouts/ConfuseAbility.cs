using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ConfuseAbility : Ability{

    [SerializeField] private float attackRange;
    [SerializeField] private GameObject musicProjectile;
    [SerializeField] private float confuseDuration = 5f;
    [SerializeField] private float splashArea = 1.5f;
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

    private void ProjHit(GameObject projectile) {

        if(targetTag == "Enemy") {
            Debug.Log("Confused Enemy");
            Collider2D[] hits = Physics2D.OverlapCircleAll(projectile.transform.position, splashArea, enemyLayers);

                foreach (var enemy in hits) {
                    EnemyChase confusedEnemy = enemy.GetComponent<EnemyChase>();
                    if (confusedEnemy != null) {
                        confusedEnemy.Confuse(confuseDuration); 
                    }
                }
        } 
        else {
            Collider2D[] hits = Physics2D.OverlapCircleAll(projectile.transform.position, splashArea, enemyLayers);

                foreach (var player in hits) {
                    if (player.CompareTag("Player")) {
                        Debug.Log("Confused Player");
                        AgentMover confusedPlayer = player.GetComponent<AgentMover>();
                        confusedPlayer.ConfusePlayer(confuseDuration);
                    }
                }
        }
    }
}