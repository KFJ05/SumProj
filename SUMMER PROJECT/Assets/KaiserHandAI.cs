using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static DropshipAI;

public class KaiserHandAI : MonoBehaviour
{

    public NavMeshAgent NavMeshAgent;

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

    Transform player;

    [Range(5,25)]
    public float fleeDistance;

    [Range (5,25)]
    public float RunAwayfromPlayerRange;

    public enum KaiserHandStates { Idle, Spawn, Moving, Enraged }

    public KaiserHandStates State;

    [Range(15f, 0f)]
    public float MoveTimer, SpawnTimer;
    float MT, ST;

   

    // Start is called before the first frame update
    void Start()
    {
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
            }
            else
            {
                NavMeshAgent.SetDestination(transform.position);
            }

        }
        if(State == KaiserHandStates.Moving)
        {
            if(R == -1)
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
            int f = -1;

            f = UnityEngine.Random.Range(0, EnemiesSpawned.Length);

            Debug.Log("Spawn");

            GameObject E = Instantiate(EnemiesSpawned[f], transform.position, transform.rotation);

            State = KaiserHandStates.Idle;
        }
    }
}
