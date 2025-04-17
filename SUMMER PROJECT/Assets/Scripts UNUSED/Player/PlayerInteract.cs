using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Start is called before the first frame update

    Camera cam;

    [SerializeField]
    float D = 3f;


    [SerializeField]
    LayerMask mask;

    PlayerUI playerUI;

    bool Interacted;

    InputM inputManager;

    void Start()
    {
        cam = GetComponent<PLayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();

        inputManager = GetComponent<InputM>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.updateText(string.Empty);

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * D);
        RaycastHit hit;
        
        if(Physics.Raycast(ray, out hit, D, mask))
        {
            if(hit.collider.GetComponent<Interact>() != null)
            {

                Interact interact = hit.collider.GetComponent<Interact>();
                playerUI.updateText(interact.PMessage);
                if(inputManager.mMovementActions.Interact.triggered)
                {
                    interact.BaseInteract();
                }

               
                //if
            }
            Debug.Log("hit");
            
        }


    }


}
