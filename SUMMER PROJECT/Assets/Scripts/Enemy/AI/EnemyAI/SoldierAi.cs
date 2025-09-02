using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using UnityEngine.UIElements;

public class SoldierAi : MonoBehaviour
{
    // Start is called before the first frame update

    //-0.0005700042 0.01324002 0.001749987 7.054 18.854 90.172 0.015 0.002 0.002000001


    public Transform GunTr;

    public GameObject Gun;
    public Transform firelocation;

    [Range(20.0f, 60.0f)]
    public float detectionAngle = 45f;

    public float RotateSpeed;

    public GameObject bullet;

    GameObject Player;

    public bool usingPartice;

    public ParticleSystem particleSystem;

    public NavMeshAgent Agent;

    bool ReadyToPullGun;

    public float Spread;

    public float shootF;

    public float TimeBetweenShots;

    public bool UseFireTime = false;

    public float FireTime = 10;

    public float MoveTime = 7.5f;

    float ft;

    float Tba;

    float Angle;

    Vector3 V3;

    public Animator Animator;

    public float MinDistanceAwayAToAim,MaxDistanceToRun;

    public MultiAimConstraint[] Contraints;

    public MultiAimConstraint Shoulder;

    public RigBuilder RigBuilder;

    public Health Health;
    public HealthBarMultiple HealthBarMultiple;
    public bool usingMultipleHealthBars;

    public Transform[] transforms;

    bool Move;
    bool MovementOverRide;

    float timer;

    int R = -1;

    public
    enum SoldierState {Aiming, Running, Shooting, Dead}

    public SoldierState State;

    private void Start()
    {

        pointList PL = GameObject.FindWithTag("PointList").GetComponent<pointList>();

        transforms = PL.PointTransforms;

        if (PL != null)
        {
            MovementOverRide = true;
            timer = MoveTime;
        }

        int b = UnityEngine.Random.RandomRange(0,3);
        if(b == 0)
        {
            if(usingMultipleHealthBars == false)
            {
                Health.SheildActive = true;
            }
        }

        Player = GameObject.FindWithTag("Player");

        var newSourceArray = new WeightedTransformArray { new WeightedTransform(Player.transform, 1f)};

        for (int i = 0; i < Contraints.Length; i++)
        {
            Contraints[i].data.sourceObjects = newSourceArray;
        }
        RigBuilder.Build();

        Tba = TimeBetweenShots;

    }


