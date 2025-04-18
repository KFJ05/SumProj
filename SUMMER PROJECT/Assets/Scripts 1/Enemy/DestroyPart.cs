using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPart : MonoBehaviour
{
    // Start is called before the first frame update

    public float TimeToDestroy;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.parent == null)
        {
            TimeToDestroy -= Time.deltaTime;

            if (TimeToDestroy <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
