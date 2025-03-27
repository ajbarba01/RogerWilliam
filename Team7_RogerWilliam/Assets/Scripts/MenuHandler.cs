using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private Menu[] menus;
    private Menu openMenu;

    void Start()
    {
        openMenu = null;

        foreach (Menu menu in menus) {
            menu.menu.SetActive(false);
        }
    }

    void Update()
    {
        foreach(Menu menu in menus) {
            if (Input.GetKeyDown(menu.accessKey)) {
                if (menu.isOpen) {
                    CloseMenu();
                }
                else {
                    OpenMenu(menu);
                }
            }
        }

        if (!openMenu.isOpen) {
            CloseMenu();
        }
    }

    void OpenMenu(Menu menu) {
        if (openMenu != null) {
            CloseMenu();
        }

        Pause.Freeze();
        openMenu = menu;
        menu.Open();
        menu.menu.SetActive(true);
    }

    void CloseMenu() {
        Pause.Unfreeze();
        openMenu.Close();
        openMenu.menu.SetActive(false);
        openMenu = null;
    }
}
