using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BillBoard : MonoBehaviour
{
     Transform Cam;
    void Update()
    {
        Cam = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        transform.LookAt(Cam);
    }
}
