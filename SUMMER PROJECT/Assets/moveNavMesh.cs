using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveNavMesh : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public float Maxy;

    bool starMoving;

    // Update is called once per frame
    void Update()
    {

        if(GameObject.Find("Moving Fortress") != null)
        {
            starMoving = true;
        }

        if(starMoving == true && transform.position.y < Maxy)
        {
            transform.Translate(0, Time.deltaTime * speed, 0);
        }
    }
}
