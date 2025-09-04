using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class DropshipAI : MonoBehaviour
{
    // Start is called before the first frame update
    public bool WinLevelOnDeath = true;

    [Header("Refrences")]
    public Transform MainBody;

    public Transform Turret;

    public Transform[] FrontThrusters;
    public Transform[] RearThrusters;

    public Transform RightDoor;
    public Transform LeftDoor;

    [Header("TurretSettings")]
    public float TimeBetweenAttacks;
    float tba;

    public GameObject Bullet;
    public GameObject Firelocation;

    [Header("RocketSetttings")]
    public float RocketFireTime;
    float RFT;
    public GameObject rocket;
    public Transform[] RockketSpawnLocations;

    public bool FireRocketOnStart;




    [Header("MovePoints Settings")]
    public Transform[] movePoints;

    public Transform CenterPoint;

    public float NavMeshBaseHeight;

    public int R = -1;

    public float MoveTimer;
    float mt;

    public float turnSpeed;


    [Header("Drop Settings")]
    public bool Dropping;

    public float DropTime;
    float DT;

    [Tooltip("This is the very first drop time, set this lower then drop time")]
    public float FirstDropTime;

    float WeakpointDissapear = 5f;

    bool spawnenemies;

    public float navMeshDropHeight;

    public float NavMeshDropSpeed = 1;

    public Transform DropPoint;

    public GameObject[] EnemiesSpawned;
    public GameObject EnemySpawnLocation;

    [Header("Refrences")]

    public Rigidbody rb;

    public Animator Animator;

    public NavMeshAgent Agent;

    GameObject Player;

    public float detectionAngle = 0f;

    public float TurretTurnSpeed = 1f;
    public bool Fireturret;

    public Canvas HeathBar;

    

    public enum DropShipStates {  Moving, Droping, dead, Idle, MovingAround}

    public DropShipStates state;

    public GameObject WeakPoint;


    private Quaternion baseRotation;

    HealthBarMultiple HPM;

    void Start()
    {
        rb.isKinematic = true;

        WeakPoint.SetActive(false);
        // Right door - Negative;
        // left door - Poistive;

        HPM = gameObject.GetComponent<HealthBarMultiple>();
        EnemySpawnLocation.transform.parent = null;
        EnemySpawnLocation.transform.position -= new Vector3(0, EnemySpawnLocation.transform.position.y, 0);

        if(FirstDropTime > 0)
        {
            mt = FirstDropTime;

            DT = FirstDropTime;
        }
        else if (FirstDropTime <=0)
        {
            mt = MoveTimer;
            DT = DropTime;
        }

        

        
        

        for (int i = 0; i < movePoints.Length; i++)
        {
            if (movePoints[i] != null)
            {
                movePoints[i].position += new Vector3(0, NavMeshBaseHeight, 0);
                movePoints[i].parent = null;
                
            }
        }
        if(DropPoint != null)
        {
            DropPoint.position += new Vector3(0, NavMeshBaseHeight, 0);
            DropPoint.parent = null;
            
        }

        Player = GameObject.FindWithTag("Player");

        baseRotation = Turret.localRotation;

        //CenterPoint.position = new Vector3(CenterPoint.position.x, DropPoint.position.y, CenterPoint.position.z);
        //transform.position += new Vector3(0, NavMeshBaseHeight, 0);

        tba = TimeBetweenAttacks;

        if(FireRocketOnStart == false)
        {
            RFT = RocketFireTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (HPM != null)
        {
            if(HPM.totalHealth <= 0)
            {
                state = DropShipStates.dead;
            }
        }

        if (state == DropShipStates.Droping)
        {
            Animator.SetBool("FF", false);
            RFT = RocketFireTime;
            WeakPoint.SetActive(true);
            Animator.SetBool("OpenDoor", true);

            Fireturret = true;

            Agent.SetDestination(DropPoint.position);

            if (Agent.baseOffset > navMeshDropHeight)
            {
                Agent.baseOffset -= Time.deltaTime * NavMeshDropSpeed;
            }
            if(Agent.baseOffset < navMeshDropHeight)
            {
                Agent.baseOffset = navMeshDropHeight;
            }

            if (Agent.baseOffset == navMeshDropHeight)
            {
                if (spawnenemies == true)
                {
                    Spawn();
                    spawnenemies = false;

                }
            }




            Dropping = true;
        }

        if (state == DropShipStates.Moving)
        {




            Animator.SetBool("Hover", false);
            Animator.SetBool("FF", true);
            

            Fireturret = false;
            spawnenemies = true;
            Dropping = false;

            if (R == -1)
            {
                R = UnityEngine.Random.Range(0, movePoints.Length);

                if (movePoints[R] != null)
                {
                    Agent.SetDestination(movePoints[R].position);
                }
            }
            if (movePoints[R] == null)
            {
                R = -1;
            }
            else if(transform.position.x == movePoints[R].position.x || transform.position.z == movePoints[R].position.z)
            {
                R = -1;
            }

            mt -= Time.deltaTime;
            if (mt <= 0)
            {
                mt = MoveTimer;
                state = DropShipStates.Droping;
            }

            RFT -= Time.deltaTime;
            if(RFT <= 0)
            {
                RFT = RocketFireTime;
                FireRockets();
            }

        }

        if (state == DropShipStates.Idle)
        {
            Animator.SetBool("Hover", true);

            Agent.SetDestination(transform.position);

            DT -= Time.deltaTime;
            if(DT <= 5)
            {
                Animator.SetBool("OpenDoor", false);
                if (WeakPoint.active == true)
                {
                    WeakpointDissapear -= Time.deltaTime;
                    if (WeakpointDissapear <= 0)
                    {
                        WeakPoint.SetActive(false);
                        WeakpointDissapear = 5f;
                    }
                }
            }
            if (DT <= 0)
            {
                DT = DropTime;
                R = -1;
                state = DropShipStates.Moving;
            }
        }

        if(state == DropShipStates.MovingAround)
        {

            float currentX = MainBody.transform.eulerAngles.x;
            float targetX = 15f; // the cap you want

            // Smoothly rotate towards the stop limit
            float newX = Mathf.LerpAngle(currentX, targetX, Time.deltaTime * turnSpeed);

            MainBody.transform.rotation = Quaternion.Euler(newX, MainBody.transform.eulerAngles.y, MainBody.transform.eulerAngles.z);

        }


        if (Dropping == false)
        {
            if (Agent.baseOffset < NavMeshBaseHeight)
            {
                Agent.baseOffset += Time.deltaTime * NavMeshDropSpeed;
            }
            else if (Agent.baseOffset > NavMeshBaseHeight)
            {
                Agent.baseOffset = NavMeshBaseHeight;
            }
        }



        if (Fireturret == true)
        {
            Vector3 toPlayer = (Player.transform.position - transform.position).normalized;

            // Work in the horizontal plane only
            toPlayer.y = 0;
            Vector3 forward = transform.forward;
            forward.y = 0;

            // Signed angle between forward and player
            float angleToPlayer = Vector3.SignedAngle(forward, toPlayer, Vector3.up);

            // Clamp current turret angle
            float currentAngle = Turret.localEulerAngles.z;
            if (currentAngle > 180) currentAngle -= 360; // convert to -180..180


            // Define allowed range (±90 for example)
            float minAngle = -90f;
            float maxAngle = 90f;

            if (angleToPlayer > 1f && currentAngle < maxAngle)
            {
                Turret.Rotate(0, 0, TurretTurnSpeed * Time.deltaTime);
            }
            else if (angleToPlayer < -1f && currentAngle > minAngle)
            {
                Turret.Rotate(0, 0, -TurretTurnSpeed * Time.deltaTime);
            }
            tba -= Time.deltaTime;
            if(tba <=0)
            {
                tba = TimeBetweenAttacks;

                GameObject B = Instantiate(Bullet, Firelocation.transform.position, Quaternion.identity);
            }
        }
        else
        {
            Turret.localRotation = Quaternion.RotateTowards(
           Turret.localRotation,
           baseRotation,
           TurretTurnSpeed * Time.deltaTime);

            tba = TimeBetweenAttacks;
        }

        if (state == DropShipStates.dead)
        {
            Fireturret = false;

            Agent.enabled = false;

            rb.isKinematic = false;

            this.enabled = false;

            Destroy(gameObject, 10f);

            HeathBar.enabled = false;

            if (EnemyManager.Instance != null)
            {
                EnemyManager.Instance.RemoveEnemy(gameObject);
            }

            if (WinLevelOnDeath == true)
            {
                Victory V = GameObject.FindWithTag("Player").GetComponent<Victory>();

                V.SetWin();

            }

        }
    }

    public void Spawn()
    {
        int f = -1;

        f = UnityEngine.Random.Range(0, EnemiesSpawned.Length);

        Debug.Log("Spawn");

        GameObject E = Instantiate(EnemiesSpawned[f], EnemySpawnLocation.transform.position, EnemySpawnLocation.transform.rotation );

        state = DropShipStates.Idle;
    }

    public void FireRockets()
    {
        if (RockketSpawnLocations.Length > 0)
        {
            for (int i = 0; i < RockketSpawnLocations.Length; i++)
            {
                if (RockketSpawnLocations[i] != null)
                {
                    GameObject Rocket = Instantiate(rocket, RockketSpawnLocations[i].position, RockketSpawnLocations[i].rotation);
                }
            }
        }
    }
}
