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

    public GameObject HealthObj;

    Transform SpawnPoint;

    [Header("Game Over Screen")]
    public Canvas GameOverScreen;
    public Canvas HealthBar;
    public bool ResetGunOnDeath;
    GameObject Gun;

    public bool UsingGun;

    public bool reset = false;

    public bool ResetMusic = false;
    public SwapMusic SwapMusic = null;
    public AudioClip LevelMusic;

    private void Start()
    {
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
        if (Gun == null)
        {
            Gun = GameObject.FindWithTag("Gun");
        }
    }
    void Update()
    {
        if(HP == null)
        {
            HP = gameObject.GetComponent<Health>();
        }
        if(HP.CurrentHealth <= 0)
        {
            reset = true;

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
        PauseManager.Instance.IsPaused = false;

        if (HealthObj != null)
        {
            HealthObj.gameObject.SetActive(true);
        }

        if(ResetMusic == true)
        {
            GameManager.Instance.StopMusic();
            GameManager.Instance.PlayMusic(LevelMusic, true);
            SwapMusic.triggered = false;
        }

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
            if(fire != null) 
                fire.enabled = false;

            GunManager.Instance.SpawnNewGun();
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        reset = false;
    }


}
