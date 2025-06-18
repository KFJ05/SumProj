using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RocketAi : MonoBehaviour
{
    // Start is called before the first frame update
    public string LockOnTag;
    Rigidbody RocketyRb;

    public float turnSpeed;
    public float RocketSpeed;

    Transform rocketT;



    void Start()
    {
       // RocketyRb = gameobject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
