using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> Enemies;

    private static EnemyManager instance;
    public static EnemyManager Instance
    {
        get
        {

            if (instance == null)
            {
                instance = FindAnyObjectByType<EnemyManager>();
            }

            if (!instance)
            {
                Debug.LogError("NO Enemy Manager Present");
            }

            return instance;
        }

    }


    public void AddEnemy(GameObject Enemy)
    {
        Enemies.Add(Enemy);
    }

    public void RemoveEnemy(GameObject Enemy)
    {
        Enemies.Remove(Enemy);
    }

    public int GetEnemyCount()
    {
        return Enemies.Count;
    }

    public GameObject LowestHealthEnemy()
    {
        float MinHealth = 9999;
        GameObject LowestHpEnemy = null;
        Health health;
        HealthBarMultiple LHpM;

        for(int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i] != null)
            {
                health = Enemies[i].GetComponent<Health>();
                if (health == null)
                {
                    LHpM = new HealthBarMultiple();
                    if (LHpM != null)
                    {
                        if(MinHealth > LHpM.totalHealth && LHpM.totalHealth > 0)
                        {
                            MinHealth = LHpM.totalHealth;
                            LowestHpEnemy = Enemies[i];
                        }
                    }
                }
                else
                {
                    if(MinHealth > health.CurrentHealth && health.CurrentHealth > 0)
                    {
                        MinHealth = health.CurrentHealth;
                        LowestHpEnemy = Enemies[i];
                    }
                }
            }
        }

        return(LowestHpEnemy);


    }

    public void RESETALL()
    {
        for(int i = 0; i < Enemies.Count;)
        {
            Destroy(Enemies[i]);
            RemoveEnemy(Enemies[i]);
        }
    }





}
