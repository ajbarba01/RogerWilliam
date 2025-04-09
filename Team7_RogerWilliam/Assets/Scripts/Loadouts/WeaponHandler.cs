using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private Weapon currentWeapon;
    [SerializeField] private LastHitEnemy lastHit;

    void Start()
    {
        SetWeapon(LoadoutManager.Instance.currentWeapon.GetPrefab());
    }

    void Update()
    {
        if (currentWeapon != null && Input.GetMouseButtonDown(0)){
            currentWeapon.Attack();
        }
    }

    void SetWeapon(GameObject weaponPrefab) {
        if (weaponPrefab == null) {
            return;
        }

        RemoveWeapon();

        GameObject newWeapon = Instantiate(weaponPrefab, transform);
        
        currentWeapon = newWeapon.GetComponent<Weapon>();
        currentWeapon.onEnemyHit.AddListener(lastHit.EnemyHit);
    }

    void RemoveWeapon() {
        if (currentWeapon != null) {
            currentWeapon.onEnemyHit.RemoveListener(lastHit.EnemyHit);
            Destroy(currentWeapon);
        }

    }
}
