using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("HPSettings")]
    public float CurrentHealth;
    public float MaxHealth;

    public bool UseHealthBar;

    public Color LerpBarDamageColour;
    public Color LerpBarHealColour;

    public float time = 2f;

    public bool Dead;

    [Header("Refrences")]
    public Image HealthBar;
    public Image LerpBar;

    float lerpTimer;

    // Start is called before the first frame update
 
    // Update is called once per frame
    void Update()
    {
        if(UseHealthBar == true)
        {
            UpdtadeHealthUI();
        }
    }

    public void UpdtadeHealthUI()
    {
        float FillHP = HealthBar.fillAmount;
        float FillLBar = LerpBar.fillAmount;

        float hFraction = CurrentHealth / MaxHealth;

        if(FillLBar > hFraction)
        {
            HealthBar.fillAmount = hFraction;
            LerpBar.color = LerpBarDamageColour;
            lerpTimer += Time.deltaTime;
            float PercentC = lerpTimer / time;
            LerpBar.fillAmount = Mathf.Lerp(FillLBar, hFraction, PercentC);
            
        }
        if (FillLBar < hFraction)
        {
 
            LerpBar.color = LerpBarHealColour;
            LerpBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float PercentC = lerpTimer / time;
            HealthBar.fillAmount = Mathf.Lerp(FillLBar, hFraction, PercentC);
        }

    }

    public void Damage(float Damage)
    {
        CurrentHealth -= Damage;

      
        lerpTimer = 0;
        
        if(CurrentHealth <= 0)
        {
            Dead = true;
        }

    }
    public void Heal(float AmountHealed)
    {
        CurrentHealth += AmountHealed;

      
         lerpTimer = 0;
        
        if(CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }



}
