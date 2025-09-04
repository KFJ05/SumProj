using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleBosses : MonoBehaviour
{


    public GameObject[] Bosses;


    // Start is called before the first frame update
    void Start()
    {
        if(EnemyManager.Instance != null)
        {
            EnemyManager.Instance.RemoveEnemy(gameObject);

            for(int i = 0; i < Bosses.Length; i++)
            {
                EnemyManager.Instance.AddEnemy(Bosses[i]);
            }
        }
        
    }

    // Update is called once per frame

}
