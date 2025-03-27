using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private LastHitEnemy lastHit;

    // Start is called before the first frame update
    void Start()
    {
        if (currentWeapon != null) {
            SetWeapon(currentWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Attack") > 0){
            currentWeapon.Attack();
        }
    }

    void SetWeapon(Weapon weapon) {
        if (currentWeapon != null) {
            weapon.onEnemyHit.RemoveListener(lastHit.EnemyHit);
        }
        currentWeapon = weapon;
        weapon.onEnemyHit.AddListener(lastHit.EnemyHit);
    }
}
