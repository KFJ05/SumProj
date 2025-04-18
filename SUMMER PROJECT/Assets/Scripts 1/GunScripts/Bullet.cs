using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    public float Damage;

    public float bulletLifeTime;

    public bool Testing;

    public string[] TagsToDamage;


    private void Awake()
    {
        if (!Testing)
        {
            Destroy(gameObject, bulletLifeTime);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        for(int i = 0; i < TagsToDamage.Count(); i++)
        {
            if(collision.gameObject.tag == TagsToDamage[i])
            {
                break;
            }



        }

        Health tempHP = collision.gameObject.GetComponent<Health>();

        if (tempHP != null)
        {
            tempHP.Damage(Damage);
        }
        if(!Testing)
        {
            Destroy(gameObject);
        }

    }
    
}
