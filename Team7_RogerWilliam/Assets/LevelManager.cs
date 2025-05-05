using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private static bool started;

    public int[] levels;
    public Door[] doors;
    public static int currentLevel = 0;
    [SerializeField] private int startLevel;

    private int numUnlocks = 6;

    public LoadoutOption[] weapons;
    public LoadoutOption[] actives;
    public LoadoutOption[] passives;

    private void Awake() {
        Instance = this;
        // DontDestroyOnLoad(this);
        // currentLevel = 0;

        if (started) {
            return;
        }
        
        LoadoutManager.Instance.UnlockWeapon(weapons[0]);
        for (int i = 1; i <= startLevel; i++) {
            UnlockLevelInst(i);
        }
    }

    public void UnlockLevelInst(int level) {
        Debug.Log("UNLOCKING");
        if (level == currentLevel + 1) {
            currentLevel++;
            if (currentLevel > numUnlocks) {
                return;
            }
            if (currentLevel >= 2) {
                LoadoutManager.Instance.UnlockWeapon(weapons[currentLevel - 2]);
                LoadoutManager.Instance.UnlockAbility(actives[currentLevel - 2]);
                LoadoutManager.Instance.UnlockPassive(passives[currentLevel - 2]);
            }
        }
        Debug.Log(currentLevel);
    }

    public void UpdateDoors() {
        foreach (Door door in doors) {
            door.UpdateLock();
        }
    }

    public static void UnlockLevel(int level) {
        Instance.UnlockLevelInst(level);
    }

}
