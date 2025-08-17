using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gunship : MonoBehaviour
{
    public enum GunShipStates { MovingToPoint, Attacking, Dead }
    public GunShipStates State;

    [Header("Prefabs")]
    public NavMeshAgent Agent;
    public GameObject Bullets;
    public GameObject Rocket;
    public float RocketTimer;
    float RT;
    public Health health;
    public Rigidbody body;
    public Transform RocketSpawn;

    public Transform[] Movepoints;
    public float YHeight;
    int r = -1;

    GameObject PLayer;

    [Header("Sheild Intrerval")]
    public float[] Intervals;

    int counter  = 1;

    public float attackTimer;
    float AT;

    public float AttackDistance;


    private void Start()
    {
        PLayer = GameObject.FindWithTag("Player");

        body.isKinematic = true;
        for(int i = 0; i < Movepoints.Length; i++)
        {
            Movepoints[i].position += new Vector3(0, YHeight, 0);
            Movepoints[i].parent = null;
        }

        AT = attackTimer;
    }


    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(health.CurrentHealth <= 0)
        {
            State = GunShipStates.Dead;
        }
        if (counter <= Intervals.Length)
        {
            if (health.CurrentHealth <= Intervals[Intervals.Length - counter])
            {
                health.SheildActive = true;
                counter++;
            }
        }
        if(State == GunShipStates.MovingToPoint)
        {
            Agent.Resume();
            if (r == -1)
            {
                r = UnityEngine.Random.Range(0, Movepoints.Length);
                if (Movepoints[r] != null)
                {
                    Agent.SetDestination(Movepoints[r].position);
                }
            }
            if (r != -1)
            {
                if (Vector3.Distance(transform.position, Movepoints[r].position) <= 10)
                {
                    State = GunShipStates.Attacking;
                }
            }
        }
        if(State == GunShipStates.Attacking)
        {
            Agent.Stop();
            transform.LookAt(PLayer.transform.position);
            AT -= Time.deltaTime;
            if(AT <= 0)
            {
                AT = attackTimer;
                State = GunShipStates.MovingToPoint;
                r = -1;
            }
            RT -= Time.deltaTime;
            if(RT <= 0)
            {
                RT = RocketTimer;
                GameObject G = Instantiate(Rocket, RocketSpawn.position, RocketSpawn.rotation);
            }
        }


        if(State == GunShipStates.Dead)
        {
            Destroy(gameObject, 3);

            Agent.enabled = false;
            body.isKinematic = false;
            body.AddTorque(50, 50, 50);

            this.enabled = false;


        }


    }
    

}
