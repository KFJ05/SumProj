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

    public EnemyAI Enemy;

    float lerpTimer;

    public float time = 2f;

    int i = 0;

    public float totalHealth;

    public float CritMUlt;
    bool didCrit;

    bool PlayPS = false;

    public ParticleSystem DamageEffect;
    public ParticleSystem CritDamageEffect;


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
            if(Enemy != null)
            {
                Enemy.DestroyTurrets();
            }

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
            if (DamageEffect != null && PlayPS == true && didCrit == false)
            {
                DamageEffect.Play();
                PlayPS = false;
            }
            else if (CritDamageEffect != null && PlayPS == true && didCrit == true)
            {
                CritDamageEffect.Play();
                PlayPS = false;
            }

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


    public void CRITDamage(float Damage)
    {
        CurrentHealthBarHP[0] -= (Damage * CritMUlt);


        lerpTimer = 0;

        if (CurrentHealthBarHP[0] <= 0)
        {
            //Dead = true;
        }
        PlayPS = true;
        didCrit = true;
    }

    public void Heal(float AmountHealed)
    {
        CurrentHealthBarHP[0] += AmountHealed;


        lerpTimer = 0;



        if (CurrentHealthBarHP[0] > MaxHealthPerHealthBar[0])
        {
            CurrentHealthBarHP[0] = MaxHealthPerHealthBar[0];
        }
    }



}
