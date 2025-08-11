using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class OfficerAI : MonoBehaviour
{
    [Range(1, 5)]
    public int OfficerTeir;
    int Ot;

    public NavMeshAgent NavMeshAgent;

    public GameObject OfficerCoat;
    public GameObject OfficerHat;
    public GameObject OfficerS_Pads;

    public float FearRange;

    public float[] HealthTeirValues;

    public GameObject[] EnemiesSpawned;

    public Transform[] SpawnLocations;

    public Health HP;

    GameObject player;

    public Animator animator;

    public float[] SummonTimers;
    float ST;

    bool pauseTimer;

    public Image Ability;

    float StillTimer = 0;

    int R = -1;

    bool summoned = false;

    public Transform[] Pointlist;

    public GameObject Shotgun;
    public Transform ShotgunLocation;

    [Header("ShotgunStats")]
    public float TimeBetweenShots;
    float TBS;

    public GameObject Bullet;

    public Transform FireLocation;

    public float RepeatFire;

    public float[] BulletsShot;
    float BullS;

    public float[] ShootF;
    public float[] spread;


    


    public enum OfficerState { moving, Aiming, idle, Scared, Summon, dead, shooting}
    public OfficerState state;

    // Start is called before the first frame update
    void Start()
    {
        TBS = TimeBetweenShots;

        pointList PL = GameObject.FindWithTag("PointList").GetComponent<pointList>();
        Pointlist = PL.PointTransforms;

        player = GameObject.FindWithTag("Player");

        Ot = OfficerTeir;
        if(OfficerTeir == 1)
        {
            OfficerCoat.SetActive(false);
            OfficerHat.SetActive(false);
            OfficerS_Pads.SetActive(false);
            HP.MaxHealth = HealthTeirValues[OfficerTeir - 1];
            HP.CurrentHealth = HealthTeirValues[OfficerTeir - 1];
        }
        else if (OfficerTeir == 2)
        {
            OfficerCoat.SetActive(true);
            OfficerHat.SetActive(false);
            OfficerS_Pads.SetActive(false);
            HP.MaxHealth = HealthTeirValues[OfficerTeir - 1];
            HP.CurrentHealth = HealthTeirValues[OfficerTeir - 1];
        }
        else if (OfficerTeir == 3)
        {
            OfficerCoat.SetActive(true);
            OfficerHat.SetActive(true);
            OfficerS_Pads.SetActive(false);
            HP.MaxHealth = HealthTeirValues[OfficerTeir - 1];
            HP.CurrentHealth = HealthTeirValues[OfficerTeir - 1];
        }
        else if (OfficerTeir == 4)
        {
            OfficerCoat.SetActive(true);
            OfficerHat.SetActive(true);
            OfficerS_Pads.SetActive(true);
            HP.MaxHealth = HealthTeirValues[OfficerTeir - 1];
            HP.CurrentHealth = HealthTeirValues[OfficerTeir - 1];
        }
        else if (OfficerTeir == 5)
        {
            OfficerCoat.SetActive(true);
            OfficerHat.SetActive(true);
            OfficerS_Pads.SetActive(true);
            HP.MaxHealth = HealthTeirValues[OfficerTeir - 1];
            HP.CurrentHealth = HealthTeirValues[OfficerTeir - 1];
            HP.SheildActive = true;
            HP.SheildCurrentHealth = HP.SheildMaxHealth;
        }
        BullS = BulletsShot[OfficerTeir - 1];
    }
    
    // Update is called once per frame
    void Update()
    {
        if(HP.CurrentHealth <= 0)
        {
            state = OfficerState.dead;
        }

        animator.SetBool("Summoning", false);
        if (state == OfficerState.Summon)
        {
            Shotgun.gameObject.SetActive(false);
            animator.SetBool("Aiming", false);

            transform.LookAt(player.transform.position);

            Debug.Log("Summon");

            animator.SetBool("Summoning", true);

            animator.SetInteger("Salute", Random.Range(1, 4));
            if (summoned == false)
            {
                if(OfficerTeir >= 3)
                {
                    HP.SheildActive = true;
                    HP.SheildCurrentHealth = HP.SheildMaxHealth;
                }

                if (OfficerTeir < 4)
                {
                    summonEnemiesTeir1To3();
                }
                else
                {
                    summonEnemiesTeir4To5();
                }
                summoned = true;
            }

            NavMeshAgent.SetDestination(transform.position);

            StillTimer += Time.deltaTime;

            pauseTimer = true;

            if (StillTimer > 7.5)
            {
                StillTimer = 0;
                R = -1;
                state = OfficerState.moving;
            }

        }

        if(state == OfficerState.moving)
        {
            Shotgun.gameObject.SetActive(false);
            animator.SetBool("Aiming", false);
            if (R == -1)
            {
                R = Random.Range(0, Pointlist.Length);

                NavMeshAgent.SetDestination(Pointlist[R].position);
            }
            if (Vector3.Distance(Pointlist[R].position, transform.position) > 2)
            {
                animator.SetBool("Running", true);
            }
            else
            {
                animator.SetBool("Running", false);
                if (OfficerTeir < 3)
                {
                    state = OfficerState.idle;
                }
                else
                {
                    state = OfficerState.Aiming;
                }
            }
        }

        if (state == OfficerState.idle)
        {
            Shotgun.gameObject.SetActive(false);
            animator.SetBool("Aiming", false);
            transform.LookAt(player.transform.position);
        }
        if (state == OfficerState.Aiming)
        {
            Shotgun.gameObject.SetActive(true);
            Shotgun.transform.position = ShotgunLocation.position;
            transform.LookAt(player.transform.position);
            animator.SetBool("Aiming", true);

            TBS -= Time.deltaTime;
            if(TBS <= 0)
            {
                TBS = TimeBetweenShots;
                FireShotgun();
            }    

        }

        if (ST >= SummonTimers[OfficerTeir-1])
        {
            ST = 0;

            state = OfficerState.Summon;
             
            summoned = false;

        }
        
        if(Ot != OfficerTeir)
        {
            Ot = OfficerTeir;
            BullS = BulletsShot[OfficerTeir - 1];
            if (OfficerTeir == 1)
            {
                OfficerCoat.SetActive(false);
                OfficerHat.SetActive(false);
                OfficerS_Pads.SetActive(false);
                HP.MaxHealth = HealthTeirValues[OfficerTeir - 1];
                HP.CurrentHealth = HealthTeirValues[OfficerTeir - 1];
            }
            else if (OfficerTeir == 2)
            {
                OfficerCoat.SetActive(true);
                OfficerHat.SetActive(false);
                OfficerS_Pads.SetActive(false);
                HP.MaxHealth = HealthTeirValues[OfficerTeir - 1];
                HP.CurrentHealth = HealthTeirValues[OfficerTeir - 1];
            }
            else if (OfficerTeir == 3)
            {
                OfficerCoat.SetActive(true);
                OfficerHat.SetActive(true);
                OfficerS_Pads.SetActive(false);
                HP.MaxHealth = HealthTeirValues[OfficerTeir - 1];
                HP.CurrentHealth = HealthTeirValues[OfficerTeir - 1];
            }
            else if (OfficerTeir == 4)
            {
                OfficerCoat.SetActive(true);
                OfficerHat.SetActive(true);
                OfficerS_Pads.SetActive(true);
                HP.MaxHealth = HealthTeirValues[OfficerTeir - 1];
                HP.CurrentHealth = HealthTeirValues[OfficerTeir - 1];
            }
            else if (OfficerTeir == 5)
            {
                OfficerCoat.SetActive(true);
                OfficerHat.SetActive(true);
                OfficerS_Pads.SetActive(true);
                HP.MaxHealth = HealthTeirValues[OfficerTeir - 1];
                HP.CurrentHealth = HealthTeirValues[OfficerTeir - 1];
            }
        }

        if(pauseTimer == false)
        {
            ST += Time.deltaTime;

            Ability.fillAmount = (ST / SummonTimers[OfficerTeir - 1]);
        }

        if (OfficerTeir == 1 && Vector3.Distance(gameObject.transform.position, player.transform.position) <= FearRange)
        {
            animator.SetBool("Scared", true);
            pauseTimer = true;
        }
        else if ((OfficerTeir == 1 && Vector3.Distance(gameObject.transform.position, player.transform.position) > FearRange) || OfficerTeir > 1)
        {
            animator.SetBool("Scared", false);
            pauseTimer = false;
        }

    }

    public void summonEnemiesTeir1To3()
    {
        for(int i = 0; i < 2; i++)
        {
            if (EnemiesSpawned[i] != null && SpawnLocations[i] != null)
            {
                GameObject G = Instantiate(EnemiesSpawned[OfficerTeir-1], SpawnLocations[i]);
                G.transform.parent = null;
                EnemyManager.Instance.AddEnemy(G);
            }
        }
        
    }
    public void summonEnemiesTeir4To5()
    {
        for (int i = 0; i < 4; i++)
        {
            if (EnemiesSpawned[i] != null && SpawnLocations[i] != null)
            {
                GameObject G = Instantiate(EnemiesSpawned[OfficerTeir - 1], SpawnLocations[i]);
                G.transform.parent = null;
                EnemyManager.Instance.AddEnemy(G);


            }
        }
        
    }

    public void FireShotgun()
    {
        Vector3 DirWithoutSpread = FireLocation.position - Shotgun.transform.position;

        float x = UnityEngine.Random.Range(-spread[OfficerTeir-1], spread[OfficerTeir - 1]);
        float y = UnityEngine.Random.Range(-spread[OfficerTeir - 1], spread[OfficerTeir - 1]);

        BullS--;

        Vector3 FireDir = DirWithoutSpread + new Vector3(x, y, 0);


        GameObject CurrBullet = Instantiate(Bullet, FireLocation.position, Quaternion.identity);

        CurrBullet.transform.forward = FireDir;

        CurrBullet.GetComponent<Rigidbody>().AddForce(FireDir.normalized * ShootF[OfficerTeir-1], ForceMode.Impulse);

        if (BullS > 0)
        {
            Invoke(nameof(FireShotgun), RepeatFire);
        }
        if (BullS <= 0)
        {
            BullS = BulletsShot[OfficerTeir - 1];
        }
    }


}
