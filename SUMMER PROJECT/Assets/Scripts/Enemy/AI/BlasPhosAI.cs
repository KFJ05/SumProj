using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasPhosAI : MonoBehaviour
{
    // Start is called before the first frame update

    public float SpecialAttackTimer;
    public enum BlasphosState { Moving, ChangePosition, Attacking, SpecialAttack, Dead }

    public BlasphosState State;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
