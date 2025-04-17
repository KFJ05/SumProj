using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Wallrunning : MonoBehaviour
{
    [Header("Wallrunning")]
    [SerializeField]
    LayerMask whatIsWall;
    [SerializeField]
    LayerMask WhatIsFloor;

    [SerializeField]
    float wallrunForce;
    [SerializeField]
    float maxwallrunTime;

    [SerializeField]
    float walljumpUpForce, walljumpSideForce;

    [SerializeField]
    float wallClimbSpeed;

    float wallRunTimer;

    float HI;
    float VI;

    [Header("input")]
    [SerializeField]
    KeyCode jumpKey = KeyCode.Space;
    [SerializeField]
    KeyCode UpwardsRunKey = KeyCode.LeftShift;
    [SerializeField]
    KeyCode DownwardsRunkey = KeyCode.LeftControl;
    bool upRunning;
    bool DownRunning;


    [Header("Detection")]
    [SerializeField]
    float wallCheckDistance;

    [SerializeField]
    float minJumpHeight;

    RaycastHit leftWallHit;
    RaycastHit RightWallHit;

    bool wallLeft;
    bool WallRight;

    [Header("refrences")]
    [SerializeField]
    Transform Orientation;
    [SerializeField]
    PlayerCam camera;

    Movement pm;
    Rigidbody rb;

    [Header("exiting")]
    bool exitingWall;
    [SerializeField]
    float exitWalltime;
    float exitwalltimer;


    [SerializeField]
    bool useGravity;
    [SerializeField]
    float gravityCounterforce;

    [SerializeField]
    float CamTilt, Camzoom;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        pm = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForWall();
        StateMachine();
    }

    private void FixedUpdate()
    {
        if(pm.wallRunning)
        {
            wallrunningMovment();
        }
    }


    private void CheckForWall()
    {
        WallRight = Physics.Raycast(transform.position, Orientation.right, out RightWallHit, wallCheckDistance, whatIsWall);
       wallLeft = Physics.Raycast(transform.position, -Orientation.right, out leftWallHit, wallCheckDistance, whatIsWall);
    }

    private bool aboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, WhatIsFloor);
    }

    private void StateMachine()
    {
        HI = Input.GetAxisRaw("Horizontal");
        VI = Input.GetAxisRaw("Vertical");

        upRunning = Input.GetKey(UpwardsRunKey);
        DownRunning = Input.GetKey(DownwardsRunkey);

        if((wallLeft || WallRight) && VI > 0 && aboveGround() && !exitingWall)
        {
            if(!pm.wallRunning)
            {
                startWallRun();
            }

            if(Input.GetKeyDown(jumpKey))
            {
                wallJump();
            }
            
        }
        else if(exitingWall)
        {
            if(pm.wallRunning)
            {
                StopWallRun();
            }
            if(exitwalltimer > 0)
            {
                exitwalltimer -= Time.deltaTime;
            }
            if(exitwalltimer <= 0)
            {
                exitingWall = false;
            }
        }

        else
        {
            if (pm.wallRunning)
            {
                StopWallRun();
            }
        }
    }


    private void startWallRun()
    {
        pm.wallRunning = true;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);



        camera.doFov(Camzoom);
        if (wallLeft)
        {
            camera.tiltCam(-CamTilt);
        }
        if (WallRight)
        {
            camera.tiltCam(CamTilt);
        }
    }
    private void wallrunningMovment()
    {
        rb.useGravity = useGravity;


        Vector3 wallNormal = WallRight ? RightWallHit.normal : leftWallHit.normal;

        Vector3 wallFowardv = Vector3.Cross(wallNormal, transform.up);

        if((Orientation.forward - wallFowardv).magnitude > (Orientation.forward - -wallFowardv).magnitude)
        {
            wallFowardv = -wallFowardv;
        }

        rb.AddForce(wallFowardv * wallrunForce, ForceMode.Force);

        if(upRunning)
        {
            rb.velocity = new Vector3(rb.velocity.x, wallClimbSpeed, rb.velocity.z);

        }   
        if(DownRunning)
        {
            rb.velocity = new Vector3(rb.velocity.x, -wallClimbSpeed, rb.velocity.z);
        }

        if (!(wallLeft && HI > 0) && !(WallRight && HI < 0))
        {
            rb.AddForce(-wallNormal * 100f, ForceMode.Force);
        }

        if (useGravity)
            rb.AddForce(transform.up * gravityCounterforce, ForceMode.Force);
    }
    private void StopWallRun()
    {
        pm.wallRunning = false;

        camera.doFov(75f);
        camera.tiltCam(0f);

    }
    private void wallJump()
    {

        exitingWall = true;
        exitwalltimer = exitWalltime;

        Vector3 wallNormal = WallRight ? RightWallHit.normal : leftWallHit.normal;

        Vector3 FTA = transform.up * walljumpUpForce + wallNormal * walljumpSideForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(FTA, ForceMode.Impulse);
    }
}
