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
    public int interactableLayer;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
        inventoryController = Camera.main.GetComponent<InventoryController>();
        interactableLayer = LayerMask.NameToLayer("Interactables");
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask) && hitInfo.collider.gameObject.layer == interactableLayer)
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null && !inventoryOpen)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
            }
            if (hitInfo.collider.GetComponent<Interactable>() != null && !inventoryOpen)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
            }
        }
        if (inputManager.onFoot.Interact.triggered)
        {
            // OpenInventory();
            // Debug.Log("I am checking if you can open your inventory");
            if (Physics.Raycast(ray, out hitInfo, distance, mask))
            {
                // Debug.Log("detected object");
                if (hitInfo.collider.gameObject.layer != interactableLayer)
                {
                    // Debug.Log("You can open your inventory");
                    OpenInventory();
                    return;
                }
                if (hitInfo.collider.GetComponent<Interactable>() != null && !inventoryOpen)
                {
                    // Debug.Log("You can't open your inventory");
                    Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                    interactable.BaseInteract();
                }
            }
            else
            {
                // Debug.Log("You can open your inventory");
                OpenInventory();
            }
            // Debug.Log("I should have done something by now");
        }
    }

    public void OpenInventory()
    {
        inventoryOpen = !inventoryOpen;
        inventoryController.SetInventoryActive(inventoryOpen);
    }

    public void GenerateRandomItem()
    {
        Debug.Log("for your sanity I disabled this function");
        // inventoryController.GenerateRandomItem();
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

    public void DropItem(GameObject itemPrefab)
    {
        GameObject itemObject = Instantiate(itemPrefab);
    }
}
