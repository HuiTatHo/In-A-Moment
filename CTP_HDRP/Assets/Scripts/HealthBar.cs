using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Slider easeSlider;

    private float lerpSpeed = 0.05f;
    public Gradient gradient;
    public Image fill;

    public PlayerHP curreentHP;


    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }
    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }


    void Update()
    {
        var hp = curreentHP.currentHealth;
        if(slider.value != easeSlider.value)
        {
            easeSlider.value = Mathf.Lerp(easeSlider.value, hp, lerpSpeed);
        }
        
    }
}
