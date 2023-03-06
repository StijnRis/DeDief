using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : UIController
{
    public GameObject loadingScreen;

    protected override void UISetup()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button startButton = root.Q<Button>("StartButton");

        startButton.clicked += () => StartGame();
    }

    private void StartGame()
    {
        SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
        Instantiate(loadingScreen);
        Destroy(gameObject);
    }
}

