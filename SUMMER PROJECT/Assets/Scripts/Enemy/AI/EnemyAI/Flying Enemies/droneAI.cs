using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class droneAI : MonoBehaviour
{
    // Start is called before the first frame update

    public float YHeight;


    public NavMeshAgent Agent;
    public quaternion AngleOffset;
    quaternion BaseAngle;
    bool changeangle;

    public bool pointchosen;
    public Transform[] Movetransforms;

    public GameObject Body;
    int r;
    public float TimeToStandStill;
    float SST;

    public enum DroneStates { moving, inplace, dead}
    public DroneStates states;

    public float RotateAngle;
    [Range(0f, 90f)]
    public float stopLimit;

    public float DampingSpeed = 2f;

    public float BobbingLimit;
    public float BobbingSpeed;
    float B_Offset;
    bool upOrDown;

    public bool Bomber;

    public float BomberTimer;
    float Btime;

    public bool MultipleBombs;
    public float NewBombTimer;
    float NBT;

    bool droppedBomb;

    public GameObject Bomb;
    public GameObject VisualBomb;

    public Rigidbody rb;

    Health health;


    void Start()
    {
        if(Bomber == true)
        {
            Btime = BomberTimer;
            droppedBomb = true;
        }

        DronePointList d = GameObject.FindWithTag("DronePointList").gameObject.GetComponent<DronePointList>();

        Movetransforms = d.PointTransforms;

        if (Body != null)
        {
            BaseAngle = Body.transform.rotation;
        }
        else
        {
            BaseAngle = gameObject.transform.rotation;
        }
        SST = TimeToStandStill;
        rb.isKinematic = true;

        health = gameObject.GetComponent<Health>();

        upOrDown = false;

        B_Offset = Agent.baseOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if(Bomber == true)
        {
            Btime -= Time.deltaTime;

            if(Btime <= 0 && droppedBomb == true)
            {
                VisualBomb.SetActive(false);
                GameObject B = Instantiate(Bomb, VisualBomb.transform.position, VisualBomb.transform.rotation);

                droppedBomb = false;



            }
            else if(droppedBomb == false && MultipleBombs == true)
            {
                NBT += Time.deltaTime;
                if(NBT >= NewBombTimer)
                {
                    Btime = BomberTimer;
                    NBT = 0;
                    droppedBomb = true;
                    VisualBomb.SetActive (true);
                }
            }
        }

        if (PauseManager.Instance != null)
        {
            if (PauseManager.Instance.IsPaused == false)
            {

                if (upOrDown == false)
                {
                    if (Agent.baseOffset >= B_Offset + BobbingLimit)
                    {
                        upOrDown = true;
                    }
                    Agent.baseOffset += Time.deltaTime * BobbingSpeed;

                }
                else if (upOrDown == true)
                {
                    if (Agent.baseOffset <= B_Offset - BobbingLimit)
                    {
                        upOrDown = false;
                    }
                    Agent.baseOffset -= Time.deltaTime * BobbingSpeed;
                }



                if (health.CurrentHealth <= 0)
                {
                    states = DroneStates.dead;
                }

                if (states == DroneStates.moving)
                {


                    float currentX = Body.transform.eulerAngles.x;
                    float targetX = stopLimit; // the cap you want

                    // Smoothly rotate towards the stop limit
                    float newX = Mathf.LerpAngle(currentX, targetX, Time.deltaTime * DampingSpeed);

                    Body.transform.rotation = Quaternion.Euler(newX, Body.transform.eulerAngles.y, Body.transform.eulerAngles.z);

                    Agent.Resume();
                    if (pointchosen == false)
                    {


                        r = UnityEngine.Random.Range(0, Movetransforms.Length);
                        if (Movetransforms[r] != null)
                        {
                            Agent.SetDestination(Movetransforms[r].position);
                        }
                        pointchosen = true;
                    }
                    if (Vector3.Distance(transform.position, Movetransforms[r].position) <= 10)
                    {
                        states = DroneStates.inplace;
                    }
                }
                if (states == DroneStates.inplace)
                {
                    float currentX = Body.transform.eulerAngles.x;
                    float targetX = 0f; // back to zero angle

                    float newX = Mathf.LerpAngle(currentX, targetX, Time.deltaTime * DampingSpeed);

                    Body.transform.rotation = Quaternion.Euler(newX, Body.transform.eulerAngles.y, Body.transform.eulerAngles.z);

                    Agent.Stop();
                    SST -= Time.deltaTime;
                    if (SST <= 0)
                    {
                        SST = TimeToStandStill;
                        states = DroneStates.moving;
                        pointchosen = false;
                    }

                }
                if (states == DroneStates.dead)
                {

                    Agent.Stop();
                    Agent.enabled = false;
                    rb.isKinematic = false;
                    rb.AddTorque(new Vector3(12, 12, 12));
                    if (EnemyManager.Instance != null)
                    {
                        EnemyManager.Instance.RemoveEnemy(gameObject);
                    }
                    Destroy(gameObject, 3);
                    this.enabled = false;
                }
            }
        }

    }
}