    // Update is called once per frame
    void Update()
    {
        if (PauseManager.Instance != null)
        {
            if (PauseManager.Instance.IsPaused == false)
            {

                if (transforms.Count() == 0)
                {
                    pointList PL = GameObject.FindWithTag("PointList").GetComponent<pointList>();

                    transforms = PL.PointTransforms;
                }

                if (!usingMultipleHealthBars)
                {
                    if (Health.CurrentHealth <= 0)
                    {
                        State = SoldierState.Dead;
                    }
                }
                if (usingMultipleHealthBars)
                {
                    if (HealthBarMultiple.totalHealth <= 0)
                    {
                        State = SoldierState.Dead;
                    }
                }


                if (State == SoldierState.Running)
                {
                  
                    if (UseFireTime)
                    {
                        ft = FireTime;
                    }
                    if (timer > 0)
                    {
                        timer -= Time.deltaTime;
                    }
                    else if (timer <= 0)
                    {
                        MovementOverRide = false;
                    }

                    Animator.SetLayerWeight(1, 0);
                    Gun.SetActive(false);
                    ReadyToPullGun = true;
                    Shoulder.weight = 0;

                    if (MovementOverRide == true)
                    {
                        if (R == -1)
                        {
                            R = UnityEngine.Random.Range(0, transforms.Length);
                        }
                        if (transforms[R] != null)
                        {
                            Agent.SetDestination(transforms[R].position);
                        }

                        if (Vector3.Distance(transform.position, transforms[R].position) <= 2)
                        {
                            timer = 0;
                        }
                    }
                    else if (Vector3.Distance(Player.transform.position, transform.position) > MinDistanceAwayAToAim)
                    {
                        if (MovementOverRide == false)
                        {
                            Agent.SetDestination(Player.transform.position);
                        }
                    }
                    else if (Vector3.Distance(Player.transform.position, transform.position) <= MinDistanceAwayAToAim)
                    {
                        if (MovementOverRide == false)
                        {
                            Agent.SetDestination(transform.position);
                            State = SoldierState.Aiming;
                        }
                    }

                    Tba = TimeBetweenShots;

                    Vector3 toPlayer = (Player.transform.position - transform.position).normalized;
                    Vector3 forward = transform.forward;

                    Vector3 direction = Player.transform.position - transform.position;
                    float dotProduct = Vector3.Dot(direction, transform.right);

                    float angle = Vector3.Angle(forward, toPlayer);
                    float TempAngle = 0;

                    if (dotProduct > 0)
                    {
                        TempAngle = -angle;
                    }
                    else if (dotProduct < 0)
                    {
                        TempAngle = angle;
                    }

                    if (TempAngle > detectionAngle || TempAngle < -detectionAngle)
                    {
                        TurnOffContraints();
                    }
                    else
                    {
                        TurnOnContraints();
                    }

                    /*
                    if(Move == true)
                    {
                        Transform P;
                        float[] XL = { MinDistanceAwayAToAim, -MinDistanceAwayAToAim };
                        float[] XZ = { MinDistanceAwayAToAim, -MinDistanceAwayAToAim };

                        var HowManyTX = XL.Length;
                        var howmanyTZ = XL.Length;

                        var RX = UnityEngine.Random.Range(0, HowManyTX);
                        var RZ = UnityEngine.Random.Range(0, howmanyTZ);

                        float A = RX == 0 ? 0 : 1;
                        Debug.Log(A);

                        Agent.SetDestination(Player.transform.position + new Vector3(RX,0,RZ));

                        V3 = Player.transform.position + new Vector3(RX, 0, RZ);

                        Move = false;
                    }

                    Vector3 toPlayer = (Player.transform.position - transform.position).normalized;
                    Vector3 forward = transform.forward;

                    Vector3 direction = Player.transform.position - transform.position;
                    float dotProduct = Vector3.Dot(direction, transform.right);

                    float angle = Vector3.Angle(forward, toPlayer);
                    float TempAngle = 0;

                    if (dotProduct > 0)
                    {
                        TempAngle = -angle;
                    }
                    else if (dotProduct < 0)
                    {
                        TempAngle = angle;
                    }

                    if (TempAngle > detectionAngle)
                    {
                       Move = true;
                    }
                    if (TempAngle < -detectionAngle)
                    {
                       Move = true; 
                    }*/



                    //ai Function here
                }
                if (State == SoldierState.Aiming)
                {
                    if (MovementOverRide == true)
                    {
                        //int A = UnityEngine.Random.Range(0, 5);

                        MovementOverRide = true;

                        timer = MoveTime;
                        State = SoldierState.Running;
                        R = -1;
                        Move = true;
                    }
                    if (UseFireTime)
                    {
                        ft -= Time.deltaTime;
                    }
                    if (ft <= 0 && UseFireTime == true)
                    {
                        int A = UnityEngine.Random.Range(0, 5);

                        MovementOverRide = true;

                        timer = MoveTime;
                        State = SoldierState.Running;
                        R = -1;
                        Move = true;
                    }
 

                    Animator.SetLayerWeight(1, 1);
                    Gun.SetActive(true);
                    if (ReadyToPullGun == true)
                        StartCoroutine(PulloutGun());

                    if (Vector3.Distance(Player.transform.position, transform.position) > MaxDistanceToRun)
                    {
                        int A = UnityEngine.Random.Range(0, 5);

                        MovementOverRide = true;

                        timer = MoveTime;
                        State = SoldierState.Running;
                        R = -1;
                        Move = true;
                    }
                    else if (Tba <= 0)
                    {
                        State = SoldierState.Shooting;
                    }
                    Tba -= Time.deltaTime;

                    Vector3 toPlayer = (Player.transform.position - transform.position).normalized;
                    Vector3 forward = transform.forward;

                    Vector3 direction = Player.transform.position - transform.position;
                    float dotProduct = Vector3.Dot(direction, transform.right);

                    float angle = Vector3.Angle(forward, toPlayer);
                    float TempAngle = 0;

                    if (dotProduct > 0)
                    {
                        TempAngle = -angle;
                    }
                    else if (dotProduct < 0)
                    {
                        TempAngle = angle;
                    }

                    if (TempAngle > detectionAngle)
                    {
                        transform.Rotate(new Vector3(0, -RotateSpeed, 0), Space.Self);
                    }
                    if (TempAngle < -detectionAngle)
                    {
                        transform.Rotate(new Vector3(0, RotateSpeed, 0), Space.Self);
                    }

                    Agent.SetDestination(transform.position);
                }
                if (State == SoldierState.Shooting)
                {
                    if (usingPartice)
                    {
                        particleSystem.Play();

                    }
                    if (!usingPartice)
                    {

                        Vector3 DirWithoutSpread = firelocation.position - Gun.transform.position;



                        float x = UnityEngine.Random.Range(-Spread, Spread);
                        float y = UnityEngine.Random.Range(-Spread, Spread);


                        Vector3 FireDir = DirWithoutSpread + new Vector3(x, y, 0);


                        GameObject CurrBullet = Instantiate(bullet, firelocation.position, Quaternion.identity);

                        CurrBullet.transform.forward = FireDir;

                        CurrBullet.GetComponent<Rigidbody>().AddForce(FireDir.normalized * shootF, ForceMode.Impulse);


                    }
                    Tba = TimeBetweenShots;
                    State = SoldierState.Aiming;
                }
                if (State == SoldierState.Dead)
                {

                }
            }
        }
 
    }

    IEnumerator PulloutGun()
    {
        for (int i = 0; i < Contraints.Length; i++)
        {
            Contraints[i].weight = 0;
        }
        Gun.transform.position = GunTr.position;
        Gun.transform.rotation = GunTr.rotation;

        yield return new WaitForSecondsRealtime(0.001f);

        for (int i = 0; i < Contraints.Length; i++)
        {
             Contraints[i].weight = 1;
        }
        ReadyToPullGun = false;
    }

    public void TurnOffContraints()
    {
        for (int i = 0; i < Contraints.Length; i++)
        {
            Contraints[i].weight = 0;
        }
    }
    public void TurnOnContraints()
    {
        for (int i = 0; i < Contraints.Length; i++)
        {
            Contraints[i].weight = 1;
        }
    }
}



