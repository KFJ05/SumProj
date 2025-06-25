using Lolopupka;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TurretAI : MonoBehaviour
{


    [Header("Refrences")]
    public GameObject bullet;


    public Transform MainBody;
    public Transform[] firelocation;

    [Header("AI Stats")]

    public bool FireBullets = true;

    public float TimetoWaitOnSpawn;

    public float FireRate;
    public int AttacksBeforeStun;
    public float StunTimer;
    public float Spread;

    public bool useBaseLookAt = true;

    public float Range;

    public float shootF;

    public bool UseAnimator;

    public Animator Anim;

    public float turnSpeed;

    float FR, ST;
    int ATS;

    GameObject Player;

    public bool FireFunctionCalledElsewhere;

    public Rigidbody TurrRB;



    [Header("Rocket Settings")]
    public GameObject Rocket;
    public Transform fireRocketlocation;
    public bool FireRockets = false;
    public float RocketFireRate = 10f;

    float RocketTimer;

    private void Awake()
    {
        ATS = AttacksBeforeStun;

        Player = GameObject.FindWithTag("Player");

        RocketTimer = RocketFireRate;

    }

    // Update is called once per frame
    void Update()
    {
        if (TimetoWaitOnSpawn <= 0)
        {
            if (FireFunctionCalledElsewhere == false)
            {


                if (ATS <= 0)
                {
                    ST = StunTimer;
                    ATS = AttacksBeforeStun;
                }
                if (ST > 0)
                {
                    if (UseAnimator)
                    {
                        Anim.SetBool("IsFiring", false);
                    }

                    ST -= Time.deltaTime;
                }
                else
                {


                    float D = Vector3.Distance(gameObject.transform.position, GameObject.FindWithTag("Player").transform.position);
                    if (D <= Range)
                    {

                        if (useBaseLookAt == true)
                        {
                            MainBody.LookAt(GameObject.FindWithTag("Player").transform);
                        }
                        else
                        {

                            var rocketTargetrot = Quaternion.LookRotation(Player.transform.position - MainBody.position);
                            TurrRB.MoveRotation(Quaternion.RotateTowards(MainBody.rotation, rocketTargetrot, turnSpeed));
                        }

                        if (UseAnimator)
                        {
                            // Debug.Log(Anim);
                            Anim.SetBool("IsFiring", true);
                        }
                        if (FR <= 0 && FireBullets == true)
                        {
                            //fire
                            FireTurrBullet();

                            ATS -= 1;
                            FR = FireRate;

                        }
                        else
                        {
                            FR -= Time.deltaTime;
                        }


                        if (FireRockets == true)
                        {
                            RocketTimer -= Time.deltaTime;

                            if (RocketTimer <= 0)
                            {
                                FireRocket();
                                RocketTimer = RocketFireRate;
                            }
                        }
                    }

                }
            }
            else
            {
                MainBody.LookAt(GameObject.FindWithTag("Player").transform);
            }
        }
        else
        {
            TimetoWaitOnSpawn -= Time.deltaTime;
        }



    }

    public void FireTurrBullet()
    {
        for (int i = 0; i < firelocation.Count(); i++)
        {
            Vector3 DirWithoutSpread = firelocation[i].position - MainBody.position;



            float x = Random.Range(-Spread, Spread);
            float y = Random.Range(-Spread, Spread);


            Vector3 FireDir = DirWithoutSpread + new Vector3(x, y, 0);


            GameObject CurrBullet = Instantiate(bullet, firelocation[i].position, Quaternion.identity);

            CurrBullet.transform.forward = FireDir;

            CurrBullet.GetComponent<Rigidbody>().AddForce(FireDir.normalized * shootF, ForceMode.Impulse);
        }

    }

    public void FireRocket()
    {
        GameObject FiredRocket = Instantiate(Rocket, fireRocketlocation.position, Quaternion.identity);
    }
}


