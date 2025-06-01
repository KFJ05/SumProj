using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarMultiple : MonoBehaviour
{
    public List<Image> healthbars = new List<Image>();
    public List<Image> LerpBars = new List<Image>();
    public Color LerpBarDamageColour;
    public Color LerpBarHealColour;

    public List<float> MaxHealthPerHealthBar = new List<float>(); 
    public List<float> CurrentHealthBarHP = new List<float>();

    public Die Death;

    float lerpTimer;

    public float time = 2f;

    int i = 0;

    public float totalHealth;

    private void Awake()
    {



        if (CurrentHealthBarHP.Count == MaxHealthPerHealthBar.Count && CurrentHealthBarHP.Count == healthbars.Count)
        {

            CurrentHealthBarHP[0] = MaxHealthPerHealthBar[0];

            for(int i = 0; i < CurrentHealthBarHP.Count; i++)
            {
                totalHealth += CurrentHealthBarHP[i];
            }
        }


    }

    private void Update()
    {
        if (totalHealth <= 0)
        {
            Death.TriggerDeath = true;
        }

        if (CurrentHealthBarHP[0] <= 0)
        {
            if (CurrentHealthBarHP[0] < 0)
            {
                if (1 < CurrentHealthBarHP.Count)
                {
                    CurrentHealthBarHP[i] -= CurrentHealthBarHP[0];
                }
            }

            Destroy(healthbars[0]);
            healthbars.Remove(healthbars[0]);
            MaxHealthPerHealthBar.Remove(MaxHealthPerHealthBar[0]);
            CurrentHealthBarHP.Remove(CurrentHealthBarHP[0]);
            Destroy(LerpBars[0]);
            LerpBars.Remove(LerpBars[0]);

        }


        UpdtadeHealthUI(0);


    }


    public void UpdtadeHealthUI(int currentHealthBar)
    {
        float FillHP = healthbars[currentHealthBar].fillAmount;
        float FillLBar = LerpBars[currentHealthBar].fillAmount;

        float hFraction = CurrentHealthBarHP[currentHealthBar] / MaxHealthPerHealthBar[currentHealthBar];



        if (FillLBar > hFraction)
        {
            healthbars[currentHealthBar].fillAmount = hFraction;

            LerpBars[currentHealthBar].color = LerpBarDamageColour;


            lerpTimer += Time.deltaTime;
            float PercentC = lerpTimer / time;
            LerpBars[currentHealthBar].fillAmount = Mathf.Lerp(FillLBar, hFraction, PercentC);

        }
        if (FillHP < hFraction)
        {

            LerpBars[currentHealthBar].color = LerpBarHealColour;
            LerpBars[currentHealthBar].fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float PercentC = lerpTimer / time;
            healthbars[currentHealthBar].fillAmount = Mathf.Lerp(FillHP, LerpBars[currentHealthBar].fillAmount, PercentC);
        }

    }

    public void Damage(float Damage)
    {
        CurrentHealthBarHP[0] -= Damage;

        totalHealth -= Damage;

        lerpTimer = 0;

        if (CurrentHealthBarHP[0] <= 0 && CurrentHealthBarHP.Count == 1)
        {
          //  Dead = true;
        }
        //PlayPS = true;
        //didCrit = false;
    }



}
