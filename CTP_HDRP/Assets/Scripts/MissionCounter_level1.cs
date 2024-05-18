using TMPro;
using UnityEngine;

public class MissionCounter_level1 : MonoBehaviour
{
    public TMP_Text enemyDeathCountText;
    public TMP_Text targetCountText;
        private void Update()
    {
        enemyDeathCountText.text = "Enemy Neutralized: " + EnemyAI_Melee_v2.enemyMeleeCount.ToString() +"/6";
        targetCountText.text = "Target Destroyed: " + InteractObject_HP.interactObjectCount.ToString() +"/6";
    }
}
