using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierFOV : MonoBehaviour
{

    public float radius;
    public float angle;

    public GameObject PlayerRef;

    public LayerMask TargetMask;
    public LayerMask ObstructionMask;

    public bool canSeePlayer;


    // Start is called before the first frame update
    void Start()
    {
        PlayerRef = GameObject.FindWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator FOVRoutine()
    {
        float delay = 0.2f;

        WaitForSeconds wait = new WaitForSeconds(delay);


        while (true)
        {
            yield return wait;
            FovCheck();
        }
    }

    private void FovCheck()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, radius, TargetMask);

        if (rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 DirToTarg = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, DirToTarg) < angle / 2)
            {
                float distoTarg = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, DirToTarg, distoTarg, ObstructionMask))
                {
                    canSeePlayer = true;
                }
                else canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }


    }
}
