using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public SceneController sceneController;

    private void OnEnable()
    {
        // sceneController = GameObject.FindGameObjectWithTag("Office").GetComponent<SceneController>();
        // transform.SetAsLastSibling();
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UISetup();
    }

    protected virtual void UISetup()
    {

    }
}
