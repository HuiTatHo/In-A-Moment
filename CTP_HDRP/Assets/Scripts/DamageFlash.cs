using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    SkinnedMeshRenderer meshRenderer;
    Color orignColor;
    float flashTime = 0.15f;


    void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        orignColor = meshRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            FLashStart();
        }
    }

    void FLashStart()
    {
        meshRenderer.material.color = Color.red;
        Invoke("FlashStop", flashTime);
    }

    void FlashStop()
    {
        meshRenderer.material.color = orignColor;
    }
}
