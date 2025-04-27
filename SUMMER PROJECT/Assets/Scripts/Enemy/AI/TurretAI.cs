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

    public float FireRate;
    public int AttacksBeforeStun;
    public float StunTimer;
    public float Spread;

    public float Range;

    public float shootF;

    public bool UseAnimator;

    public Animator Anim;

    float FR, ST;
    int ATS;

    private void Awake()
    {
        ATS = AttacksBeforeStun;
    }

    // Update is called once per frame
    void Update()
    {

      

        if (ATS <= 0)
        {
            ST = StunTimer;
            ATS = AttacksBeforeStun;
        }
        if(ST > 0)
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
                
                MainBody.LookAt(GameObject.FindWithTag("Player").transform);
                if (UseAnimator)
                {
                    Debug.Log(Anim);
                    Anim.SetBool("IsFiring", true);
                }
                if (FR <= 0)
                {
                    //fire
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

                        ATS -= 1;
                        FR = FireRate;
                    
                }
                else
                {
           

                    FR -= Time.deltaTime;
                }
            }

        }




    }
}
