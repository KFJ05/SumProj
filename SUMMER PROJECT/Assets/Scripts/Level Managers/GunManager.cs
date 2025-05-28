using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [Header("Refrences")]
    public GameObject[] Gun;
    public Transform[] GunSpawnLocation;

    private static GunManager instance;
    public static GunManager Instance
    {
        get
        {

            if (instance == null)
            {
                instance = FindAnyObjectByType<GunManager>();
            }

            if (!instance)
            {
                Debug.LogError("NO Gun Manager Present");
            }

            return instance;
        }

    }

    public void SpawnNewGun()
    {
        for (int i = 0; i < Gun.Count(); i++)
        {
            if (Gun[i] != null && GunSpawnLocation[i] != null)
            {


                Fire CurrGunFire = Gun[i].GetComponent<Fire>();
                CurrGunFire.ResetGun();

                PickUP CurrGunPU = Gun[i].GetComponent<PickUP>();
                CurrGunPU.ResetGun();
                
         
                Gun[i].transform.position = GunSpawnLocation[i].position;
                Gun[i].transform.rotation = GunSpawnLocation[i].rotation;
                
            }
        }
    }




}
