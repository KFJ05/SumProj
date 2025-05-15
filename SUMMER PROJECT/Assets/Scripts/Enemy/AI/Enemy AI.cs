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


    //Patrolling
    public Vector3 walkPoint;
    bool walkpointSet;
    public float walkPointRange;

    //Attacking
    public float TBA;

    //states
    public float SightRange, AttackRange;

    public bool CallFire;

    public TurretAI[] Turrets;


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
        if(Vector3.Distance(transform.position, Player.position) <= SightRange && Vector3.Distance(transform.position, Player.position) > AttackRange)
        {
            moveToPlayer();
            CancelInvoke();
        }
        else if(Vector3.Distance(transform.position, Player.position) <= AttackRange)
        {
            stopmoving();
            Invoke(nameof(Fire), TBA);
        }
        else if (Vector3.Distance(transform.position, Player.position) > SightRange)
        {
            stopmoving();
        }
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
