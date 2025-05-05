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
    public bool ResetGunOnDeath;
    GameObject Gun;

    public bool UsingGun;
    
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
            if (UsingGun == true)
            {
                if (Gun == null)
                {
                    Gun = GameObject.FindWithTag("Gun");
                }
                if (ResetGunOnDeath)
                {
                    GunManager.Instance.SpawnNewGun();
                }
                else
                {
                    Fire CF = Gun.GetComponent<Fire>();
                    CF.ResetGun();
                }
            }



            
            



            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            HealthBar.gameObject.SetActive(false);

            move.enabled = false;
            Slide.enabled = false;
            wallRun.enabled = false;
            Swing.enabled = false;
            Cam.enabled = false;
            if (UsingGun == true)
            {
                Fire fire = Gun.GetComponent<Fire>();
                fire.enabled = false;
            }
            

            GameOverScreen.gameObject.SetActive(true);
        }
        else
        {
            GameOverScreen.gameObject.SetActive(false);
        }
    }


    public void RESPAWN()
    {
        HealthBar.gameObject.SetActive(true);

        if (SpawnManager.Instance != null)
        {
            SpawnManager.Instance.ResetAllSpawners();
        }
        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.RESETALL();
        }
        if(PartManager.Instance != null)
        {
            PartManager.Instance.ResetParts();
        }

        SpawnPoint = GameObject.FindWithTag("Player Spawn").transform;

        HP.CurrentHealth = HP.MaxHealth;


        transform.position = SpawnPoint.position;
        transform.rotation = SpawnPoint.rotation;

        move.enabled = true;
        Slide.enabled = true;
        wallRun.enabled = true;
        Swing.enabled = true;
        Cam.enabled = true;
        if (UsingGun == true)
        {
            Fire fire = Gun.GetComponent<Fire>();
            fire.enabled = false;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }


}
