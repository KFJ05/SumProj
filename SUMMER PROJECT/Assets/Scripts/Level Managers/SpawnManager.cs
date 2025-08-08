using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Spawn[] spawners;

    public pointList[] PointList;

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

    public void TurnOnSpawner(Spawn spawn)
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            if (spawners[i] == spawn)
            {
                PointList[i].TurnOn();
            }
        }

    }

    public void ResetAllSpawners()
    {
       for(int i = 0; i < spawners.Count(); i++)
        {
            spawners[i].resetSpawner();
            PointList[i].turnoff();
        }
    }

}
