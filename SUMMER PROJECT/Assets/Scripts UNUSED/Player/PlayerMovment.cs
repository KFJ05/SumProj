using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    CharacterController controller;
    Vector3 PlayerV;

    [SerializeField]
    float speed;

    [SerializeField]
    float G = -9.8f;

    [SerializeField]
    float jumpHeight;

    bool onGround;

    float SprintVal = 1;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = controller.isGrounded;
    }

    public void SetSprint(float x)
    {
        SprintVal = x;

    }

    public void ProcessMove(Vector2 X)
    {
        Vector3 MoveDir = Vector3.zero;


        MoveDir.x = X.x;
        MoveDir.z = X.y;

        controller.Move(transform.TransformDirection(MoveDir) * speed * SprintVal *  Time.deltaTime);
        PlayerV.y += G * Time.deltaTime;
        if (onGround && PlayerV.y < 0 )
        {
            PlayerV.y = -2;
        }

        Debug.Log(PlayerV.y);
        controller.Move(PlayerV * Time.deltaTime);

    }

    public void Jump()
    {
        if(onGround)
        {
            PlayerV.y = Mathf.Sqrt(jumpHeight * -3 * G);
        }
    }

    public void sprint()
    {
        SetSprint(2.5f);


    }

    public void UnSprint()
    {
        SetSprint(1);
    }
}
