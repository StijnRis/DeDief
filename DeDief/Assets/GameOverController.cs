using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameOverController : UIController
{
    public int moneyValue = 0;
    private Label valueText;

    protected override void UISetup()
    {
        // sceneController.player.GetComponent<PlayerHealth>().ResetHealth();
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button menuButton = root.Q<Button>("MenuButton");
        Button playButton = root.Q<Button>("PlayButton");
        valueText = root.Q<Label>("ValueText");

        moneyValue = SceneController.totalValue;
        SetMoneyValue(moneyValue);

        menuButton.clicked += () => MainMenu();
        playButton.clicked += () => PlayAgain();
    }

    public void SetMoneyValue(int moneyValue)
    {
        valueText.text = "€" + moneyValue.ToString();
    }

    private void PlayAgain()
    {
        Debug.Log("clack");
        SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
        Destroy(gameObject);
    }

    private void MainMenu()
    {
        Debug.Log("click");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("gone");
    }
}

