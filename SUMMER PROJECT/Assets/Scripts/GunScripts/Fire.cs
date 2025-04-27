using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    [Header("Refrences")]
    public GameObject bullet;
    public Camera PlayerCam;
    public Transform FireLocation;
    public GameObject MuzzleFlash;
    TextMeshProUGUI Text;
    Canvas canvas;


    [Header("BulletStats")]
    public float shootF;
    public float UpwardF;

    //gun stats

    [Header("GunStats")]
    public float TimeBetweenShooting;
    public float Spread;
    public float ReloadTime;
    public float timeBetweenShots;

    public int MagSize;
    public int BuletsFiredOnClick;

    public bool FullAuto;

    int BulletsLeft, bulletsShot;

    bool shooting, ReadyToShoot, reloading;

    [Header("KeyInputs")]
    public KeyCode FireKey = KeyCode.Mouse0;
    public KeyCode ReloadKey = KeyCode.R;

    [Header("Bug Fixing!!!")]
    public bool AllowInvoke = true;

    private void Awake()
    {
        BulletsLeft = MagSize;

        canvas = GameObject.FindWithTag("GunUI").GetComponent<Canvas>();

        Text = GameObject.FindWithTag("AmmoText").GetComponent<TextMeshProUGUI>();

        ReadyToShoot = true;

        PlayerCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    private void OnEnable()
    {
        canvas.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        canvas.gameObject.SetActive(false);
    }


    private void Update()
    {
        if(PlayerCam == null)
        {
            PlayerCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }
        if(canvas == null || Text == null)
        {
            canvas = GameObject.FindWithTag("GunUI").GetComponent<Canvas>();

            Text = GameObject.FindWithTag("AmmoText").GetComponent<TextMeshProUGUI>();


        }

        Text.text = (BulletsLeft/BuletsFiredOnClick) + " / " + (MagSize/BuletsFiredOnClick);

        MyInput();
    }

    private void MyInput()
    {
        if(FullAuto)
        {
            shooting = Input.GetKey(FireKey);
        }
        else
        {
            shooting = Input.GetKeyDown(FireKey);
        }

        if(Input.GetKeyDown(ReloadKey) && BulletsLeft < MagSize && !reloading)
        {
            Reload();
        }
        if (ReadyToShoot && shooting && !reloading && BulletsLeft <= 0)
        {
            Reload();
        }

            if (ReadyToShoot && shooting && !reloading && BulletsLeft > 0)
        {
            bulletsShot = 0;

            Shoot();
        }

        
    }

    private void Shoot()
    {
        ReadyToShoot = false;

        Ray ray = PlayerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetpoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetpoint = hit.point;
        }
        else
        {
            targetpoint = ray.GetPoint(75);
        }

        Vector3 DirWithoutSpread = targetpoint - FireLocation.position;

        float x = Random.Range(-Spread, Spread);
        float y = Random.Range(-Spread, Spread);


        Vector3 FireDir = DirWithoutSpread + new Vector3(x, y, 0);


        GameObject CurrBullet = Instantiate(bullet, FireLocation.position, Quaternion.identity);

        CurrBullet.transform.forward = FireDir;

        CurrBullet.GetComponent<Rigidbody>().AddForce(FireDir.normalized * shootF,ForceMode.Impulse);
        CurrBullet.GetComponent<Rigidbody>().AddForce(PlayerCam.transform.up * UpwardF, ForceMode.Impulse);

        if (MuzzleFlash != null)
        {
            Instantiate(MuzzleFlash, FireLocation.position, Quaternion.identity);
        }

        if(AllowInvoke)
        {
            Invoke("ResetShot", TimeBetweenShooting);
            AllowInvoke = false;
        }

        BulletsLeft--;
        bulletsShot++;

        if(bulletsShot < BuletsFiredOnClick && BulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }


    }

    private void ResetShot()
    {
        ReadyToShoot = true;
        AllowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", ReloadTime);
    }
    private void ReloadFinished()
    {
        BulletsLeft = MagSize;
        reloading = false;
    }

    public void ResetGun()
    {
        BulletsLeft = MagSize;
    }


}
