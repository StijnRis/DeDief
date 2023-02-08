using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;
    private PlayerInteract inv;
    
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        inv = GetComponent<PlayerInteract>();

        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        onFoot.Crouch.canceled += ctx => motor.Crouch();
        onFoot.Sprint.canceled += ctx => motor.Sprint();
        onFoot.Crawl.performed += ctx => motor.Crawl();
        onFoot.Crawl.canceled += ctx => motor.Crawl();

        onFoot.Inventory.performed += vtx => inv.OpenInventory();
        onFoot.RandomItem.performed += vtx => inv.GenerateRandomItem();
        onFoot.InsertItem.performed += vtx => inv.InsertRandomItem();
        onFoot.RotateItem.performed += vtx => inv.RotateItem();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //tell the playermotor to move using the value from our movement action.
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
