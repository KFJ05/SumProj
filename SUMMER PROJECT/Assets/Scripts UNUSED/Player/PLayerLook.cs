using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerLook : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera cam;

    float Xrot = 0f;
    [SerializeField]
    float xSen = 30f;
    [SerializeField]
    float ySen;

    [SerializeField]
    float SprintVal = 1;


    public void proccesLook(Vector2 i)
    {
        float MX = i.x;
        float MY = i.y;


        Xrot -= (MY * Time.deltaTime) * ySen;
        Xrot = Mathf.Clamp(Xrot, -80, 80); 

        cam.transform.localRotation = Quaternion.Euler(Xrot, 0, 0);

        transform.Rotate(Vector3.up * (MX * Time.deltaTime) * xSen);


    }
}
