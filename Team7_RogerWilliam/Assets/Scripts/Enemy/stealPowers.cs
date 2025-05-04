using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stealPowers : MonoBehaviour
{
    public LoadoutManager bossLoadout;
    // Start is called before the first frame update
    void Start()
    {
        setBossLoadoutFromPlayer();
    }

    // Update is called once per frame
    void setBossLoadoutFromPlayer()
    {
        bossLoadout.SetWeapon(LoadoutManager.Instance.currentWeapon);
        bossLoadout.SetAbility(LoadoutManager.Instance.currentAbility);
        // if(bossLoadout.GetAbility().name == "Roll(Clone)")
        // {
        //     bossLoadout.SetAbility()
        // }
        bossLoadout.SetPassive(LoadoutManager.Instance.currentPassive);
    }
}
