using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;
using static DropshipAI;

public class KaiserHandAI : MonoBehaviour
{

    public NavMeshAgent NavMeshAgent;

    public Animator animator;


    public GameObject OfficerCoat;
    public GameObject OfficerHat;
    public GameObject OfficerS_Pads;

    public int R = -1;

    public float[] HealthTeirValues;
    public float[] SheildTierValues;

    public GameObject[] EnemiesSpawned;

    public HealthBarMultiple HBM;

    public GameObject[] MovePoints;

    public GameObject SpawnPoint;

    [Header("HealthIntervals")]

    public float[] HealthIntervals;

    int Counter = 1;
    public GameObject[] intervalSpawn;


    Transform player;

    [Range(5,25)]
    public float fleeDistance;

    [Range (5,25)]
    public float RunAwayfromPlayerRange;

    public enum KaiserHandStates { Idle, Spawn, Moving, Enraged, dead }

    public KaiserHandStates State;

    [Range(5f, 120f)]
    public float MoveTimer, SpawnTimer;
    float MT, ST;


    [Header("Drones Settings")]
    public float DroneSummonTime;
    public float DST;
    public GameObject[] DroneSpawnPoints;
    public GameObject Drone;

   

    // Start is called before the first frame update
    void Start()
    {
        DST = DroneSummonTime;
        R = -1;
        MT = MoveTimer;
        ST = SpawnTimer;

        if (SpawnPoint != null)
        {
            transform.position = SpawnPoint.transform.position;
        }

        for (int i = 0; i < MovePoints.Length; i++)
        {
            if (MovePoints[i] != null)
            {
                MovePoints[i].transform.parent = null;
            }
        }
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        DST -= Time.deltaTime;
        if(DST <= 0)
        {
            DST = DroneSummonTime;
            SummonDrone();
        }


        if(Counter <= HealthIntervals.Length)
        {
            if(HBM.totalHealth <= HealthIntervals[HealthIntervals.Length - Counter])
            {
                if (intervalSpawn[intervalSpawn.Length -  Counter] != null)
                {
                    GameObject E = Instantiate(intervalSpawn[intervalSpawn.Length - Counter], transform.position, transform.rotation);
                }

                if(HBM.totalHealth <= 225)
                {
                    OfficerS_Pads.SetActive(false);
                }
                if(HBM.totalHealth <= 150)
                {
                    OfficerHat.SetActive(false);
                }
                if(HBM.totalHealth <= 75)
                {
                    OfficerCoat.SetActive(false);
                }
             

                Counter++;
            }


        }

        if(HBM.totalHealth <= 0)
        {
            State = KaiserHandStates.dead;
        }



        if (State == KaiserHandStates.Idle)
        {
            ST -= Time.deltaTime;
            MT -= Time.deltaTime;
            if(ST <= 0)
            {
                ST = SpawnTimer;
                State = KaiserHandStates.Spawn;
            }
            if(MT <= 0)
            {
                State = KaiserHandStates.Moving;
                MT = MoveTimer;
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= RunAwayfromPlayerRange)
            {
                Vector3 fleeDirection = (transform.position - player.position).normalized;

                // Target position to run to
                Vector3 newGoal = transform.position + fleeDirection * fleeDistance;

                NavMeshHit hit;
                if (NavMesh.SamplePosition(newGoal, out hit, 5f, NavMesh.AllAreas))
                {
                    NavMeshAgent.SetDestination(hit.position);
                }
                animator.SetBool("Running", true);
            }
            else
            {
                NavMeshAgent.SetDestination(transform.position);
                animator.SetBool("Running", false);
            }
        }
        if(State == KaiserHandStates.Moving)
        {
            animator.SetBool("Running", true);

            if (R == -1)
            {
                R = Random.Range(0, MovePoints.Length);
                if (MovePoints[R] != null)
                {
                    NavMeshAgent.SetDestination(MovePoints[R].transform.position);

                }
            }
            if (Vector3.Distance(transform.position, MovePoints[R].transform.position) < 5f)
            {
                R = -1;
                State = KaiserHandStates.Idle;
            }
        }

        if(State == KaiserHandStates.Spawn)
        {
            animator.SetBool("Running", false);
            int f = -1;

            f = UnityEngine.Random.Range(0, DroneSpawnPoints.Length);

            Debug.Log("Spawn");

            if (Counter <= EnemiesSpawned.Length)
            {
                GameObject E = Instantiate(EnemiesSpawned[EnemiesSpawned.Length - Counter], DroneSpawnPoints[f].transform.position, transform.rotation);
            }
            else
            {
                GameObject E = Instantiate(EnemiesSpawned[0], transform.position, transform.rotation);
            }

                State = KaiserHandStates.Idle;
        }

        if (State == KaiserHandStates.dead)
        {

            Destroy(gameObject, 5);
            Victory V = GameObject.FindWithTag("Player").GetComponent<Victory>();

            EnemyManager.Instance.RESETALL();

            V.SetWin();
            
            this.enabled = false;

            //Destroy(gameObject, 5);


        }
    }

    public void SummonDrone()
    {
        for(int i = 0; i < DroneSpawnPoints.Length; i++)
        {
            if (DroneSpawnPoints[i] != null)
            {
                if (DroneSpawnPoints[i].transform.childCount == 0)
                {
                    GameObject D = Instantiate(Drone, DroneSpawnPoints[i].transform.position, Quaternion.identity, DroneSpawnPoints[i].transform);
                    break;
                }
            }
        }
    }
}
