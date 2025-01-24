using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarSlider : MonoBehaviour
{
    public Slider healthSlider;
    public Image fill;
    public Gradient gradient;


    public void SetMaxHealth(int maxHp)
    {
        healthSlider.maxValue = maxHp;
        healthSlider.value = maxHp;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int healthShown)
    {
        healthSlider.value = healthShown;

        fill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }
}
