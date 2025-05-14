using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookSpawn : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Spawn;

    private void Start()
    {
        transform.LookAt(Spawn.transform);
    }

    private void Awake()
    {
        transform.LookAt(Spawn.transform);
    }
}
