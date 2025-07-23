using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    public bool SpawnBasedOnBossTransform = false;

    bool spawnTigger = false;


    public Transform[] spawnLocations;

    public Vector3[] SpawnRelitiveToBoss;

    public GameObject[] enemiesSpawned;



    

    // Update is called once per frame
    void Update()
    {

        if (spawnTigger)
        {
            GameObject G;
            for (int i = 0; i < enemiesSpawned.Length; i++)
            {
                if (SpawnBasedOnBossTransform == false)
                {
                    G = Instantiate(enemiesSpawned[i], spawnLocations[i].position, spawnLocations[i].rotation);
                }
                else
                {
                    G = Instantiate(enemiesSpawned[i], transform.position + SpawnRelitiveToBoss[i], transform.rotation);
                }
                EnemyManager.Instance.AddEnemy(G);


            }
            spawnTigger = false;
        }

    }

    public void SetSTrigger(bool strigger)
    {
        spawnTigger = strigger;
    }

}
