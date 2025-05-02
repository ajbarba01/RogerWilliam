using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frost : Passive
{
    private struct FrostedEnemy {
        public float start;
        public Color ogColor;

        public FrostedEnemy(float start, Color color) {
            this.start = start;
            this.ogColor = color;
        }
    };

    [SerializeField] private float frostDuration, frostMultiplier;
    [SerializeField] private Color frostColor;

    private Dictionary<AgentMover, FrostedEnemy> frostedEnemies = new Dictionary<AgentMover, FrostedEnemy>();

    void Start()
    {
        LoadoutManager.Instance.postWeaponUpdated.AddListener(Reset);
        Reset(null);
    }

    void Reset(LoadoutOption weapon) {
        Player.Instance.GetComponentInChildren<WeaponHandler>().GetWeapon().onEnemyHit.AddListener(EnemyHit);
    }

    void EnemyHit(Health enemyHealth) {
        AgentMover mover = enemyHealth.GetComponent<AgentMover>();
        if (frostedEnemies.ContainsKey(mover)) {
            frostedEnemies[mover] = new FrostedEnemy(Time.time, frostedEnemies[mover].ogColor);
        }
        else {
            StartCoroutine(ApplyFrost(mover));
        }
    }

    private IEnumerator ApplyFrost(AgentMover mover) {

        SpriteRenderer renderer = mover.GetComponentInChildren<SpriteRenderer>();
        Color ogColor = renderer.color;
        renderer.color = frostColor;

        frostedEnemies[mover] =  new FrostedEnemy(Time.time, ogColor);

        mover.SetSlow(frostMultiplier);

        while (frostedEnemies[mover].start + frostDuration > Time.time) {
            yield return new WaitForSeconds(Globals.TickRate);
        }

        mover.RemoveSlow();
        renderer.color = ogColor;
        
        frostedEnemies.Remove(mover);
    }

    private void OnDestroy() {
        Player.Instance.GetComponentInChildren<WeaponHandler>().GetWeapon().onEnemyHit.RemoveListener(EnemyHit);

        foreach (KeyValuePair<AgentMover, FrostedEnemy> entry in frostedEnemies) {
            entry.Key.RemoveSlow();
            entry.Key.GetComponentInChildren<SpriteRenderer>().color = entry.Value.ogColor;
        }
    }
}
