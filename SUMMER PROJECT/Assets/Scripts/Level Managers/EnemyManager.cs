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

    public void RESETALL()
    {
        for(int i = 0; i < Enemies.Count;)
        {
            Destroy(Enemies[i]);
            RemoveEnemy(Enemies[i]);
        }
    }





}
