using UnityEngine;

public class TeleportSpawner_1 : MonoBehaviour
{
    public int requiredTargetCount = 6; 
    public int requiredEnemyCount = 6;
    public GameObject objectToActivate; 

    private void Update()
    {
        CheckMission();
    }

    private void CheckMission()
    {
        if ( EnemyAI_Melee_v2.enemyMeleeCount >= requiredEnemyCount && InteractObject_HP.interactObjectCount >= requiredTargetCount)
        {
            objectToActivate.SetActive(true); 
        }
    }
}
