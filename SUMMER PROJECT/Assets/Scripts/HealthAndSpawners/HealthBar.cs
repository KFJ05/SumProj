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

    public bool usingTurretDie = true;

    public Die Death;

    public EnemyAI Enemy;

    float lerpTimer;

    public float time = 2f;

    int i = 0;

    public float totalHealth;

    public float CritMUlt;
    bool didCrit;

    bool PlayPS = false;

    public GameObject[] Segments;
    public float[] HealthPerSegment;
    public Material SegmentMaterial;
    public Material SegMat;

    public ParticleSystem DamageEffect;
    public ParticleSystem CritDamageEffect;

    float storedDamage;


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

    private void Start()
    {
        for (int i = 0; i < Segments.Length; i++)
        {
            if (Segments[i] != null)
            {
                SegmentMaterial = Segments[i].GetComponent<Image>().material;
                SegMat = new Material(SegmentMaterial);
            }
            if (SegmentMaterial != null)
            {
                float segmentAmmount = MaxHealthPerHealthBar[i] / HealthPerSegment[i];
                float NewSegmentAmount = segmentAmmount;

                SetSegments(NewSegmentAmount, i);
            }
        }

    }
    public void SetSegments(float NewSegA, int i)
    {
        SegMat.SetFloat("_Frequency", NewSegA);
        SegmentMaterial = Segments[i].GetComponent<Image>().material = SegMat;
    }

    private void Update()
    {
        if (totalHealth <= 0)
        {
            if(Enemy != null)
            {
                Enemy.DestroyTurrets();
            }
            if (usingTurretDie)
            {
                Death.TriggerDeath = true;
            }
        }

        if (CurrentHealthBarHP[0] <= 0)
        {
            if (CurrentHealthBarHP[0] < 0)
            {
                storedDamage = CurrentHealthBarHP[0];

            }
            Destroy(healthbars[0]);
            healthbars.Remove(healthbars[0]);
            MaxHealthPerHealthBar.Remove(MaxHealthPerHealthBar[0]);
            CurrentHealthBarHP.Remove(CurrentHealthBarHP[0]);
            Destroy(LerpBars[0]);
            LerpBars.Remove(LerpBars[0]);
        }
        if (storedDamage < 0)
        {
            CurrentHealthBarHP[0] += storedDamage;
            storedDamage = 0;
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
        if (CurrentHealthBarHP[0] != null)
        {
            CurrentHealthBarHP[0] -= Damage;

            totalHealth -= Damage;

            lerpTimer = 0;

            if (CurrentHealthBarHP[0] <= 0 && CurrentHealthBarHP.Count == 1)
            {
                //  Dead = true;
            }
            PlayPS = true;
            //didCrit = false;
        }
    }


    public void CRITDamage(float Damage)
    {

        if (CurrentHealthBarHP[0] != null)
        {
            CurrentHealthBarHP[0] -= (Damage * CritMUlt);

            totalHealth -= (Damage * CritMUlt);

            lerpTimer = 0;

            if (CurrentHealthBarHP[0] <= 0)
            {
                //Dead = true;
            }
            PlayPS = true;
            didCrit = true;
        }
    }

    public void Heal(float AmountHealed)
    {
        CurrentHealthBarHP[0] += AmountHealed;
        totalHealth += AmountHealed;
        lerpTimer = 0;
        if (CurrentHealthBarHP[0] > MaxHealthPerHealthBar[0])
        {
            float F = CurrentHealthBarHP[0] - MaxHealthPerHealthBar[0];
            totalHealth -= F;
            CurrentHealthBarHP[0] = MaxHealthPerHealthBar[0];
        }
      
    }



}
