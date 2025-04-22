using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    Movement move;
    Slideing Slide;
    Wallrunning wallRun;
    Swinging Swing;
    PlayerCam Cam;
    Health HP;

    Transform SpawnPoint;

    [Header("Game Over Screen")]
    public Canvas GameOverScreen;
    public Canvas HealthBar;


    void Update()
    {
        if(HP == null)
        {
            HP = gameObject.GetComponent<Health>();
        }
        if(HP.CurrentHealth <= 0)
        {
            if(move == null)
                move = gameObject.GetComponent<Movement>();
            if(Slide == null)
                Slide = gameObject.GetComponent<Slideing>();
            if(wallRun == null)
                wallRun = gameObject.GetComponent<Wallrunning>();
            if(Swing == null)
                Swing = gameObject.GetComponent<Swinging>();
            if(Cam == null)
                Cam = gameObject.GetComponentInChildren<PlayerCam>();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;


            move.enabled = false;
            Slide.enabled = false;
            wallRun.enabled = false;
            Swing.enabled = false;
            Cam.enabled = false;

            GameOverScreen.gameObject.SetActive(true);
        }
        else
        {
            GameOverScreen.gameObject.SetActive(false);
        }
    }


    public void RESPAWN()
    {
        SpawnManager.Instance.ResetAllSpawners();
        EnemyManager.Instance.RESETALL();

        SpawnPoint = GameObject.FindWithTag("Player Spawn").transform;

        HP.CurrentHealth = HP.MaxHealth;


        transform.position = SpawnPoint.position;
        transform.rotation = SpawnPoint.rotation;

        move.enabled = true;
        Slide.enabled = true;
        wallRun.enabled = true;
        Swing.enabled = true;
        Cam.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }


}
