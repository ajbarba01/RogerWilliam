using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int[] levels;
    public Door[] doors;
    public int currentLevel = 1;
    [SerializeField] private int startLevel;

    private int numUnlocks = 5;

    public LoadoutOption[] weapons;
    public LoadoutOption[] actives;
    public LoadoutOption[] passives;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
            currentLevel = 0;
            LoadoutManager.Instance.UnlockWeapon(weapons[0]);
            for (int i = 1; i <= startLevel; i++) {
                UnlockLevelInst(i);
            }
        }
        else {
            Destroy(gameObject);
        }
    }

    public void UnlockLevelInst(int level) {
        if (level == currentLevel + 1) {
            currentLevel++;
            if (currentLevel > numUnlocks) {
                return;
            }
            LoadoutManager.Instance.UnlockWeapon(weapons[currentLevel - 1]);
            LoadoutManager.Instance.UnlockAbility(actives[currentLevel - 1]);
            LoadoutManager.Instance.UnlockPassive(passives[currentLevel - 1]);
        }
        Debug.Log(Instance.currentLevel);
    }

    public static void UnlockLevel(int level) {
        Instance.UnlockLevelInst(level);
    }
}
