using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;


    public Transform orientation;
    public Transform CamHolder;

    float xrot;
    float yrot;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    // Update is called once per frame
    void Update()
    {

        float mousx = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mousey = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yrot += mousx;

        xrot -= mousey;

        xrot = Mathf.Clamp(xrot, -90, 90);

        CamHolder.rotation = Quaternion.Euler(xrot, yrot, 0);
        orientation.rotation = Quaternion.Euler(0, yrot, 0);

        

    }

    public void doFov(float endVal)
    {
        GetComponent<Camera>().DOFieldOfView(endVal, 0.25f);
    }
    public void tiltCam(float zT)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zT), 0.25f);
    }


}
