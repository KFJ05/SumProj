using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveNavMesh : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public float Maxy;

    bool starMoving;

    public Spawn bossSpawn;

    // Update is called once per frame
    void Update()
    {


        if(transform.position.y < Maxy && bossSpawn.AlreadySpawned == true)
        {
            transform.Translate(0, Time.deltaTime * speed, 0);
        }

        Respawn r = GameObject.FindWithTag("Player").GetComponent<Respawn>();

        if(r.reset == true)
        {
            transform.position = Vector3.zero;
        }

    }
}
