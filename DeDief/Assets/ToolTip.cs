using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    TextMeshProUGUI title;
    TextMeshProUGUI description;

    public RectTransform rectTransform;

    void Awake()
    {
        title = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        description = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    public void Init(string titleText, string descriptionText)
    {
        title.text = titleText;
        description.text = descriptionText;
    }

    void Update()
    {
        transform.SetAsLastSibling();
    }
}
