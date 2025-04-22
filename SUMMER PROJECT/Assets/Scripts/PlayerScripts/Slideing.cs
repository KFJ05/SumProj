using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slideing : MonoBehaviour
{
    [Header("Refrences")]

    [SerializeField]
    Transform orientation;

    [SerializeField]
    Transform playerObj;

    Rigidbody rb;

    Movement pm;

    [Header("Sliding")]
    [SerializeField]
    float MaxSlideTime;
    [SerializeField]
    float slideforce;

    float slideTimer;

    [SerializeField]
    float startYscale;

    float normalYscale;

    [Header("input")]
    [SerializeField]
    KeyCode slideKey = KeyCode.LeftControl;

    float HI;
    float VI;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<Movement>();

        normalYscale = playerObj.localScale.y;

        slideTimer = MaxSlideTime;
    }

    private void Update()
    {
        HI = Input.GetAxisRaw("Horizontal");
        VI = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(slideKey) && (HI != 0 || VI != 0) )
        {
            startSlide();
        }
        if(Input.GetKeyUp(slideKey) && pm.sliding)
        {
            stopSlide();
        }
    }

    private void FixedUpdate()
    {
        if(pm.sliding)
        {
            slidingMov();
        }
    }

    private void startSlide()
    {
        pm.sliding = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, startYscale, playerObj.localScale.z);

        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    }

    private void stopSlide()
    {
        pm.sliding = false;

        playerObj.localScale = new Vector3(playerObj.localScale.x, normalYscale, playerObj.localScale.z);

        slideTimer = MaxSlideTime;
    }

    private void slidingMov()
    {
        Vector3 InputDir = orientation.forward * VI + orientation.right * HI;
        rb.AddForce(Vector3.down * 5f, ForceMode.Force);

        if (!pm.onslope() && rb.velocity.y > -0.1f)
        {
            rb.AddForce(InputDir.normalized * slideforce, ForceMode.Force);

            slideTimer -= Time.deltaTime;

            
        }


        else
        {
            rb.AddForce(pm.GetSlopeMoveDir(InputDir) * slideforce, ForceMode.Force);
        }



        if (slideTimer <= 0)
        {
            stopSlide();
        }
    }

}
