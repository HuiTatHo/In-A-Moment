using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool collided = false;

    private void Start()
    {
        StartCoroutine(DestroyBulletAfterDelay(5f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collided)
        {
            if (!collision.collider.CompareTag("Enemy") && !collision.collider.CompareTag("InteractObject"))
            {
                // 如果碰撞到的物体不是 "Enemy" 或 "InteractObject"，则销毁子弹对象
                Destroy(gameObject);
            }
            collided = true;
        }
    }

    private System.Collections.IEnumerator DestroyBulletAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!collided)
        {
            // 如果在延迟时间内没有发生碰撞，销毁子弹对象
            Destroy(gameObject);
        }
    }
}
