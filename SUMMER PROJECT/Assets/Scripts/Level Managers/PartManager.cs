using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartManager : MonoBehaviour
{

    private static PartManager instance;
    public static PartManager Instance
    {
        get
        {

            if (instance == null)
            {
                instance = FindAnyObjectByType<PartManager>();
            }

            if (!instance)
            {
                Debug.LogError("NO Part Manager Present");
            }

            return instance;
        }

    }

    public List<GameObject> EnemyParts;

    public void AddPart(GameObject Part)
    {
        EnemyParts.Add(Part);
    }
    public void RemovePart(GameObject Part)
    {
        EnemyParts.Remove(Part);
    }

    public void ResetParts()
    {
        for(int i = 0; i < EnemyParts.Count;)
        {
            Destroy(EnemyParts[i]);
            EnemyParts.Remove(EnemyParts[i]);
        }
    }

}
