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
            bool requested = Input.GetKeyDown(menu.accessKey);

            if (menu == openMenu) {
                if (!menu.isOpen || requested) {
                    CloseMenu();
                }
            }
            else if (menu.isOpen || requested) {
                OpenMenu(menu);
            }
        }
    }

    void OpenMenu(Menu menu) {
        if (openMenu != null) {
            CloseMenu();
        }

        if (menu.shouldPause) {
            Pause.Freeze();
        }
        else {
            Pause.Unfreeze();
        }

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
