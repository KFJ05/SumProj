using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPos : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform T;


    private void OnEnable()
    {

        transform.position = T.position;
        transform.rotation = T.rotation;
        transform.localScale = T.localScale;
    }
}
