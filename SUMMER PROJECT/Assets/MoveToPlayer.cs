using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    GameObject player;


    private void Update()
    {
        player = GameObject.FindWithTag("Player");

        transform.LookAt(player.transform);

        transform.Translate(Vector3.forward * Time.deltaTime * 12, Space.Self);
    }

}
