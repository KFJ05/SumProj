using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Spawn[] spawners;

    private static SpawnManager instance;
    public static SpawnManager Instance
    {
        get
        {

            if (instance == null)
            {
                instance = FindAnyObjectByType<SpawnManager>();
            }

            if (!instance)
            {
                Debug.LogError("NO Spawn Manager Present");
            }

            return instance;
        }

    }

    public void ResetAllSpawners()
    {
       for(int i = 0; i < spawners.Count(); i++)
        {
            spawners[i].resetSpawner();
        }
    }

}
