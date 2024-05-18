using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour

{
    public Slider slider;
    public Slider easeSlider;

    private float lerpSpeed = 0.05f;
    public Gradient gradient;
    public Image fill;

    public PlayerHP currentPlayerHP; // 引用PlayerHP脚本

    public void SetMaxStamina(int stamina)
    {
        slider.maxValue = stamina;
        slider.value = stamina;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetStamina(int stamina)
    {
        slider.value = stamina;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    void Update()
    {
        int currentStamina = currentPlayerHP.currentStamina; // 获取当前體力值
        if (slider.value != easeSlider.value)
        {
            easeSlider.value = Mathf.Lerp(easeSlider.value, currentStamina, lerpSpeed);
        }
    }
}

