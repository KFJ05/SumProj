using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemies;

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
                Debug.LogError("NO GAME Manager Present");
            }

            return instance;
        }

    }

    public void AddEnemy(GameObject Enemy)
    {
        enemies.Add(Enemy);
    }

    public void RemoveEnemy(GameObject Enemy)
    {
        enemies.Remove(Enemy);
    }

    public void RESETALL()
    {
        for(int i = 0; i < enemies.Count;)
        {
            Destroy(enemies[i]);
            RemoveEnemy(enemies[i]);
        }
    }





}
