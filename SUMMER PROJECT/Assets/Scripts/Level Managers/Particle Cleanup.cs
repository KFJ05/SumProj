using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticleCleanup : MonoBehaviour
{
    // Start is called before the first frame update
    public float TimetoLast;



    private void Update()
    {
        if(gameObject.transform.parent == null)
        {
            Invoke(nameof(DestroyParticle), TimetoLast);
        }
    }

    public void DestroyParticle()
    {
        Destroy(gameObject);
    }

}
