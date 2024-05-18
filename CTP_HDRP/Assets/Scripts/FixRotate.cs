using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotate : MonoBehaviour
{

    void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,180);
    }
}
