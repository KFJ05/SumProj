using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class KeyPad : Interact
{
    [SerializeField]
    GameObject[] ConnectedDoors;

    bool OnOOff = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void InteraWithObject()
    {
        

        OnOOff = !OnOOff;
        for (int i = 0; i < ConnectedDoors.Length; i++)
        {
            ConnectedDoors[i].GetComponent<Animator>().SetBool("isOpen", OnOOff);
        }

    }


}
