using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheild : MonoBehaviour
{
    // Start is called before the first frame update
    public float YOffset;

    List<GameObject> list = new List<GameObject>();
    // Update is called once per frame
    private void Update()
    {
        if(transform.parent != null)
        {
            transform.position = transform.parent.transform.position + new Vector3(0, YOffset, 0);
        }
    }
    public int SetShield()
    {
        if(EnemyManager.Instance != null)
        {
            GameObject GObj = EnemyManager.Instance.LowestHealthEnemy();
            Health health = GObj.GetComponent<Health>();

            if (health != null)
            {
                health.SheildActive = true;
            }
            return 0;
        }
        else
        {
            return -1;
        }
    }
}
