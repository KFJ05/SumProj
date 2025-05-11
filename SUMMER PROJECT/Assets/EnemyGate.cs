using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGate : MonoBehaviour
{
    // Start is called before the first frame update

    Animator anim;


    public Vector3 Open;
    public Vector3 Closed;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyManager.Instance.GetEnemyCount() > 0)
        {
            // play animation to seal gate. right now ist just a change of position
            transform.position = Closed;

        }
        else if(EnemyManager.Instance.GetEnemyCount() <= 0)
        {
            transform.position = Open;
        }

        
    }
}
