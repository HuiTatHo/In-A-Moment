using UnityEngine;
using UnityEngine.AI;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f; 
    public float rotationSpeed = 5f; 
    public Transform target; 
    public float scaleDuration = 0.5f; 
    public float initialScale = 0.001f; 
    public float targetScale = 0.5f; 
    public float delayAfterScale = 1f;
    public float destroyDelay = 3f; 

    private NavMeshAgent agent; 

    private void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform; 
        }

        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(ScaleBullet());
        Destroy(gameObject, destroyDelay);
    }

    private void Update()
    {
        if (target != null)
        {
            
            Vector3 direction = target.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

           
            agent.SetDestination(target.position);
        }
    }


    private System.Collections.IEnumerator ScaleBullet()
    {
        float elapsedTime = 0f;
        Vector3 initialScaleVector = new Vector3(initialScale, initialScale, initialScale);
        Vector3 targetScaleVector = new Vector3(targetScale, targetScale, targetScale);

        while (elapsedTime < scaleDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / scaleDuration);
            transform.localScale = Vector3.Lerp(initialScaleVector, targetScaleVector, t);
            yield return null;
        }

        yield return new WaitForSeconds(delayAfterScale);


    }
}