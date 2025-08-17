using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealObk : MonoBehaviour
{
    // Start is called before the first frame update

    public float HealthHealed;

   


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Health hp = collision.collider.GetComponent<Health>();
            if (hp == null)
            {
                hp = collision.collider.GetComponentInParent<Health>();
            }

            if (hp != null)
            {
                hp.Heal(HealthHealed);
            }
            gameObject.SetActive(false);

        }  
    }
}
