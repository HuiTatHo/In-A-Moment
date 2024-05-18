using UnityEngine;

public class ScanController : MonoBehaviour
{
    public GameObject spherePrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GenerateSphere();
        }
    }

    private void GenerateSphere()
    {
        Instantiate(spherePrefab, transform.position, Quaternion.identity);
    }
}