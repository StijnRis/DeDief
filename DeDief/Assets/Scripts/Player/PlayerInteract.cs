using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;
    // public GameObject inventory;
    private InventoryController inventoryController;
    public static bool inventoryOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
        inventoryController = Camera.main.GetComponent<InventoryController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            // Debug.Log("detected object");
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }

    public void OpenInventory()
    {
        inventoryOpen = !inventoryOpen;
        inventoryController.SetInventoryActive(inventoryOpen);
    }

    public void GenerateRandomItem()
    {
        inventoryController.GenerateRandomItem();
    }

    public void InsertRandomItem()
    {
        Debug.Log("for your sanity I disabled this function");
        // inventoryController.InsertRandomItem();
    }

    public void RotateItem()
    {
        inventoryController.RotateItem();
    }
}
