using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Victory: MonoBehaviour
{

    Movement move;
    Slideing Slide;
    Wallrunning wallRun;
    Swinging Swing;
    PlayerCam Cam;

    GameObject Gun;

    bool Win;

    public int LowestEnemiesAliveNeededToWin;

    [Header("Victory Screen")]
    public Canvas WinScreen;
    public Canvas HealthBar;
    public TextMeshProUGUI Requirements;


    private void Awake()
    {
        Requirements.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (EnemyManager.Instance != null)
        {
            if (other.gameObject.tag == "WIN" && EnemyManager.Instance.GetEnemyCount() <= LowestEnemiesAliveNeededToWin)
            {
                win();
            }
            else if(other.gameObject.tag == "WIN" && EnemyManager.Instance.GetEnemyCount() > LowestEnemiesAliveNeededToWin)
            {
                Requirements.gameObject.SetActive(true);
            }
        }
        else
        {
            if (other.gameObject.tag == "WIN")
            {
                win();
            }
        }
       
    }

    private void Update()
    {
        if(Win == false)
        {
            WinScreen.gameObject.SetActive(false);
        }
        if (EnemyManager.Instance != null)
        {
            if(Requirements.gameObject.activeInHierarchy == true && (EnemyManager.Instance.GetEnemyCount() - LowestEnemiesAliveNeededToWin == 1))
            {
                Requirements.text = "Must Deafeat " + (EnemyManager.Instance.GetEnemyCount() - LowestEnemiesAliveNeededToWin) + " more enemy to Unlock Exit";
            }
            else if (Requirements.gameObject.activeInHierarchy == true && (EnemyManager.Instance.GetEnemyCount() - LowestEnemiesAliveNeededToWin > 1))
            {
                Requirements.text = "Must Deafeat " + (EnemyManager.Instance.GetEnemyCount() - LowestEnemiesAliveNeededToWin) + " more enemies to Unlock Exit";

            }
            else
            {
                Requirements.gameObject.SetActive(false);
            }
        }
    }

    public void win()
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

        Gun = GameObject.FindWithTag("Gun");
        if (Gun != null)
        {
            Fire CF = Gun.GetComponent<Fire>();
            CF.enabled = false;
        }

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
