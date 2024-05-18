using System.Collections.Generic;
using UnityEngine;

public class PlayerOnHit : MonoBehaviour
{
    public PlayerHP playerHP;
    public AudioSource audioSource;
    public AudioClip enemyBulletHit;
    public AudioClip enemyMeleeHit;

    [Header("DamageFlash")]
    SkinnedMeshRenderer meshRenderer;
    Color originalColor;
    float flashTime = 0.15f;
    float flashAlpha = 0.2f;

    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();

    void Awake()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        originalColor = meshRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            playerHP.TakeDamage(30);
            FlashStart();
            audioSource.PlayOneShot(enemyBulletHit);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("EnemyMelee"))
        {
            playerHP.TakeDamage(30);
            FlashStart();
            audioSource.PlayOneShot(enemyMeleeHit);
        }
    }

    void SaveOriginalMaterials()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            originalMaterials.Add(renderer, renderer.materials);
        }
    }

    void FlashStart()
    {
        SaveOriginalMaterials();

        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            Material[] newMaterials = new Material[materials.Length];

            for (int i = 0; i < materials.Length; i++)
            {
                Material originalMaterial = materials[i];
                Material newMaterial = new Material(originalMaterial);
                newMaterial.color = Color.red;
                newMaterials[i] = newMaterial;
            }

            renderer.materials = newMaterials;
        }

        Invoke("FlashStop", flashTime);
    }

    void FlashStop()
    {
        foreach (KeyValuePair<Renderer, Material[]> entry in originalMaterials)
        {
            Renderer renderer = entry.Key;
            Material[] originalMaterials = entry.Value;

            renderer.materials = originalMaterials;
        }

        originalMaterials.Clear();
    }
}