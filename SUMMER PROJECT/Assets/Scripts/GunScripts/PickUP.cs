using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PickUP : MonoBehaviour
{
    [Header("Refrences")]
    public Fire GunScript;
    public Rigidbody rb;
    public BoxCollider Coll;
    public Transform player, gunContainer, fpsCam;
    public Canvas Stats;


    public TextMeshProUGUI GunNameTmp;
    public TextMeshProUGUI GunDescTmp;
    public TextMeshProUGUI GunStatsTmp;
    public TextMeshProUGUI GunEffectsTmp;


    [Header("Pick up and throw stats")]
    public float pickUpRange;
    public float dropFForce;
    public float dropUForce;

    public bool EquipedonStart = true;

    

    public bool equipped;
    public static bool slotFull;

    [Header("KeyBinds")]
    public KeyCode PickUPWeapon = KeyCode.E;
    public KeyCode DropWeapon = KeyCode.Q;

    bool Turnoff;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        gunContainer = GameObject.FindWithTag("GUN_CONTAINER").transform;
        fpsCam = GameObject.FindWithTag("MainCamera").transform;

        if (EquipedonStart == false)
        {
            equipped = false;
            slotFull = false;
        }
        

        if (!equipped)
        {
            GunScript.enabled = false;
            rb.isKinematic = false;
            Coll.isTrigger = false;
        }
        if (equipped)
        {
            GunScript.enabled = true;
            rb.isKinematic = true;
            Coll.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        if (player == null || gunContainer == null || fpsCam == null)
        {
            player = GameObject.FindWithTag("Player").transform;
            gunContainer = GameObject.FindWithTag("GUN_CONTAINER").transform;
            fpsCam = GameObject.FindWithTag("MainCamera").transform;
        }
        else
        {

            float DTP = Vector3.Distance(player.position , transform.position);


            if (!equipped && DTP <= pickUpRange && Input.GetKey(PickUPWeapon) && !slotFull)
            {
                Pickup();
                if (Turnoff == false)
                {
                    GunStatManager.Instance.turnOffPicture();
                    Turnoff = true;
                }
            }

            else if (DTP <= pickUpRange && !equipped)
            {
               
                    GunStatManager.Instance.setName(GunNameTmp.text);
                    GunStatManager.Instance.SetDesc(GunDescTmp.text);
                    GunStatManager.Instance.SetStats(GunStatsTmp.text);
                    GunStatManager.Instance.SetEffects(GunEffectsTmp.text);

                    GunStatManager.Instance.turnOnPicture();
                Turnoff = false;
                


                //Stats.gameObject.SetActive(true);
                /*TextMeshProUGUI Name = GameObject.Find(GunNameGObjName).GetComponent<TextMeshProUGUI>();
                Name.text = GunName;
                TextMeshProUGUI Desc = GameObject.Find(GunDescGObjName).GetComponent<TextMeshProUGUI>();
                Desc.text = GunDesc;
                TextMeshProUGUI Stats = GameObject.Find(GunStatsGObjName).GetComponent<TextMeshProUGUI>();
                Stats.text = GunStats;
                TextMeshProUGUI Effects = GameObject.Find(GunEffectsGObjName).GetComponent<TextMeshProUGUI>();
                Effects.text = GunEffects;

                GameObject.FindWithTag("GunStats").SetActive(true);
                */

            }

            else
            {
                if (Turnoff == false)
                {
                    GunStatManager.Instance.turnOffPicture();
                    Turnoff = true;
                }

                // GameObject.FindWithTag("GunStats").SetActive(false);
            }

      
            if (equipped && Input.GetKey(DropWeapon))
            {
                Drop();
            }
        }

    }
    private void Pickup()
    {
        equipped = true;
        slotFull = true;

        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        rb.isKinematic = true;
        Coll.isTrigger = true;

        GunScript.enabled = true;
    }
    private void Drop()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        Coll.isTrigger = false;

        

        rb.velocity = GameObject.FindWithTag("Player").GetComponent<Rigidbody>().velocity;

        rb.AddForce(fpsCam.forward * dropFForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUForce, ForceMode.Impulse);

        float R = Random.Range(-1, 1);
        rb.AddTorque(R, R, R);
        

        GunScript.enabled = false;
    }    

    public void ResetGun()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        Coll.isTrigger = false;

        GunScript.enabled = false;

    }

}
