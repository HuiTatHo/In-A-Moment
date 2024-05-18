using UnityEngine;

public class TeleportSpawner_0 : MonoBehaviour
{
    public int requiredTargetCount = 1; 
    public GameObject objectToActivate; 

    private void Update()
    {
        CheckMission();
    }

    private void CheckMission()
    {
        if (InteractObject_HP.interactObjectCount >= requiredTargetCount)
        {
            objectToActivate.SetActive(true); 
        }
    }
}
