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
                if (tempHP == null)
                {
                    tempHP = collision.gameObject.GetComponentInParent<Health>();
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

                if (!Testing)
                {
                    Destroy(gameObject);
                }

                break;
            }
        }
    }



}
