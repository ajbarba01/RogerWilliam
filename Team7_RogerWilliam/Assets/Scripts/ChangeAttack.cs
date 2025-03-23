using UnityEngine;

public class ChangeAttack : MonoBehaviour
{
    public MonoBehaviour attackMode1; // Assign the first attack script in the Inspector
    public MonoBehaviour attackMode2; // Assign the second attack script in the Inspector

    void Start()
    {
        // Ensure only one mode is active at the start
        if (attackMode1 != null && attackMode2 != null)
        {
            attackMode1.enabled = true;
            attackMode2.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchAttackMode(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchAttackMode(false);
        }
    }

    void SwitchAttackMode(bool activateFirst)
    {
        if (attackMode1 != null && attackMode2 != null)
        {
            attackMode1.enabled = activateFirst;
            attackMode2.enabled = !activateFirst;
        }
    }
}
