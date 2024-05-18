using UnityEngine;

public class Highlighted : MonoBehaviour
{
    public Material highlightMaterial;
    private Material[] originalMaterials;
    private MeshRenderer meshRenderer;
    public AudioSource audioSource;
    public AudioClip enterSound;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterials = meshRenderer.materials;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scanner"))
        {
            Material[] newMaterials = new Material[originalMaterials.Length + 1];
            originalMaterials.CopyTo(newMaterials, 0);
            newMaterials[originalMaterials.Length] = highlightMaterial;
            meshRenderer.materials = newMaterials;

            audioSource.PlayOneShot(enterSound);

            StartCoroutine(ResetMaterialsAfterDelay(3f));
        }
    }

    private System.Collections.IEnumerator ResetMaterialsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        meshRenderer.materials = originalMaterials;
    }
}
