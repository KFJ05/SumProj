using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WallBossAI : MonoBehaviour
{
    public GameObject[] Turrets;

    public float TimeToDestroy;

    public float HealTimer;

    float TimerLeft;

    public Animator WallAnimator; 

    Health Whp;


    private void Awake()
    {
        WallAnimator.SetBool("Spawn", true);
    }

    private void Update()
    {

        if (Whp == null)
        {
            Whp = gameObject.GetComponent<Health>();
        }

        if(Whp != null && Whp.CurrentHealth <= 0)
        {
            for(int i = 0; i < Turrets.Count(); i++)
            {
                if (Turrets[i] != null)
                {
                    TurretAI T = Turrets[i].gameObject.GetComponent<TurretAI>();
                    if (T != null)
                    {
                        T.enabled = false;
                    }
                    Die D = Turrets[i].gameObject.GetComponent<Die>();

                    if (D != null)
                    {
                        D.TriggerDeath = true;
                    }
                }
            

            }
            Invoke(nameof(DestroyWall), TimeToDestroy);
        }

        //TimerLeft -= Time.deltaTime;
        if(TimerLeft <= 0)
        {
            Heal();
            TimerLeft = HealTimer;
        }



        

    }

    public void DestroyWall()
    {
        Destroy(gameObject);
    }

    public void Heal()
    {
        if (Whp == null)
        {
            Whp = gameObject.GetComponent<Health>();
        }

        Whp.Heal(25);
    }

}
