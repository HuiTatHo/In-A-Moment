using UnityEngine;

public class InteractObject_HP : MonoBehaviour
{
    public GameObject VFX1;
    public GameObject VFX2;
    public GameObject VFX3;
    public GameObject VFX4;
    public AudioSource audioSource;
    public AudioClip acHit;
    public AudioClip acExploe;

    public static int interactObjectCount = 0;



    private int interactionsCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {

            Destroy(other.gameObject);


            interactionsCount++;


            switch (interactionsCount)
            {
                case 1:
                    VFX1.SetActive(true);
                    audioSource.PlayOneShot(acHit);
                    break;
                case 2:
                    VFX2.SetActive(true);
                    audioSource.PlayOneShot(acHit);
                    break;
                case 3:
                    VFX3.SetActive(true);
                    audioSource.PlayOneShot(acHit);
                    break;
                case 4:
                    VFX4.SetActive(true);
                    audioSource.PlayOneShot(acHit);
                    audioSource.PlayOneShot(acExploe);
                    StartCoroutine(DestroyAfterVFX());
                    break;
                default:
                    break;
            }
        }
        else if (other.CompareTag("melee"))
        {
            interactionsCount++;


            switch (interactionsCount)
            {
                case 1:
                    VFX1.SetActive(true);
                    audioSource.PlayOneShot(acHit);
                    break;
                case 2:
                    VFX2.SetActive(true);
                    audioSource.PlayOneShot(acHit);
                    break;
                case 3:
                    VFX3.SetActive(true);
                    audioSource.PlayOneShot(acHit);
                    break;
                case 4:
                    VFX4.SetActive(true);
                    audioSource.PlayOneShot(acHit);
                    audioSource.PlayOneShot(acExploe);
                    StartCoroutine(DestroyAfterVFX());
                    break;
                default:
                    break;
            }
        }

    }

    private System.Collections.IEnumerator DestroyAfterVFX()
    {
        yield return new WaitForSeconds(VFX4.GetComponent<ParticleSystem>().main.duration);

        interactObjectCount++;
        Destroy(gameObject);
    }
}