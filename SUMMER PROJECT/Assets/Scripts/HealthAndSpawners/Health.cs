using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("HPSettings")]
    public float CurrentHealth;
    public float MaxHealth;
    float MH;

    public bool UseHealthBar;
    public bool useSheildBar;

    public Color LerpBarDamageColour;
    public Color LerpBarHealColour;

    public float time = 2f;

    public bool Dead;

    public float CritMUlt;
    bool didCrit;

    bool PlayPS = false;

    [Header("Refrences")]
    public Image HealthBar;
    public Image LerpBar;
    public GameObject Segments;
    public Material SegmentMaterial;
    public Material SegMat;

   

    public float HealthPerSegment;

    public GameObject damageText;

    public ParticleSystem DamageEffect;
    public ParticleSystem CritDamageEffect;

    float lerpTimer;


    [Header("Sheild settings")]
    public float SheildSize;
    public bool SheildActive;
    bool SheildTrigger;
    public float SheildCurrentHealth;
    public float SheildMaxHealth; 

    [Header("Sheild Prefabs")]
    public GameObject SheildPrefab;
    public Image SheildHBar;
    public Image SheildLerpBar;
    public float Sheildtime = 4f;
    GameObject sheild;
    public float YOffset;

    public bool isPlayer = false;

    float D = 0;




    private void Start()
    {
        MH = MaxHealth;

        if (Segments != null)
        {
            SegmentMaterial = Segments.GetComponent<Image>().material;
            SegMat = new Material(SegmentMaterial);
        }
        if (SegmentMaterial != null)
        {
            float segmentAmmount = MaxHealth/ HealthPerSegment;
            float NewSegmentAmount = segmentAmmount;

            SetSegments(NewSegmentAmount);
        }
        if (SheildHBar != null && SheildLerpBar != null)
        {
            SheildHBar.gameObject.SetActive(false);
            SheildLerpBar.gameObject.SetActive(false);
        }

    }

    public void SetSegments( float NewSegA)
    {
        SegMat.SetFloat("_Frequency", NewSegA);
        SegmentMaterial = Segments.GetComponent<Image>().material = SegMat;
    }

    void Update()
    {
        if(MH != MaxHealth)
        {
            MH = MaxHealth;

            if (Segments != null)
            {
                SegmentMaterial = Segments.GetComponent<Image>().material;
                SegMat = new Material(SegmentMaterial);
            }
            if (SegmentMaterial != null)
            {
                float segmentAmmount = MaxHealth / HealthPerSegment;
                float NewSegmentAmount = segmentAmmount;

                SetSegments(NewSegmentAmount);
            }
            SheildHBar.gameObject.SetActive(false);
            SheildLerpBar.gameObject.SetActive(false);
        }

        if(SheildActive == true && SheildTrigger == false)
        {
            if (SheildPrefab != null)
            {
                SheildCurrentHealth = SheildMaxHealth;
                sheild = Instantiate(SheildPrefab);
                sheild.transform.parent = gameObject.transform;
                sheild.transform.localScale = Vector3.one * SheildSize;
                SheildHBar.gameObject.SetActive(true);
                SheildLerpBar.gameObject.SetActive(true);
                SheildTrigger = true;

                Sheild S = sheild.gameObject.GetComponent<Sheild>();
                S.YOffset = YOffset;
            }
        }
        if(SheildCurrentHealth <= 0 && SheildActive == true)
        {
            if (SheildPrefab != null)
            {
                SheildCurrentHealth = 0;
                SheildActive = false;
                Destroy(sheild);
                SheildHBar.gameObject.SetActive(false);
                SheildLerpBar.gameObject.SetActive(false);
                SheildTrigger = false;
            }
        }
        if(CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }

        if(UseHealthBar == true)
        {
            if (SheildActive == true)
            {
                UpdtadeSheildUI();
                UpdtadeHealthUI();
            }
            else
            {
                UpdtadeHealthUI();
            }
        }
        else if ( useSheildBar == true)
        {
            if (SheildActive == true)
            {
                UpdtadeSheildUI();
            }
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
            LerpBar.fillAmount = Mathf.Lerp(FillLBar, hFraction, PercentC);
            
        }
        if (FillHP < hFraction)
        {
 
            LerpBar.color = LerpBarHealColour;
            LerpBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float PercentC = lerpTimer / time;
            HealthBar.fillAmount = Mathf.Lerp(FillHP, LerpBar.fillAmount, PercentC);
        }

    }

    public void UpdtadeSheildUI()
    {
        if (SheildPrefab == null)
        {
            SheildActive = false;
            return;
        }

        float FillHP = SheildHBar.fillAmount;
        float FillLBar = SheildLerpBar.fillAmount;

        float SFraction = SheildCurrentHealth / SheildMaxHealth;



        if (FillLBar > SFraction)
        {
            SheildHBar.fillAmount = SFraction;

            SheildLerpBar.color = LerpBarDamageColour;

            lerpTimer += Time.deltaTime;
            float PercentC = lerpTimer / Sheildtime;
            SheildLerpBar.fillAmount = Mathf.Lerp(FillLBar, SFraction, PercentC);

        }
        if (FillHP < SFraction)
        {

            SheildLerpBar.color = LerpBarHealColour;
            SheildLerpBar.fillAmount = SFraction;
            lerpTimer += Time.deltaTime;
            float PercentC = lerpTimer / Sheildtime;
            SheildHBar.fillAmount = Mathf.Lerp(FillHP, SheildLerpBar.fillAmount, PercentC);
        }

    }


    public void Damage(float Damage)
    {
        D = Damage;
        if (SheildActive == true)
        {
            SheildCurrentHealth -= Damage;

        }
        else
        {
            CurrentHealth -= Damage;


            lerpTimer = 0;

            if (CurrentHealth <= 0)
            {
                Dead = true;
            }
            PlayPS = true;
            if (DamageEffect != null && PlayPS == true && didCrit == false)
            {
                ShowText();
                DamageEffect.Play();
                PlayPS = false;
            }
            didCrit = false;
        }
    }

    public void CRITDamage(float Damage)
    {
        D = (Damage * CritMUlt);
        if (SheildActive == true)
        {
            SheildCurrentHealth -= (Damage * CritMUlt);
        }
        else
        {
            CurrentHealth -= (Damage * CritMUlt);

            lerpTimer = 0;

            if (CurrentHealth <= 0)
            {
                Dead = true;
            }
            PlayPS = true;
            if (CritDamageEffect != null && PlayPS == true && didCrit == true)
            {
                ShowText();
                CritDamageEffect.Play();
                PlayPS = false;
            }
            didCrit = true;
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

    public void ShowText()
    {
        if(damageText == null)
        {
            return;
        }

        var DTxt = Instantiate(damageText, transform.position, Quaternion.identity, transform);
        DTxt.GetComponent<TextMesh>().text = D.ToString();
    }



}
