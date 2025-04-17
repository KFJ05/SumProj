using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputM : MonoBehaviour
{
    private PlayerInput mPlayerInput;
    public PlayerInput.BaseMovementActions mMovementActions;
    private PlayerInteract playerI;


    PlayerMovment move;
    PLayerLook look;
    [SerializeField]
    float T;

    float Ti;

    

    // Start is called before the first frame update
    void Awake()
    {
        mPlayerInput = new PlayerInput();
        mMovementActions = mPlayerInput.BaseMovement;
        


        move = GetComponent<PlayerMovment>();
        look = GetComponent<PLayerLook>();

        playerI = GetComponent<PlayerInteract>();




        mMovementActions.Jump.performed += C => move.Jump();

        mMovementActions.Sprint.performed += C => move.sprint();

        mMovementActions.Sprint.canceled += C => move.UnSprint();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move.ProcessMove(mMovementActions.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        look.proccesLook(mMovementActions.Look.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        mMovementActions.Enable();
    }

    private void OnDisable()
    {
        mMovementActions.Disable();
    }


    



}
