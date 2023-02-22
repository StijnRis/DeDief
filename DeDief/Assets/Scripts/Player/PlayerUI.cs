using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;

    [SerializeField]
    private TextMeshProUGUI valueText;
    // Start is called before the first frame update

    public ToolTip toolTip;

    void Start()
    {
        
    }

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
        valueText.text = "Total value: €" + ValueCounter.totalValue.ToString();
    }

    public void ShowToolTip(string promptTitle, string promptDescription)
    {
        toolTip.Init(promptTitle, promptDescription);
        toolTip.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f - 100, 0);
        toolTip.gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        toolTip.gameObject.SetActive(false);
    }
}
