using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;

public class BlasPhosAI : MonoBehaviour
{
    // Start is called before the first frame update


    [Header("BaseStats")]

    public float WaitTime;
    float Wt;

    public float FireTime;
    float Ft;

    public bool UseFire;

    public float Spread;

    public float shootF;
    public GameObject bullet;

    public GameObject Gun;
    public Transform firelocation;

    public NavMeshAgent agent;
    [Range(0f, 120f)]
    public float SpecialAttackTimer;

    float S_A_T;

    [Range(0f, 30f)]
    public float postSpecialAttackWaitTime;

    [Range(-100f, 100f)]
    public float speed = 10;

    [Range(0f, 15f)]
    public float swapSpeedCounter = 10f;

    public GameObject[] Movepoints;

    public GameObject Centralpoint;
    public float MaxDisFromCentralPoint;

    GameObject Storedpos;

    bool PreformSpecialAttack;

    [Header("Prefabs")]
    public GameObject Meteor;
    public GameObject GigaMeteor;

    public ParticleSystem FireAttack;
    public GameObject[] FireSpots;
    bool locationSelected;
    public float SweepTime;
    float TimeElapsed;
    int f;




    public ParticleSystem FireRing;

    HealthBarMultiple Hbm;

    [Header("Phase2")]
    public float Phase2HealthLimit;
    public bool Phase2Start = false;

    public float MeteorTime;
    float MT;

    [Header("AnimatorSettings")]
    public Animator animator;
    public string[] AnimatorVariableNames = {"Speed", "Charge", "Idle"};

    [Header("TeleportSettings")]
    public ParticleSystem[] teleportObj;

    public enum BlasphosState { Moving, ChangePosition, Attacking, SpecialAttack, Dead, Waiting }

    public BlasphosState State;

    public bool TeleportToCentralPoint;




