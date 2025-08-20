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

    public bool useParticles = true;

    public ParticleSystem rocketExplode;

    Transform rocketT;

    public string[] notDetnatedBy;



    void Start()
    {
        RocketyRb = GetComponent<Rigidbody>();
        rocketT = GetComponent<Transform>();

        RocketTarget = GameObject.FindWithTag(LockOnTag);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PauseManager.Instance != null)
        {
            if (PauseManager.Instance.IsPaused == false)
            {
                if (!RocketyRb)
                {
                    return;
                }

                RocketyRb.velocity = rocketT.forward * RocketSpeed;

                var rocketTargetrot = Quaternion.LookRotation(RocketTarget.transform.position - rocketT.position);


                RocketyRb.MoveRotation(Quaternion.RotateTowards(rocketT.rotation, rocketTargetrot, turnSpeed));
            }
            else if(PauseManager.Instance.IsPaused)
            {
                RocketyRb.velocity = Vector3.zero;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (useParticles)
        {
            bool triggerd = true;
            for (int i = 0; i < notDetnatedBy.Length; i++)
            {
                if (collision.gameObject.tag == notDetnatedBy[i])
                {
                    triggerd = false;
                }
            }

            if (triggerd == true)
            {
                Collider c = rocketExplode.gameObject.GetComponent<Collider>();
                if (c != null)
                {
                    c.enabled = true;
                }

                rocketExplode.gameObject.transform.parent = null;
                rocketExplode.Play();

                Destroy(gameObject);
            }
        }
    }
}
