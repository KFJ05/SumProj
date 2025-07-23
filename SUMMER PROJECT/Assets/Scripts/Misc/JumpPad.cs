using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RaisingPlatform : MonoBehaviour
{
    public float moveheight;
    public bool startMoving = false;
    public string[] MoveTags;

    public float Maxheight;

    public Vector3 originalPos;


    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < MoveTags.Length; i++)
        {
            if(collision.gameObject.tag == MoveTags[i])
            {
                startMoving = true;
                break;
            }
        }
    }

    private void Update()
    {
        if (startMoving)
        {
            transform.Translate(Vector3.up * moveheight * Time.deltaTime);
        }

        if(transform.position.y >= Maxheight)
        {
            startMoving = false;

            transform.position = originalPos;
        }
    }
}
