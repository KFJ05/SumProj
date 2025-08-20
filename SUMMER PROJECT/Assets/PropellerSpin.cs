using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerSpin : MonoBehaviour
{
    // Start is called before the first frame update

    Health hp;

    public float rotateSpeed;

    public bool RotateX, RotateZ;
    public bool RotateY = true;

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
                if(RotateX)
                {
                    transform.Rotate(rotateSpeed * Time.deltaTime,0, 0);
                }
                if(RotateY)
                    transform.Rotate(0, rotateSpeed * Time.deltaTime, 0); 
                if(RotateZ)
                    transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
            }
        }
        
    }
}
