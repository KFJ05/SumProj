using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Die : MonoBehaviour
{
    Health health;



    public GameObject[] parts;
    public float explosiveRange;

    public Animator anim;
    public bool useAnimator;

    private void Awake()
    {
        health = gameObject.GetComponent<Health>();
    }


    private void Update()
    {
        if(health.Dead == true)
        {
            if (useAnimator)
            {
                anim.SetBool("IsFiring", false);
            }
            for (int i = 0; i < parts.Count(); i++)
            {
                parts[i].gameObject.transform.SetParent(null);
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


}