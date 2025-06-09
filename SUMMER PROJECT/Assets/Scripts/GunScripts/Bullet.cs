using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    public float Damage;

    public float bulletLifeTime;

    public bool Testing;

    public string[] TagsToDamage;

    public LayerMask Layer;

    public ParticleSystem poofEffect;

    private void Awake()
    {
        if (!Testing)
        {
            Destroy(gameObject, bulletLifeTime);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < TagsToDamage.Length; i++)
        {
            if (collision.gameObject.CompareTag(TagsToDamage[i]))
            {
                // Health should be on the hit object or one of its parents
                Health tempHP = collision.gameObject.GetComponent<Health>();
                HealthBarMultiple tempHPM = null;
                if (tempHP == null)
                {
                    tempHP = collision.gameObject.GetComponentInParent<Health>();
                }
                
                if(tempHP == null)
                {
                    
                    tempHPM = collision.gameObject.GetComponent<HealthBarMultiple>();
                    if(tempHPM == null)
                    {
                        tempHPM = collision.gameObject.GetComponentInParent<HealthBarMultiple>();
                    }

                }

                // Check if the actual collider that was hit has the WeakSpot
                WeakSpot WS = collision.collider.GetComponent<WeakSpot>();

                if (tempHP != null)
                {
                    if (WS != null)
                    {
                        tempHP.CRITDamage(Damage);
                    }
                    else
                    {
                        tempHP.Damage(Damage);
                    }
                }
                if(tempHPM != null)
                {
                    tempHPM.Damage(Damage);
                }

                break;
            }
        }

        if (!Testing)
        {
            poofEffect.transform.parent = null;
            poofEffect.Play();

            Destroy(gameObject);
        }
    }



}
