using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{

    MovingFortressAI MFAI;

    // Start is called before the first frame update
    void Start()
    {
        MFAI = GetComponentInParent<MovingFortressAI>();
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            MFAI.startSpawning = true;
        }
    }
}
