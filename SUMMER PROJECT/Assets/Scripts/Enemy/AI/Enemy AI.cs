using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform Player;

    public bool moveToPoints;

    public string[] GameObjectNames;



    bool Fired;

    //Attacking
    public float TBA;

    //states
    public float SightRange, AttackRange;

    

    public LayerMask PlayerLayer, whatisGround;

    bool playerinSightRange, playerinAttackRange;

    [Header("Refrences")]
    public GameObject[] turrets;

    public Health HP;

    public Die EnemyDeath;



    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();



        
    }



    private void Update()
    { 
        
        playerinSightRange = Physics.CheckSphere(transform.position, SightRange, PlayerLayer);
        playerinAttackRange = Physics.CheckSphere(transform.position, AttackRange, PlayerLayer);

        if (playerinSightRange && !playerinAttackRange)
        {
            moveToPlayer();
            //CancelInvoke();
        }
        else if(playerinAttackRange && playerinSightRange)
        {
            stopmoving();
            //Fire();
           // Fired = false;
 

        }
       // else if (playerinAttackRange && playerinSightRange && Fired == false)
      //  {
             //Invoke(nameof(ResetFire), TBA);
       // }
        else if (!playerinSightRange && !playerinAttackRange)
        {
            stopmoving();
        }
        if(HP.CurrentHealth <= 0)
        {
            for(int i = 0; i < turrets.Count(); i++)
            {
                TurretAI T = turrets[i].gameObject.GetComponent<TurretAI>();
                if (T != null)
                {
                    T.enabled = false;
                }
                Die D = turrets[i].GetComponent<Die>();
                if(D != null)
                {
                    D.TriggerDeath = true;
                }
            }

            EnemyDeath.TriggerDeath = true;
        }

        
        

       // agent.destination = Player.position;
    }



    public void moveToPlayer()
    {
        agent.SetDestination(Player.position);
    }

    public void stopmoving()
    {
        agent.SetDestination(transform.position);
    }

    public void MovetoSpot()
    {

    }



}
