using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
    public float rotationSpeed = 10f; // 物體的旋轉速度
    public float floatSpeed = 1f; // 物體的浮動速度
    public float floatHeight = 0.5f; // 物體的浮動高度

    private float originalY; // 物體的初始Y位置

    void Start()
    {
        originalY = transform.position.y;
    }

    void Update()
    {
        // 根據時間和速度計算旋轉角度
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // 繞著Y軸旋轉物體
        transform.Rotate(Vector3.up, rotationAmount, Space.World);

        // 計算浮動的位置
        float newY = originalY + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // 更新物體的位置
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
