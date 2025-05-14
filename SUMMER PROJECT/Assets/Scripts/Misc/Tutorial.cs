using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [Header("Refrence")]
    public GameObject TutorialScreen;
    public Canvas HealthBar;

    Movement move;
    Slideing Slide;
    Wallrunning wallRun;
    Swinging Swing;
    PlayerCam Cam;
    Health HP;
    GameObject Gun;

    public bool UsingGun;

    bool calledonce = false;
    public bool removetutorial;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (move == null)
            move =  GameObject.FindWithTag("Player").GetComponent<Movement>();
        if (Slide == null)
            Slide = GameObject.FindWithTag("Player").GetComponent<Slideing>();
        if (wallRun == null)
            wallRun = GameObject.FindWithTag("Player").GetComponent<Wallrunning>();
        if (Swing == null)
            Swing = GameObject.FindWithTag("Player").GetComponent<Swinging>();
        if (Cam == null)
            Cam = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerCam>();
        if (UsingGun == true)
        {
            if (Gun == null)
            {
                Gun = GameObject.FindWithTag("Gun");
            }


        }

        if (calledonce == false)
        {
            TutorialScreen.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && calledonce == false)
        {
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
            calledonce = true;
            TutorialScreen.gameObject.SetActive(true);
        }


    }

    public void ExitTutorial()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        HealthBar.gameObject.SetActive(true);

        move.enabled = true;
        Slide.enabled = true;
        wallRun.enabled = true;
        Swing.enabled = true;
        Cam.enabled = true;
        if (UsingGun == true)
        {
            Fire fire = Gun.GetComponent<Fire>();
            fire.enabled = true;
        }
        TutorialScreen.gameObject.SetActive(false);
        if(removetutorial == true)
        {
            Destroy(gameObject);
        }

    }
}
