using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    public float xRotation = 0f;
    public float cameraSensitivity;
    public Slider sensitivitySlider;

    //public float xSensitivity = 30f;
    //public float ySensitivity = 30f;

    public void ProcessLook(Vector2 input)
    {
        cameraSensitivity = (sensitivitySlider.value) / 500;

        if (!PlayerInteract.inventoryOpen && !PlayerInteract.settingsOpen)
        {
            float mouseX = input.x;
            float mouseY = input.y;

            xRotation -= (mouseY) * cameraSensitivity;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);

            cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

            transform.Rotate(Vector3.up * (mouseX) * cameraSensitivity);
        }
    }
}
