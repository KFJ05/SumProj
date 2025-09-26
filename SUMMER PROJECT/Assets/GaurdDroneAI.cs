using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GaurdDroneAI : MonoBehaviour
{
    // Start is called before the first frame update

    Health HP;

    HealthBarMultiple HBM;

    Rigidbody RB;

    [Header("Heal Stats")]
    [Range(1,100)]
    public float HealAmount;
    public float Healtimer;
    float HT;




    void Start()
    {


        RB = gameObject.GetComponentInChildren<Rigidbody>();

        RB.isKinematic = true;
        RB.useGravity = false;

        HP = gameObject.GetComponent<Health>();

        HBM = gameObject.GetComponentInParent<HealthBarMultiple>();

        HBM.Heal(HealAmount);

        HT = Healtimer;
    }

    // Update is called once per frame
    void Update()
    {
        HT -= Time.deltaTime;



        if(HT <= 0)
        {
            HT = Healtimer;
            HBM.Heal(HealAmount);
        }

        if(HP.CurrentHealth <= 0)
        {
            RB.isKinematic = false;
            RB.useGravity = true;
            RB.AddTorque(new Vector3(12, 12, 12));

            transform.parent = null;
            Destroy(gameObject, 0.5f);
            this.enabled = false;
        }
        
    }
}
