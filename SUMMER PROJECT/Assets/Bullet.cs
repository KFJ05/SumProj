using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    public float Damage;

    public float bulletLifeTime;

    Rigidbody rb;

    // Update is called once per frame

    private void Start()
    {
        Destroy(gameObject, bulletLifeTime);

        rb = gameObject.GetComponent<Rigidbody>();

        
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }


}
