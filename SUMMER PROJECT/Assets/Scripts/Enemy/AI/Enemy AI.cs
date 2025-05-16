using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform Player;

    public bool moveToPoints;

    public string[] GameObjectNames;


    

    //Attacking
    public float TBA;

    //states
    public float SightRange, AttackRange;

    public bool CallFire;

    public TurretAI[] Turrets;

    public LayerMask PlayerLayer, whatisGround;

    bool playerinSightRange, playerinAttackRange;


    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();

        if(CallFire)
        {
            for(int i = 0; i < Turrets.Count(); i++)
            {
                if (Turrets[i] != null)
                {
                    Turrets[i].FireFunctionCalledElsewhere = true;
                }
            }
        }

        
    }



    private void Update()
    { 
        
        playerinSightRange = Physics.CheckSphere(transform.position, SightRange, PlayerLayer);
        playerinAttackRange = Physics.CheckSphere(transform.position, AttackRange, PlayerLayer);

        if (playerinSightRange && !playerinAttackRange)
        {
            moveToPlayer();
            CancelInvoke();
        }
        else if(playerinAttackRange && playerinSightRange)
        {
            stopmoving();
            Invoke(nameof(Fire), TBA);
        }
        else if (!playerinSightRange && !playerinAttackRange)
        {
            stopmoving();
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

    public void Fire()
    {
        for(int i = 0; i < Turrets.Count(); i++)
        {
            if (Turrets[i] != null)
            {
                Turrets[i].FireTurrBullet();
            }
        }
    }




}
