using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Spawn[] spawners;

    public pointList[] PointList;

    public DronePointList[] dronePointLists;

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

    public void TurnOnPointList(Spawn spawn)
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            if (spawners[i] == spawn)
            {
                PointList[i].TurnOn();
            }
            if(spawners[i] != spawn)
            {
                PointList[i].turnoff();
            }
        }

    }

    public void TurnOnDronePointList(Spawn spawn)
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            if (spawners[i] == spawn)
            {
                dronePointLists[i].TurnOn();
            }
            if (spawners[i] != spawn)
            {
                dronePointLists[i].turnoff();
            }
        }

    }

    public int GetSpawncompleted()
    {
        int f = 0;
        for(int i = 0; i < spawners.Length;i++)
        {
            if(spawners[i] != null)
            {
                if (spawners[i].AlreadySpawned == true)
                {
                    f++;
                }
            }
        }
        return f;
    }

    public void ResetAllSpawners()
    {
       for(int i = 0; i < spawners.Count(); i++)
        {
            if (spawners[i] != null)
            {
                spawners[i].resetSpawner();
            }
            if (PointList.Count() > 0)
            {
                if (PointList[i] != null)
                {
                    PointList[i].turnoff();
                }
            }
            if(dronePointLists.Count() > 0)
            {
                if(dronePointLists[i] != null)
                {
                    dronePointLists[i].turnoff();
                }
            }
        }
    }

}
