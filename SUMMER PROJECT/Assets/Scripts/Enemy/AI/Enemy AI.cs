using Lolopupka;
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

    public string[] Movespots;

    List<GameObject> MoveL = new List<GameObject>(); 


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

    public GameObject body;

    public bool getAgent = true;




    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").transform;

        if (getAgent)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        for(int i = 0; i < Movespots.Count(); i++)
        {
            GameObject MoveLoc = GameObject.Find(Movespots[i]);
            if (MoveLoc != null)
            {
                MoveL.Add(MoveLoc);
            }
        }
        
    }



    private void Update()
    {

        if (moveToPoints != true)
        {
            playerinSightRange = Physics.CheckSphere(transform.position, SightRange, PlayerLayer);
            playerinAttackRange = Physics.CheckSphere(transform.position, AttackRange, PlayerLayer);

            if (playerinSightRange && !playerinAttackRange)
            {
                moveToPlayer();
                //CancelInvoke();
            }
            else if (playerinAttackRange && playerinSightRange)
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
            if(HP != null)
                CheckHealth();

        }
        else
        {
            for(int i =0; i < MoveL.Count; i++)
            {
                if (MoveL[i] == null)
                {
                    i = 0;
                }

                if (body.transform.position.x == MoveL[i].transform.position.x && body.transform.position.z == MoveL[i].transform.position.z)
                {
                    Debug.Log("Move");


                    if (i + 1 < MoveL.Count)
                    {
                        MovetoSpot(MoveL[i + 1].transform);
                    }
                    else
                    {
                        MovetoSpot(MoveL[0].transform);
                    }
                 
                }
            }
            if(HP != null)
                CheckHealth();
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

    public void MovetoSpot(Transform trans)
    {
        Vector3 position = new Vector3(trans.position.x, body.transform.position.y, trans.position.z);

        agent.SetDestination(position);
    }

    public void CheckHealth()
    {
        if (HP.CurrentHealth <= 0)
        {
            for (int i = 0; i < turrets.Count(); i++)
            {
                TurretAI T = turrets[i].gameObject.GetComponent<TurretAI>();
                if (T != null)
                {
                    T.enabled = false;
                }
                Die D = turrets[i].GetComponent<Die>();
                if (D != null)
                {
                    D.TriggerDeath = true;
                }
            }


            

            EnemyDeath.TriggerDeath = true;

            
        }
    }

    public void DestroyTurrets()
    {
        for (int i = 0; i < turrets.Count(); i++)
        {
            TurretAI T = turrets[i].gameObject.GetComponent<TurretAI>();
            if (T != null)
            {
                T.enabled = false;
            }
            Die D = turrets[i].GetComponent<Die>();
            if (D != null)
            {
                D.TriggerDeath = true;
            }

        }
    }

 }
