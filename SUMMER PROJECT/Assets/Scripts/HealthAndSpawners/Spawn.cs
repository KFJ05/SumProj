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

    Spawn spawn;

    [Range(1,5)]
    public int OfficerRank;


    private void Start()
    {
        spawn = gameObject.GetComponent<Spawn>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == SpawnTag && AlreadySpawned == false)
        {
            AlreadySpawned = true;
            for (int i = 0; i < spawnedEntities.Count(); i++)
            {



                if (spawnLocations[i] != null)
                {
                    GameObject G = Instantiate(spawnedEntities[i], spawnLocations[i].position, spawnLocations[i].rotation);
                    if(G.GetComponent<OfficerAI>() != null)
                    {
                        OfficerAI officerAI = G.GetComponent<OfficerAI>();
                        officerAI.OfficerTeir = OfficerRank;
                    }
                    EnemyManager.Instance.AddEnemy(G);
                }
                else
                {
                    GameObject G = Instantiate(spawnedEntities[i], spawnedEntities[i].transform.position, spawnedEntities[i].transform.rotation);
                    EnemyManager.Instance.AddEnemy(G);
                }
            }
            if(SpawnManager.Instance != null)
            {
                SpawnManager.Instance.TurnOnPointList(spawn);
                SpawnManager.Instance.TurnOnDronePointList(spawn);
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
