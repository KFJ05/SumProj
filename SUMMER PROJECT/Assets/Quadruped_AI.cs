using JetBrains.Annotations;
using Lolopupka;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Quadruped_AI : MonoBehaviour
{

    [Header("Refrences")]
    public GameObject[] Turrets;

    Health QuadHP;

    public Transform MainTurret;

    public proceduralAnimation ProAnim;
    public ProceduralBodyController ProBody;


    GameObject Player;

    [Header("MovementOptions")]
    public float DistanceToStand;
    public float speed;

    public float TimeToDestroyLegs;

    float D;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");

        D = Vector3.Distance(gameObject.transform.position, Player.transform.position);
    }

    

    // Update is called once per frame


    private void Update()
    {
        if (QuadHP == null)
        {
            QuadHP = gameObject.GetComponent<Health>();
        }

        if (D > DistanceToStand && QuadHP.CurrentHealth > 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, MainTurret);
        }






        if (QuadHP != null && QuadHP.CurrentHealth <= 0)
        {

            for (int i = 0; i < Turrets.Count(); i++)
            {
                if (Turrets[i] != null)
                {
                    TurretAI T = Turrets[i].gameObject.GetComponent<TurretAI>();
                    if (T != null)
                    {
                        T.enabled = false;
                    }
                    Die D = Turrets[i].gameObject.GetComponent<Die>();

                    if (D != null)
                    {
                        D.TriggerDeath = true;
                    }
                }


            }

            EnemyManager.Instance.RemoveEnemy(gameObject);

            ProAnim.enabled = false;
            ProBody.enabled = false;

            Invoke(nameof(DestroyLegs), TimeToDestroyLegs);
        }

      
    }

    public void DestroyLegs()
    {
        Destroy(gameObject);
    }

}

