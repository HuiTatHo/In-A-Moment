using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    private Quaternion originalRotation;
    private bool isLifting = false;
    public float recoil = 5f;

    

    private void Start()
    {
        originalRotation = transform.localRotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isLifting)
        {
            isLifting = true;
            transform.localRotation *= Quaternion.Euler(0f, recoil, 0f);
        }

        if (Input.GetKeyUp(KeyCode.J) && isLifting)
        {
            isLifting = false;
            transform.localRotation = originalRotation;
        }
    }
}
