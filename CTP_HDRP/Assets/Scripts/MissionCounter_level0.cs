using TMPro;
using UnityEngine;

public class MissionCounter_level0 : MonoBehaviour
{
    public TMP_Text targetCountText;
        private void Update()
    {
        targetCountText.text = "Target Destroyed: " + InteractObject_HP.interactObjectCount.ToString() + "/1";
    }
}
