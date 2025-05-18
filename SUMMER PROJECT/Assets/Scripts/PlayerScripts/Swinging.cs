using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Swinging : MonoBehaviour
{
    [Header("input")]
    [SerializeField]
    KeyCode swingKey = KeyCode.Mouse0;

    [Header("Refrences")]
    public LineRenderer lr;
    [SerializeField]
    Transform gunTip, cam, player;
    [SerializeField]
    LayerMask whatIsGrapple;
    [SerializeField]
    Movement playerMovement;


    [Header("swinging")]
    float MaxSwingDistance = 25f;
    Vector3 swingPort;
    SpringJoint joint;

    Vector3 currentGrapplePos;


    [Header("AirControll")]
    public Transform orientation;
    public Rigidbody rb;
    public float horizontalThrust;
    public float fowardThrust;
    public float extendCableSpeed;


    [Header("Prediction")]
    public RaycastHit predicionHit;
    public float predictionSphereCastRad;
    public Transform PredictionPoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(swingKey))
        {
            StartSwing();
        }
        if(Input.GetKeyUp(swingKey))
        {
            StopSwing();
        }

        CheckforSwingPoints();

        if(joint != null)
        {
            AirControl();
        }
        
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void DrawRope()
    {
        if (!joint) return;

        currentGrapplePos = Vector3.Lerp(currentGrapplePos, swingPort, Time.deltaTime * 8f);



        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePos);

    }

    private void StartSwing()
    {


      
        if(predicionHit.point == Vector3.zero)
        {
            return;
        }

        playerMovement.swinging = true;

        swingPort = predicionHit.point;
        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = swingPort;

        float disFromPoint = Vector3.Distance(player.position, swingPort);

        joint.maxDistance = disFromPoint * 0.8f;
        joint.minDistance = disFromPoint * 0.25f;

        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;

        lr.positionCount = 2;
        currentGrapplePos = gunTip.position;
          
        
    }

    private void StopSwing()
    {
        playerMovement.swinging = false;

        lr.positionCount = 0;
        Destroy(joint);
    }

    private void AirControl()
    {
        if(Input.GetKey(KeyCode.D))
        {
            rb.AddForce(orientation.right * horizontalThrust * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-orientation.right * horizontalThrust * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(orientation.forward * fowardThrust * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-orientation.forward * fowardThrust * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 DirToPoint = swingPort - transform.position;
            rb.AddForce(DirToPoint.normalized * fowardThrust * Time.deltaTime);

            float DtoP = Vector3.Distance(transform.position, swingPort);

            joint.maxDistance = DtoP * 0.8f;
            joint.minDistance = DtoP * 0.25f;
        }

        /*
        if (Input.GetKey(KeyCode.S))
        {


            float Edfp = Vector3.Distance(transform.position, swingPort) + extendCableSpeed;

            joint.maxDistance = Edfp * 0.8f;
            joint.minDistance = Edfp * 0.25f;
        }
        */
    }

    private void CheckforSwingPoints()
    {
        if(joint != null)
        {
            return;
        }

        RaycastHit shperecastHit;
        Physics.SphereCast(cam.position, predictionSphereCastRad, cam.forward, out shperecastHit, MaxSwingDistance, whatIsGrapple);

        RaycastHit RCH;
        Physics.Raycast(cam.position, cam.forward, out RCH, MaxSwingDistance, whatIsGrapple);

        Vector3 RealHitpoint;

        if(RCH.point != Vector3.zero)
        {
            RealHitpoint = RCH.point;
        }
        else if(shperecastHit.point != Vector3.zero)
        {
            RealHitpoint = shperecastHit.point;
        }

        else
        {
            RealHitpoint = Vector3.zero; 
        }


        if(RealHitpoint != Vector3.zero)
        {
            PredictionPoint.gameObject.SetActive(true);
            PredictionPoint.position = RealHitpoint;
        }

        else
        {
            PredictionPoint.gameObject.SetActive(false);
        }

        predicionHit = RCH.point == Vector3.zero ? shperecastHit : RCH;

    }
}
