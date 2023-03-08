using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SeenAlert : MonoBehaviour
{
    TextMeshProUGUI title;
    TextMeshProUGUI description;

    private InventoryController inventoryController;
    public int secondsBeforeHide = 5;
    public string titleText;
    public string descriptionText;
    public string seenBy;
    float timer = 0.0f;

    void Awake()
    {
        title = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        description = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        // gameObject.SetActive(false);
    }

    public void Init(string name, InventoryController inventoryController)
    {
        this.inventoryController = inventoryController;
        transform.SetParent(inventoryController.canvas.transform);
        seenBy = name;
        timer = 0;
        // Debug.Log(inventoryController.CheckDuplicateSeenAlert(seenBy));
        if (inventoryController.CheckDuplicateSeenAlert(seenBy))
        {
            Destroy(gameObject);
        }

        title.text = titleText;
        description.text = descriptionText;
        // gameObject.SetActive(true);
    }

    void Update()
    {
        timer += Time.deltaTime;
        float seconds = timer % 60;
        description.text = descriptionText + seenBy + " (" + Mathf.RoundToInt(secondsBeforeHide - seconds).ToString() + ")";
        if (Mathf.RoundToInt(seconds) >= secondsBeforeHide)
        {
            Destroy(gameObject);
        }
    }
}
