using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveParticleSystem : MonoBehaviour
{
    // Start is called before the first frame update


    

    public Health hp;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3((hp.CurrentHealth * 3) - 150, -215, 0);
    }
        
        
}
