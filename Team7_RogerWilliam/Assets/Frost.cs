using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frost : MonoBehaviour
{
    [SerializeField] private float frostDuration, frostMultiplier;
    [SerializeField] private Color frostColor;

    void Start()
    {
        LoadoutManager.Instance.postWeaponUpdated.AddListener(Reset);
        Reset(null);
    }

    void Reset(LoadoutOption weapon) {
        Player.Instance.GetComponentInChildren<WeaponHandler>().GetWeapon().onEnemyHit.AddListener(EnemyHit);
    }

    void EnemyHit(Health enemyHealth) {
        StartCoroutine(ApplyFrost(enemyHealth.GetComponent<AgentMover>()));
        Debug.Log("Enemy hit frost applied");
    }

    private IEnumerator ApplyFrost(AgentMover mover) {
        if (!mover.frosted) {
            SpriteRenderer renderer = mover.GetComponentInChildren<SpriteRenderer>();
            Color ogColor = renderer.color;
            renderer.color = frostColor;

            mover.frosted = true;
            mover.SetSlow(frostMultiplier);
            yield return new WaitForSeconds(frostDuration);

            if (!mover) {
                yield break;
            }

            mover.RemoveSlow();
            mover.frosted = false;

            renderer.color = ogColor;
        }
    }
}
