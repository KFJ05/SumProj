using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatExplosion : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Explosion;

    public float ExplosionTimer;
    float ET;

    public float DestroyTimer;

    void Start()
    {
        ET = ExplosionTimer;
        Destroy(gameObject, DestroyTimer);
    }

    // Update is called once per frame
    void Update()
    {
        ET -= Time.deltaTime;
        if(ET <= 0)
        {
            ET = ExplosionTimer;
            GameObject G = Instantiate(Explosion, transform.position, Quaternion.identity, null);
            ParticleSystem P = G.GetComponent<ParticleSystem>();
            if (P != null)
            {
                P.Play();
            }

        }
    }
}
