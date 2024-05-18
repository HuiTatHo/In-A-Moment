using UnityEngine;

public class Teleport : MonoBehaviour
{
    public LevelLoader levelLoader; // 对应的 LevelLoader 组件

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelLoader.LoadNextLevel();
        }
    }
}