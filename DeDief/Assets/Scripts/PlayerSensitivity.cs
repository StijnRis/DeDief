using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSensitivity : MonoBehaviour
{
    int n;
    public TextMeshProUGUI sensitivityText;
    public Slider sensitivitySlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sensitivityText.text = "Camera Sensitivity: " + sensitivitySlider.value;
    }
}
