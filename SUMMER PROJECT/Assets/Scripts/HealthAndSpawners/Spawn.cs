using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform[] spawnLocations;
    public GameObject[] spawnedEntities;

    public string SpawnTag;

    public bool AlreadySpawned = false;


    private void OnTriggerEnter(Collider other)
    {



        if (other.gameObject.tag == SpawnTag && AlreadySpawned == false)
        {
            for(int i = 0; i < spawnedEntities.Count(); i++)
            {

                if (spawnLocations[i] != null)
                {
                    GameObject G = Instantiate(spawnedEntities[i], spawnLocations[i].position, spawnLocations[i].rotation);
                    EnemyManager.Instance.AddEnemy(G);
                }
                else
                {
                    GameObject G = Instantiate(spawnedEntities[i], spawnedEntities[i].transform.position, spawnedEntities[i].transform.rotation);
                    EnemyManager.Instance.AddEnemy(G);
                }
                
                
            }
            AlreadySpawned = true;
        }
    }

    public void ForceSpawn()
    {
        for (int i = 0; i < spawnedEntities.Count(); i++)
        {
            if (spawnLocations[i] != null)
            {
                GameObject G = Instantiate(spawnedEntities[i], spawnLocations[i].position, spawnLocations[i].rotation);
                EnemyManager.Instance.AddEnemy(G);
            }
            else
            {
                GameObject G = Instantiate(spawnedEntities[i], spawnedEntities[i].transform.position, spawnedEntities[i].transform.rotation);
                EnemyManager.Instance.AddEnemy(G);
            }
        }
    }

 
    public void resetSpawner()
    {
        AlreadySpawned = false;
    }



}
