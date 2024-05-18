using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState_UI : MonoBehaviour
{
    public GameObject walkUI;
    public GameObject runUI;
    public GameObject standUI;

    public PlayerController playerController;
    void Update()
    {
        bool isWalking = playerController.GetWalking();
        bool isRunning =  playerController.GetRunning();

        if (isWalking && !isRunning)
        {
            SetActiveUI(walkUI);
            SetInactiveUI(runUI);
            SetInactiveUI(standUI);
        }
        else if (isRunning)
        {
            SetInactiveUI(walkUI);
            SetActiveUI(runUI);
            SetInactiveUI(standUI);
        }
        else if (!isWalking)
        {
            SetInactiveUI(walkUI);
            SetInactiveUI(runUI);
            SetActiveUI(standUI);
        }
    }

    void SetActiveUI(GameObject uiObject)
    {
        uiObject.SetActive(true);
    }

    void SetInactiveUI(GameObject uiObject)
    {
        uiObject.SetActive(false);
    }
}
