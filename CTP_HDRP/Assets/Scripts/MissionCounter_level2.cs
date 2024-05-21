using TMPro;
using UnityEngine;

public class MissionCounter_level2 : MonoBehaviour
{
    public TMP_Text enemyDeathCountText;
    public TMP_Text targetCountText;
        private void Update()
    {
        enemyDeathCountText.text = "Enemy Neutralized: " + EnemyAI_Range.enemyRangeCount.ToString() +"/6";
        targetCountText.text = "Target Destroyed: " + InteractObject_HP.interactObjectCount.ToString() +"/6";
    }
}
