using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button startButton = root.Q<Button>("StartButton");

        startButton.clicked += () => startGame();
    }

    private void startGame()
    {
        SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
    }
}
