using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DropshipAI : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Turret;

    public Transform[] FrontThrusters;
    public Transform[] RearThrusters;

    public Transform RightDoor;
    public Transform LeftDoor;

    public NavMeshAgent Agent;

    GameObject Player;

    public float detectionAngle = 0f;

    public float TurretTurnSpeed = 1f;
    public bool Fireturret;



    private Quaternion baseRotation;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");

        baseRotation = Turret.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Fireturret == true)
        {
            Vector3 toPlayer = (Player.transform.position - transform.position).normalized;

            // Work in the horizontal plane only
            toPlayer.y = 0;
            Vector3 forward = transform.forward;
            forward.y = 0;

            // Signed angle between forward and player
            float angleToPlayer = Vector3.SignedAngle(forward, toPlayer, Vector3.up);

            // Clamp current turret angle
            float currentAngle = Turret.localEulerAngles.z;
            if (currentAngle > 180) currentAngle -= 360; // convert to -180..180

            Debug.Log(currentAngle);

            // Define allowed range (±90 for example)
            float minAngle = -90f;
            float maxAngle = 90f;

            if (angleToPlayer > 1f && currentAngle < maxAngle)
            {
                Turret.Rotate(0, 0, TurretTurnSpeed * Time.deltaTime);
            }
            else if (angleToPlayer < -1f && currentAngle > minAngle)
            {
                Turret.Rotate(0, 0, -TurretTurnSpeed * Time.deltaTime);
            }
        }
        else
        {
            Turret.localRotation = Quaternion.RotateTowards(
           Turret.localRotation,
           baseRotation,
           TurretTurnSpeed * Time.deltaTime);
        }
    }
}
