using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurretTransform : MonoBehaviour
{
    // Start is called before the first frame update

    public bool AddRocketsOnHpInterval;
    public bool RemoveRocketsOnHpInterval;

    public bool AddBulletsOnHpInterval;
    public bool RemoveBulletsOnHpInterval;

    public float HpInterval;

    [Header("Refrences")]
    public Health BaseHP;
    public HealthBarMultiple MultHP;
    public TurretAI TurrAI;
    public bool usingMultipleHealthbars = false;

    public bool TriggerOnce;
    bool triggerd = false;

    public bool ChangeAiStats;

    public float newFireRate, NewStunTimer, NewBulletSpread, newRange, newBulletSpeed, newTurnSpeed;

    public bool ChangeRocketStats;
    public float newRocketTimer;


    public bool usingParts;
    public GameObject[] parts;
    public float explosiveRange;

    // Update is called once per frame
    void Update()
    {
        if (usingMultipleHealthbars == true)
        {
            if (MultHP.totalHealth <= HpInterval && !triggerd)
            {
                if (AddRocketsOnHpInterval == true)
                {
                    TurrAI.FireRockets = true;
                }
                if (RemoveRocketsOnHpInterval == true)
                {
                    TurrAI.FireRockets = false;
                }
                if (AddBulletsOnHpInterval == true)
                {
                    TurrAI.FireBullets = true;
                }
                if (RemoveBulletsOnHpInterval == true)
                {
                    TurrAI.FireBullets = false;
                }

                if (TriggerOnce)
                {
                    triggerd = true;
                    VisualDestruction();
                }
                if(ChangeAiStats == true)
                {
                    SetNewStats();
                }
                if(ChangeRocketStats == true)
                {
                    SetNewrocketTimer();
                }
            }
        }
        else
        {
            if (BaseHP.CurrentHealth <= HpInterval && !triggerd)
            {
                if (AddRocketsOnHpInterval == true)
                {
                    TurrAI.FireRockets = true;
                }
                if (RemoveRocketsOnHpInterval == true)
                {
                    TurrAI.FireRockets = false;
                }
                if (AddBulletsOnHpInterval == true)
                {
                    TurrAI.FireBullets = true;
                }
                if (RemoveBulletsOnHpInterval == true)
                {
                    TurrAI.FireBullets = false;
                }


                if(TriggerOnce)
                {
                    triggerd = true;
                    VisualDestruction();
                }
                if (ChangeAiStats == true)
                {
                    SetNewStats();
                }
                if (ChangeRocketStats == true)
                {
                    SetNewrocketTimer();
                }
            }
        }
    }


    public void VisualDestruction()
    {
            for (int i = 0; i < parts.Count(); i++)
            {
                parts[i].gameObject.transform.SetParent(null);
                parts[i].gameObject.tag = "SparePart";
                if (PartManager.Instance != null)
                {
                    PartManager.Instance.AddPart(parts[i]);
                }
                Rigidbody rb = parts[i].gameObject.GetComponent<Rigidbody>();
                Collider Col = parts[i].gameObject.GetComponent<Collider>();

                Col.isTrigger = false;

                rb.isKinematic = false;
                rb.useGravity = true;

                float x = Random.Range(-explosiveRange, explosiveRange);
                float y = Random.Range(0, explosiveRange);
                float z = Random.Range(-explosiveRange, explosiveRange);

                rb.AddForce(x, y, z, ForceMode.Impulse);
            }
     }

    public void SetNewStats()
    {
        TurrAI.FireRate = newFireRate;
        TurrAI.StunTimer = NewStunTimer;
        TurrAI.Spread = NewBulletSpread;
        TurrAI.Range = newRange;
        TurrAI.shootF = newBulletSpeed;
        TurrAI.turnSpeed = newTurnSpeed;
    }


    public void SetNewrocketTimer()
    {
        TurrAI.RocketFireRate = newRocketTimer;
    }
}



