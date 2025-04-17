using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject bullet;
    public Transform fireL;

    

    KeyCode Shoot = KeyCode.Mouse0;

    private void Update()
    {
        if(Input.GetKeyDown(Shoot))
        {
            GameObject B = Instantiate(bullet, fireL.position, fireL.rotation);

           
        }
    }




}
