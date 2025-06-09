using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Die : MonoBehaviour
{
    Health health;

    public bool usingMultipleHpBars;

    public GameObject[] parts;
    public float explosiveRange;

    public Animator anim;
    public bool useAnimator;

    public bool TriggerDeath = false;

    public bool useHealth = true;

    public bool WinLevelOnDeath = false;

    public bool useParticles;
    public ParticleSystem DeathExplosiom;

    private void Awake()
    {
        health = gameObject.GetComponent<Health>();
    }


    private void Update()
    {
        if (health == null || useHealth == false)
        {
            if (TriggerDeath == true)
            {
                DestroyEnemy();
            }
        }
        else if ( health.CurrentHealth <= 0 && useHealth == true)
        {
            DestroyEnemy();
        }

    }

    public void DestroyEnemy()
    {


        if(useParticles == true)
        {
            DeathExplosiom.gameObject.transform.SetParent(null);
            DeathExplosiom.Play();
        }


        if (WinLevelOnDeath == true)
        {
            Victory V = GameObject.FindWithTag("Player").GetComponent<Victory>();

            V.SetWin();

        }


        if (useAnimator)
        {
            anim.SetBool("IsFiring", false);
        }
        for (int i = 0; i < parts.Count(); i++)
        {
            parts[i].gameObject.transform.SetParent(null);
            parts[i].gameObject.tag = "SparePart";
            if(PartManager.Instance != null)
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
        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.RemoveEnemy(gameObject);
        }
      

        Destroy(gameObject);

    }




}