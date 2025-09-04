using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public GameObject[] Enemies;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Enemies.Length; i++)
        {
            if(Enemies[i] != null)
            {
                if(EnemyManager.Instance != null)
                {
                    EnemyManager.Instance.AddEnemy(Enemies[i]);
                }
            }
        }
    }


}
