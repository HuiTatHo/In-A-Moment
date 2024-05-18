using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject hud;
    public bool isMainMenuOpen = false;

private void Update()
{
    OpenMainMenu();
}

    void OpenMainMenu()
    {
        if (mainMenu.activeInHierarchy == false)
        {
            isMainMenuOpen = false;
            hud.SetActive(true);
        }
        else
        {
            isMainMenuOpen = true;
            hud.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMainMenuOpen = !isMainMenuOpen;
            mainMenu.SetActive(isMainMenuOpen);
        }
    }
}
