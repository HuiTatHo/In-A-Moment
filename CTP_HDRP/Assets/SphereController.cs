using UnityEngine;

public class SphereController : MonoBehaviour
{
    public float expandSpeed = 1f;
    public float destroyDelay = 3f;

    private void Start()
    {
        ExpandSphere();
    }

    private void ExpandSphere()
    {
        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = initialScale * 50f; // 设置目标放大倍数

        StartCoroutine(ScaleOverTime(initialScale, targetScale, destroyDelay));
    }

    private System.Collections.IEnumerator ScaleOverTime(Vector3 initialScale, Vector3 targetScale, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        Destroy(gameObject);
    }
}