    // public BlasphosAttacks
    private void Start()
    {
        for (int i = 0; i < Movepoints.Length; i++)
        {
            if (Movepoints[i] != null)
            {
                Movepoints[i].transform.SetParent(null);
            }
        }
        Centralpoint.transform.SetParent (null);

        StartCoroutine(SwapSpeed());
        S_A_T = SpecialAttackTimer;
        Wt = WaitTime;
        State = BlasphosState.Waiting;


        Ft = FireTime;
        Hbm = gameObject.GetComponent<HealthBarMultiple>();



        // StartCoroutine(preformSpecialAttack());
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.Instance != null)
        {
            if (PauseManager.Instance.IsPaused == false)
            {


                if (Phase2Start == false)
                {
                    if (Hbm.totalHealth <= Phase2HealthLimit)
                    {
                        Phase2Start = true;
                    }
                }

                if (Hbm.totalHealth <= 0)
                {
                    State = BlasphosState.Dead;
                    FireRing.Stop();
                    FireAttack.Stop();
                }
                if (State == BlasphosState.Waiting)
                {
                    Wt -= Time.deltaTime;
                    if (Wt <= 0)
                    {
                        State = BlasphosState.Moving;
                    }
                }
                if (State == BlasphosState.Moving)
                {

                    if (UseFire == false)
                    {
                        BlasphosFire();
                    }
                    else if (UseFire == true)
                    {
                        Ft -= Time.deltaTime;
                        if (Ft <= 0)
                        {
                            FireBullet();
                        }
                    }
                    //animation          
                    agent.Stop();
                    S_A_T -= Time.deltaTime;

                    transform.LookAt(GameObject.FindWithTag("Player").transform);
                    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0), Space.Self);

                    if (Phase2Start == true)
                    {
                        MT -= Time.deltaTime;
                        if (MT <= 0)
                        {
                            MT = MeteorTime;
                            SpecialAttackMeteor();
                        }
                    }


                    if (S_A_T <= 0)
                    {
                        State = BlasphosState.ChangePosition;
                        MT = MeteorTime;
                        S_A_T = SpecialAttackTimer;

                    }
                }
                if (State == BlasphosState.ChangePosition)
                {
                    animator.SetBool(AnimatorVariableNames[1], true);
                    FireAttack.Stop();
                    agent.Resume();

                    int i = Random.Range(0, Movepoints.Length);

                    GameObject go = Movepoints[i];
                    Storedpos = go;

                    agent.SetDestination(go.transform.position);

                    State = BlasphosState.SpecialAttack;

                }
                if (State == BlasphosState.SpecialAttack)
                {
                    if (transform.position.x == Storedpos.transform.position.x && transform.position.z == Storedpos.transform.position.z)
                    {
                        animator.SetBool(AnimatorVariableNames[2], true);
                        animator.SetBool(AnimatorVariableNames[1], false);
                        transform.LookAt(GameObject.FindWithTag("Player").transform);
                        PreformSpecialAttack = true;
                    }

                }

            }
        }
    }

    public IEnumerator SwapSpeed()
    {
        while (true)
        {

                    if ((PreformSpecialAttack == false))
                    {
                        yield return new WaitForSeconds(swapSpeedCounter);
                        {
                            int f = Random.RandomRange(0, 3);

                            if (f == 0)
                            {
                                if (PauseManager.Instance != null)
                                 {
                                    if (PauseManager.Instance.IsPaused == false)
                                    {
                                        speed *= -1;
                                        animator.SetFloat(AnimatorVariableNames[0], speed);
                                    }
                                }

                                // swap animation
                            }
                        }
                    }
                    else if ((PreformSpecialAttack == true))
                    {
                        yield return null;
                        //yield break;
                        ChooseSpecialAttack();

                        Debug.Log("specialAttack");
                        yield return new WaitForSeconds(postSpecialAttackWaitTime);
                        BacktoMoving();
                    }
                
            
        }
    }

    public void BacktoMoving()
    {
        animator.SetBool(AnimatorVariableNames[1], false);
        animator.SetBool(AnimatorVariableNames[2], false);
        PreformSpecialAttack = false;
        State = BlasphosState.Moving;

        if (TeleportToCentralPoint == true || Phase2Start == true)
        {
            Teleport();
            transform.position = Centralpoint.transform.position;
        }
    }

    public void ChooseSpecialAttack()
    {
        /*
        if(UseFire == true)
        {
            UseFire = false;
        }
        else
        {
            UseFire = true;
        }
        */
        UseFire = !UseFire;
        if (Phase2Start == false)
        {
            int i = Random.Range(0, 6);

            if (i == 0 || i == 1 || i == 2)
            {
                SpecialAttackMeteor();
            }
            if (i == 3 || i == 4 || i == 5)
            {
                SpecialAttackFireRing();
            }
        }
        else
        {
            int i = Random.Range(0, 6);
         
            if (i == 0 || i == 1 || i == 2)
            {
                SpecialAttackFireRing();
            }
            if(i == 3 || i== 4 || i == 5)
            {
                SpecialAttackGigaMeteor();
            }
        }
    }
    public void SpecialAttackHeal()
    {
        Hbm.Heal(50);
    }
    public void SpecialAttackMeteor()
    {
        GameObject FiredRocket = Instantiate(Meteor, firelocation.position, Quaternion.identity);
        if(EnemyManager.Instance != null)
            EnemyManager.Instance.AddEnemy(FiredRocket);
    }
    public void SpecialAttackGigaMeteor()
    {
        GameObject FiredRocket = Instantiate(GigaMeteor, firelocation.position, Quaternion.identity);
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.AddEnemy(FiredRocket);
    }
    public void SpecialAttackFireRing()
    {
        FireRing.Play();
    }

    public void FireBullet()
    {
        Vector3 DirWithoutSpread = firelocation.position - Gun.transform.position;

        float x = UnityEngine.Random.Range(-Spread, Spread);
        float y = UnityEngine.Random.Range(-Spread, Spread);

        Vector3 FireDir = DirWithoutSpread + new Vector3(x, y, 0);

        GameObject CurrBullet = Instantiate(bullet, firelocation.position, Quaternion.identity);

        CurrBullet.transform.forward = FireDir;

        CurrBullet.GetComponent<Rigidbody>().AddForce(FireDir.normalized * shootF, ForceMode.Impulse);

        Ft = FireTime;
    }


    public void Teleport()
    {
        for (int i = 0; i < teleportObj.Length; i++)
        {
            if (teleportObj[i] != null)
            {
                teleportObj[i].Play();
            }
        }


    }

    public void BlasphosFire()
    {
        if (locationSelected == false)
        {

            f = Random.Range(0, FireSpots.Length);

            locationSelected = true;

            FireAttack.transform.position = FireSpots[f].transform.position;
            FireAttack.transform.rotation = FireSpots[f].transform.rotation;
        }



        if (FireAttack.isPlaying == false)
        {
            FireAttack.Play();
        }
        if(f == 0)
        {
            TimeElapsed += Time.deltaTime;
            float PersentageComplete = TimeElapsed / SweepTime;

            FireAttack.transform.position = Vector3.Lerp(FireSpots[0].transform.position, FireSpots[1].transform.position, PersentageComplete);
            //FireAttack.transform.Rotate(new Vector3(0, 1, 0), Space.Self);
            FireAttack.gameObject.transform.LookAt(GameObject.FindWithTag("Player").transform.position);

            if(PersentageComplete >= 1)
            {
                f = 1;

                TimeElapsed = 0;
            }

        }

        if (f == 1)
        {
            TimeElapsed += Time.deltaTime;
            float PersentageComplete = TimeElapsed / SweepTime;

            FireAttack.transform.position = Vector3.Lerp(FireSpots[1].transform.position, FireSpots[0].transform.position, PersentageComplete);
            // FireAttack.transform.Rotate(new Vector3(0, -1, 0), Space.Self);
            FireAttack.gameObject.transform.LookAt(GameObject.FindWithTag("Player").transform.position);

            if (PersentageComplete >= 1)
            {
                f = 0;

                TimeElapsed = 0;
            }

        }



        Ft = FireTime;
    }

}
