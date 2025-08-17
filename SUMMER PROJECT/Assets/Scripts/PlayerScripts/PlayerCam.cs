using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    float SENS;

    public Transform orientation;
    public Transform CamHolder;

    float xrot;
    float yrot;

    // Start is called before the first frame update
    void Start()
    {

        if(GameManager.Instance != null)
        {
            sensX = GameManager.Instance.GetmouseSensitivity();
            sensY = GameManager.Instance.GetmouseSensitivity();
            SENS = GameManager.Instance.GetmouseSensitivity();
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.Instance != null)
        {
            if (PauseManager.Instance.IsPaused == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                float mousx = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
                float mousey = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

                yrot += mousx;

                xrot -= mousey;

                xrot = Mathf.Clamp(xrot, -90, 90);

                CamHolder.rotation = Quaternion.Euler(xrot, yrot, 0);
                orientation.rotation = Quaternion.Euler(0, yrot, 0);

            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                if (GameManager.Instance != null)
                {
                    if (SENS != GameManager.Instance.GetmouseSensitivity())
                    {
                        sensX = GameManager.Instance.GetmouseSensitivity();
                        sensY = GameManager.Instance.GetmouseSensitivity();
                    }
                }

            }
        }

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
