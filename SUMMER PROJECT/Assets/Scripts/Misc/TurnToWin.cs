using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnToWin : MonoBehaviour
{
    // Start is called before the first frame update
    public string WinTag;

    public Material NormalMat;
    public Material WinMat;

    public TextMeshProUGUI text;

    public int ArenasCleardCondition;



    // Update is called once per frame
    void Update()
    {
        if ((SpawnManager.Instance != null))
        {
            text.text = Convert.ToString(SpawnManager.Instance.GetSpawncompleted()) + " / " + Convert.ToString(ArenasCleardCondition); 

            if(SpawnManager.Instance.GetSpawncompleted() >= ArenasCleardCondition)
            {
                gameObject.GetComponent<Renderer>().material = WinMat;
                gameObject.tag = WinTag;
                gameObject.GetComponent<Collider>().isTrigger = true;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material = NormalMat;
                gameObject.tag = "Untagged";
                gameObject.GetComponent<Collider>().isTrigger = false;
            }

        }
             
        
    }
}
