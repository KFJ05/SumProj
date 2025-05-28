using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image[] healthbars;
    public Image LerpBar;
    public Color LerpBarDamageColour;
    public Color LerpBarHealColour;

    public float totalHealth;
    public float currenthealth;

    float MaxhealthPerhpBar;
    float CurrHpPerBar;

    float lerpTimer;

    public float time = 2f;

    private void Awake()
    {
        currenthealth = totalHealth;

        MaxhealthPerhpBar = totalHealth / healthbars.Count();
        CurrHpPerBar = currenthealth / healthbars.Count();


    }


    public void UpdtadeHealthUI(int currentHealthBar)
    {
        float FillHP = healthbars[currentHealthBar].fillAmount;
        float FillLBar = LerpBar.fillAmount;

        float hFraction = currenthealth / totalHealth;



        if (FillLBar > hFraction)
        {
            healthbars[currentHealthBar].fillAmount = hFraction;

            LerpBar.color = LerpBarDamageColour;


            lerpTimer += Time.deltaTime;
            float PercentC = lerpTimer / time;
            LerpBar.fillAmount = Mathf.Lerp(FillLBar, hFraction, PercentC);

        }
        if (FillHP < hFraction)
        {

            LerpBar.color = LerpBarHealColour;
            LerpBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float PercentC = lerpTimer / time;
            healthbars[currentHealthBar].fillAmount = Mathf.Lerp(FillHP, LerpBar.fillAmount, PercentC);
        }

    }



}
