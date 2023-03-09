using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameOverController : UIController
{
    private Label valueText;
    private Label balanceText;

    public static bool didPlayerDie = false;

    public GameObject loadingScreen;
    public static int addToBalance;

    protected override void UISetup()
    {
        // sceneController.player.GetComponent<PlayerHealth>().ResetHealth();
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button menuButton = root.Q<Button>("MenuButton");
        Button playButton = root.Q<Button>("PlayButton");
        valueText = root.Q<Label>("ValueText");
        balanceText = root.Q<Label>("BalanceText");

        MainMenuController.balance += addToBalance;
        SetMoneyValue(addToBalance, MainMenuController.balance);

        menuButton.clicked += () => MainMenu();
        playButton.clicked += () => PlayAgain();
    }

    public void SetMoneyValue(int moneyValue, int balance)
    {
        valueText.text = "You have stolen: €" + moneyValue.ToString();
        balanceText.text = "Balance: €" + balance.ToString();
        if (didPlayerDie)
        {
            valueText.text = valueText.text + " (you died)";
        }
    }

    private void PlayAgain()
    {
        Debug.Log("clack");
        SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
        Instantiate(loadingScreen);
        Destroy(gameObject);
    }

    private void MainMenu()
    {
        Debug.Log("click");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Instantiate(loadingScreen);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("gone");
    }
}

