using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory: MonoBehaviour
{

    Movement move;
    Slideing Slide;
    Wallrunning wallRun;
    Swinging Swing;
    PlayerCam Cam;

    bool Win;

    Transform SpawnPoint;

    [Header("Victory Screen")]
    public Canvas WinScreen;
    public Canvas HealthBar;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WIN")
        {
            Win = true;
            if (move == null)
                move = gameObject.GetComponent<Movement>();
            if (Slide == null)
                Slide = gameObject.GetComponent<Slideing>();
            if (wallRun == null)
                wallRun = gameObject.GetComponent<Wallrunning>();
            if (Swing == null)
                Swing = gameObject.GetComponent<Swinging>();
            if (Cam == null)
                Cam = gameObject.GetComponentInChildren<PlayerCam>();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            HealthBar.gameObject.SetActive(false);

            move.enabled = false;
            Slide.enabled = false;
            wallRun.enabled = false;
            Swing.enabled = false;
            Cam.enabled = false;

            WinScreen.gameObject.SetActive(true);
        }
       
    }

    private void Update()
    {
        if(Win == false)
        {
            WinScreen.gameObject.SetActive(false);
        }
    }




}
