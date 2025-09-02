using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticleCleanup : MonoBehaviour
{
    // Start is called before the first frame update
    public float TimetoLast;

    public Collider Collider;
    public float ColliderTime = 0.1f;

    private void Start()
    {
        Collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if(Collider != null)
        {
            if (gameObject.transform.parent == null)
            {
                Invoke(nameof(turnoffCollider), ColliderTime);
            }
        }

        if(gameObject.transform.parent == null)
        {
            Invoke(nameof(DestroyParticle), TimetoLast);
        }
    }

    public void DestroyParticle()
    {
        Destroy(gameObject);
    }

    public void turnoffCollider()
    {
        Collider.enabled = false;
    }

}
