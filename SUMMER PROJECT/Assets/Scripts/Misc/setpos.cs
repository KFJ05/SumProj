using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setpos : MonoBehaviour
{
    public float x, y, z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        transform.position = new Vector3(x, y, z);
    }

}
