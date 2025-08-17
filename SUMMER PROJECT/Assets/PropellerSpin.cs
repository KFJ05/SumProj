using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerSpin : MonoBehaviour
{
    // Start is called before the first frame update

    Health hp;

    public float rotateSpeed;

    void Start()
    {
        hp = gameObject.GetComponentInParent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp != null)
        {
            if (hp.CurrentHealth > 0)
            {
                transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);      
            }
        }
        
    }
}
