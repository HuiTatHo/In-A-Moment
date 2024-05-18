using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseChecker : MonoBehaviour
{

    public GameObject backpack;
    public GameObject pauseMenu;

    void Update()
    {
        if ((pauseMenu.activeInHierarchy == false) && (backpack.activeInHierarchy == false) )
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
        
    }
}
