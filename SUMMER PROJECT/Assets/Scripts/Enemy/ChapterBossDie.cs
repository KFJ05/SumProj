using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ChapterBossDie : MonoBehaviour
{
    // Start is called before the first frame update

    //public TimelineClip clip;
    public PlayableDirector director;

    

    public Collider[] colliders;
    public Rigidbody[] Bodies;
    public Animator animator;

    public float TimeToDestroy;

    public Health Hp;
    public HealthBarMultiple HealthBarMultiple;
    public bool usingMultipleHpBars;

    bool Triggerd = false;

    public bool WinLevelOnDeath = false;

    private void Start()
    {
       // director.
    }

    // Update is called once per frame
    void Update()
    {
        if (!usingMultipleHpBars && Triggerd == false)
        {
            if (Hp.CurrentHealth <= 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    colliders[i].isTrigger = false;
                }
                for (int i = 0; i < Bodies.Length; i++)
                {
                    Bodies[i].isKinematic = false;
                }
                animator.enabled = false;
                Triggerd = true;
            }
        }

        if (usingMultipleHpBars && Triggerd == false)
        {
            if (HealthBarMultiple.totalHealth <= 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    colliders[i].isTrigger = false;
                }
                for (int i = 0; i < Bodies.Length; i++)
                {
                    Bodies[i].isKinematic = false;
                }
                animator.enabled = false;
                Triggerd = true;
            }
        }

        if (Triggerd == true)
        {
            TimeToDestroy -= Time.deltaTime;
        }
        if(TimeToDestroy <= 0)
        {
            if (EnemyManager.Instance != null)
            {
                EnemyManager.Instance.RemoveEnemy(gameObject);
            }

            Destroy(gameObject);
        }

        if (WinLevelOnDeath == true && Triggerd == true)
        {
            Victory V = GameObject.FindWithTag("Player").GetComponent<Victory>();

            V.SetWin();

        }
    }
}
