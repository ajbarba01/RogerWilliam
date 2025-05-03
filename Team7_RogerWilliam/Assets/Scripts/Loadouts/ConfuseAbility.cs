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
        mover.FaceTowardsMouse(transform.position);

        Vector3 dist = Util.DistToMouse(transform.position);
        if (dist.magnitude > attackRange) {
            dist = dist.normalized * attackRange;
        }

        anim.PlayOnce("Player_Punch");

        Vector3 target = transform.position + dist;

        Vector3 initialPos = transform.position + dist.normalized * 0.5f;
        GameObject proj = Instantiate(musicProjectile, initialPos, Util.QuaternionTowardsMouse(transform.position));
        TargetProjectile targetProj = proj.GetComponent<TargetProjectile>();
        targetProj.SetTarget(target);
        targetProj.SetSpeed(speed);

        targetProj.onHit.AddListener(ProjHit);
    }

    private void ProjHit(GameObject hitEnemy) {

        EnemyChase confusedEnemy = hitEnemy.GetComponent<EnemyChase>();
        if (confusedEnemy != null) {
            confusedEnemy.Confuse(confuseDuration); 
        }
    }
}