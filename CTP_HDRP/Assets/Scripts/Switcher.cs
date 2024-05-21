using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class Switcher : MonoBehaviour
{
    public VolumeProfile profile1;
    public VolumeProfile profile2; 
    public GameObject[] objectsGroup1; 
    public GameObject[] objectsGroup2; 

    public PlayerHP playerHP;
    
    public Animator voidSwitchTransition;

    public Light directionalLight; 
    public float intensity1 = 1.0f; 
    public float intensity2 = 0.1f; 


    public AudioSource audioSource;
    public AudioClip switchSFX; 

    private Volume volume; 
    private bool isProfile1Active = true; 

    private HDAdditionalLightData hdLight; 

    private void Start()
    {
        volume = GetComponent<Volume>();
        hdLight = directionalLight.GetComponent<HDAdditionalLightData>(); 
        StartCoroutine(DebuffCoroutine());
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchProfile();
            SwitchObjectsGroup();
            ToggleLightIntensity(); 
            PlaySoundEffect(); 
        }
    }

    private void SwitchProfile()
    {
        if (isProfile1Active)
        {
            // Volume Profile 1
            volume.profile = profile2;
            isProfile1Active = false;
            voidSwitchTransition.SetBool("Now", true);
        }
        else
        {
            // Volume Profile 2
            volume.profile = profile1;
            isProfile1Active = true;
            voidSwitchTransition.SetBool("Now", false);
        }
    }

    private void SwitchObjectsGroup()
    {
        if (isProfile1Active)
        {
            
            foreach (GameObject obj in objectsGroup1)
            {
                obj.SetActive(false);
            }

            
            foreach (GameObject obj in objectsGroup2)
            {
                obj.SetActive(true);
            }
        }
        else
        {
            
            foreach (GameObject obj in objectsGroup2)
            {
                obj.SetActive(false);
            }

            
            foreach (GameObject obj in objectsGroup1)
            {
                obj.SetActive(true);
            }
        }
    }

        private System.Collections.IEnumerator DebuffCoroutine()
    {
        while (true)
        {
            if (isProfile1Active)
            {
                playerHP.HealHP(1);
            }
            else
            {

                playerHP.TakeDamage(2);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void ToggleLightIntensity()
    {
        
        if (hdLight.intensity == intensity1)
        {
            hdLight.intensity = intensity2;
        }
        else
        {
            hdLight.intensity = intensity1;
        }
    }

    private void PlaySoundEffect()
    {
        if (switchSFX != null)
        {
            audioSource.PlayOneShot(switchSFX);
        }
    }
}