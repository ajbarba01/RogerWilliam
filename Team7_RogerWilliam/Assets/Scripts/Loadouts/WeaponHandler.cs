using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponHandler : MonoBehaviour
{
    private Weapon currentWeapon;
    [SerializeField] private LastHitEnemy lastHit;
    [SerializeField] private AnimationManager playerAnim;
    [SerializeField] private AgentMover mover;


    public UnityEvent onAttack;

    private bool paused;

    void Awake()
    {
        // Debug.Log("WEAPON HANDLER LISTENING");

        LoadoutManager.Instance.weaponUpdated.AddListener(SetWeapon);
    }

    void Update()
    {
        if (Pause.isPaused || paused) return;

        if (currentWeapon != null && Input.GetMouseButton(0) && !currentWeapon.OnCooldown()){
            currentWeapon.Attack();
            onAttack.Invoke();
        }
    }

    void SetWeapon(LoadoutOption weapon) {
        if (weapon == null || weapon.GetPrefab() == null) {
            return;
        }

        GameObject weaponPrefab = weapon.GetPrefab();

        RemoveWeapon();

        GameObject newWeapon = Instantiate(weaponPrefab, transform);
        
        currentWeapon = newWeapon.GetComponent<Weapon>();
        currentWeapon.onEnemyHit.AddListener(lastHit.EnemyHit);
        currentWeapon.Initialize(playerAnim, mover);
    }

    public Weapon GetWeapon() {
        return currentWeapon;
    }

    public void Interrupt() {
        currentWeapon.OnInterrupt();
    }

    void RemoveWeapon() {
        if (currentWeapon != null) {
            currentWeapon.onEnemyHit.RemoveListener(lastHit.EnemyHit);
            Destroy(currentWeapon.gameObject);
        }

    }

    public void SetPause(bool pause) {
        paused = pause;
    }
}
