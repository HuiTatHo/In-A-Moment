using UnityEngine;

public class FollowBody : MonoBehaviour
{
    public Transform childTransform; 

    private void Update()
    {
        transform.position = childTransform.position;
    }
}