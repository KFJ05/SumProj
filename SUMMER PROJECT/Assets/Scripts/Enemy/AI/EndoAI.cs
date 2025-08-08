using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EndoAI : MonoBehaviour
{
    // Start is called before the first frame update

    public NavMeshAgent Agent;

    [Range(1f,90f)]
    public float timeToCharge;
    float ChargeMeter;

    public Image Image;

    public Transform[] MovePoints;
    public string Tag;

    [Header("Scripts")]
    public pointList pointList;

    [Header("Endo Mode Dictionary\n" +
            "1: normal\n" +
            "2: Shield\n" +
            "3:Elite\n" +
            "4: Terminator")]
    [Range(1,4)]
    public int EndoMode;
    //script 1
    Sheild Sheild;
    //script 3
    //script 4

    public bool UsingManyHealthBar;

    


    public enum EndoStates {Moving, Shooting, Dead}
    public EndoStates State;

    [Header("Shooting Stats")]
    public float amountOfShots;
    float shots;

    public GameObject ShotObject;

    public Health Health;

    public HealthBarMultiple HBM;

    public bool dead;


    void Start()
    {
        //if 1
        if(EndoMode == 2)
        {
            Sheild = gameObject.GetComponent<Sheild>();
        }
        //if 3
        //if 4

        shots = amountOfShots;

        Health = gameObject.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Health.CurrentHealth <= 0)
        {
            dead = true;
        }

        if (dead == false)
        {
            ChargeMeter += Time.deltaTime;
            Image.fillAmount = (ChargeMeter / timeToCharge);
            if (ChargeMeter >= timeToCharge)
            {
                ChargeMeter = 0;
                //if 1
                if (Sheild != null)
                {
                    int f = Sheild.SetShield();
                    if (f == -1)
                    {
                        if (UsingManyHealthBar == false)
                        {
                            Health H = GetComponent<Health>();
                            if (H.SheildActive == true)
                            {
                                H.SheildCurrentHealth = H.SheildMaxHealth;
                            }
                            else
                            {
                                H.SheildActive = true;
                            }
                        }
                    }
                }
                // Call Ability Script;
            }
        }

        if (State == EndoStates.Moving)
        {

        }
        

    }
}
