using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointList : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform[] PointTransforms;
    public string TagtoAssign;

    public void TurnOn()
    {
        gameObject.tag = TagtoAssign;
    }
    public void turnoff()
    {
        gameObject.tag = "Untagged";
    }
}
