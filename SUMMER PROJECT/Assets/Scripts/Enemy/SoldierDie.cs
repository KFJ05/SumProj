using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierDie : MonoBehaviour
{
    // Start is called before the first frame update

    public Collider[] colliders;
    public Rigidbody[] Bodies;
    public Animator animator;

    public float TimeToDestroy;

    public Health Hp;
    public HealthBarMultiple HealthBarMultiple;
    public bool usingMultipleHpBars;

    public Canvas HealthBar;

    bool Triggerd = false;

    public bool WinLevelOnDeath = false;

    // Update is called once per frame

    private void Start()
    {
     
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].isTrigger = true;
            }
            for (int i = 0; i < Bodies.Length; i++)
            {
                Bodies[i].isKinematic = true;
            }
            animator.enabled = true;
        
    }

    void Update()
    {
        if (PauseManager.Instance != null)
        {
            if (PauseManager.Instance.IsPaused == false)
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
                    if (HealthBar != null)
                    {
                        HealthBar.gameObject.SetActive(false);
                    }
                    TimeToDestroy -= Time.deltaTime;
                }
                if (TimeToDestroy <= 0)
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
    }
}
