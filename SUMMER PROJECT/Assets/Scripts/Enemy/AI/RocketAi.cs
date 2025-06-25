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

    GameObject RocketTarget;

    public float turnSpeed;
    public float RocketSpeed;

    public ParticleSystem rocketExplode;

    Transform rocketT;



    void Start()
    {
        RocketyRb = GetComponent<Rigidbody>();
        rocketT = GetComponent<Transform>();

        RocketTarget = GameObject.FindWithTag(LockOnTag);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!RocketyRb)
        {
            return;
        }

        RocketyRb.velocity = rocketT.forward * RocketSpeed;

        var rocketTargetrot = Quaternion.LookRotation(RocketTarget.transform.position - rocketT.position);
        

        RocketyRb.MoveRotation(Quaternion.RotateTowards(rocketT.rotation, rocketTargetrot, turnSpeed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        rocketExplode.gameObject.transform.parent = null;
        rocketExplode.Play();

        Destroy(gameObject);
    }
}
