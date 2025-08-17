using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePointList : MonoBehaviour
{
    // Start is called before the first frame update

    public float YHeight;
    public Transform[] PointTransforms;
    public string TagtoAssign;

    private void Start()
    {
        for (int i = 0; i < PointTransforms.Length; i++)
        {
            if (PointTransforms[i] != null)
            {
                PointTransforms[i].transform.position = new Vector3(PointTransforms[i].transform.position.x, YHeight, PointTransforms[i].transform.position.z);
            }
        }
    }

    public void TurnOn()
    {
        gameObject.tag = TagtoAssign;
    }
    public void turnoff()
    {
        gameObject.tag = "Untagged";
    }
}
