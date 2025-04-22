using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [Header("movement")]
    [SerializeField]
    float walkspeed;

    float lastPreservedSpeed;

    bool preserve;

    float moveSpeed;

    [SerializeField]
    float slideSpeed;

    [SerializeField, Range(1f,15f)]
    float SlideSlopeMultiplier;

    [SerializeField]
    float wallrunSpeed;

    [SerializeField]
    float SwingSpeed;

    float DMoveSpeed;
    float LastDMoveSpeed;

    [SerializeField]
    float groundDrag;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    float JumpCooldown;

    [SerializeField]
    float airMultiplier;

    bool ReadytoJump = true;

    [Header("ground check")]

    [SerializeField]
    float playerheight;

    [SerializeField]
    LayerMask whatisGround;

    [SerializeField]
    Transform orientation;

    [Header("Crouching")]
    [SerializeField]
    float crouchSpeed;

    [SerializeField]
    float crouchYscale;

    [SerializeField]
    float NormalScale;


    [Header("SlopeSettings")]

    [SerializeField]
    float MaxSlopeAngle;

    RaycastHit slopeHit;

    bool exitingSlope;

    public float MAX_SPEED;


    [Header("keybinds")]
    [SerializeField]
    KeyCode jumpKey = KeyCode.Space;
    [SerializeField]
    KeyCode CrouchAndSlideKey = KeyCode.C;

    

    bool Grounded;

    float horizontalInput;
    float Vinput;

    Vector3 MoveDir;

    Rigidbody rb;


    public MoveState state;
    public enum MoveState
    {
        walking,
        crouching,
        wallRunning,
        Sliding,
        Swinging,
        air


    }

    public bool sliding;
    public bool wallRunning;
    public bool swinging;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        DMoveSpeed = walkspeed;

        LastDMoveSpeed = DMoveSpeed;

        rb.freezeRotation = true;

        NormalScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        Grounded = Physics.Raycast(transform.position, Vector3.down, playerheight * 0.5f + 0.2f, whatisGround);

        getInput();

        SpeedControl();

        stateHandler();

        if(Grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }


        if (moveSpeed >= MAX_SPEED)
        {
            moveSpeed = MAX_SPEED;
        }
        else if (moveSpeed < walkspeed && preserve == false)
        {
            moveSpeed = walkspeed;
        }
        else if(preserve == true)
        {
            moveSpeed = lastPreservedSpeed;
        }


    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    private void stateHandler()
    {
        if(wallRunning)
        {
            state = MoveState.wallRunning;
            DMoveSpeed = wallrunSpeed;
        }


        if(sliding)
        {
            state = MoveState.Sliding;

            if(onslope() && rb.velocity.y < 0.1f)
            {
                DMoveSpeed = slideSpeed;
            }
            else
            {
                DMoveSpeed = walkspeed;
            }
        }


        else if(swinging)
        {
            state = MoveState.Swinging;
            moveSpeed = SwingSpeed;
        }

        else if (Grounded && Input.GetKeyDown(CrouchAndSlideKey))
        {
            state = MoveState.crouching;
            DMoveSpeed = crouchSpeed;
        }

        else if (Grounded)
        {
            state = MoveState.walking;
            DMoveSpeed = walkspeed;
        }

        else if (!Grounded)
        {
            state = MoveState.air;
        }



        if (!wallRunning)
        {
            if (Mathf.Abs(DMoveSpeed - LastDMoveSpeed) > 4f)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());

                Debug.Log("Lerp");
            }
            else
            {
                moveSpeed = DMoveSpeed;
            }
        }
        else
        {
            lastPreservedSpeed = moveSpeed;

            StopAllCoroutines();

            preserve = true;

            moveSpeed = lastPreservedSpeed;
        }

            LastDMoveSpeed = DMoveSpeed;

    }

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        preserve = false;

        float time = 0;
        float D = Mathf.Abs(DMoveSpeed - moveSpeed);
        float StartV = moveSpeed;

    
            while (time < D)
            {
                if (sliding && rb.velocity.y < 0.1f)
                {
                    moveSpeed = Mathf.Lerp(StartV, DMoveSpeed * SlideSlopeMultiplier, time / D);
                    time += Time.deltaTime;
                    yield return null;
                }

                else
                {

                    moveSpeed = Mathf.Lerp(StartV, DMoveSpeed, time / D);
                    time += Time.deltaTime;
                    yield return null;


                }
            }
        


            moveSpeed = DMoveSpeed;

    }




    private void getInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        Vinput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && ReadytoJump && Grounded)
        {
            ReadytoJump = false;

            jump();

            Invoke(nameof(resetjumo), JumpCooldown);
        }

        if(Input.GetKeyDown(CrouchAndSlideKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYscale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(CrouchAndSlideKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, NormalScale, transform.localScale.z);

           
        }
    }

    private void movePlayer()
    {
        if (swinging) return;

        MoveDir = orientation.forward * Vinput + orientation.right * horizontalInput;


        if (Grounded)
        {
            rb.AddForce(MoveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        if(onslope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDir(MoveDir) * moveSpeed * 20f, ForceMode.Force);

            if(rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        else if(!Grounded)
        {
            rb.AddForce(MoveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        if(!wallRunning) rb.useGravity = !onslope();
    }


    private void SpeedControl()
    {
        if (onslope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }
        else
        {

            Vector3 flatvel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            if (flatvel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatvel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);


            }
        }
    }

    private void jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        exitingSlope = true;

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void resetjumo()
    {
        ReadytoJump = true;

        exitingSlope = false;
    }

    public bool onslope()
    {
        
        

        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerheight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < MaxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDir( Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }
}
