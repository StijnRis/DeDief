using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;

public class MainMenuController : UIController
{
    public GameObject loadingScreen;
    public static int balance = 0;
    public Label balanceText;

    public Button upgradeButton;
    public Label officeLevelText;
    private bool confirm = false;
    private string confirmText;
    public int secondsToConfirm = 5;
    float timer = 0.0f;
    // public int officeLevel = 1;
    private int price = 0;

    protected override void UISetup()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button startButton = root.Q<Button>("StartButton");
        upgradeButton = root.Q<Button>("UpgradeButton");
        balanceText = root.Q<Label>("BalanceText");
        officeLevelText = root.Q<Label>("OfficeLevel");

        upgradeButton.text = "Upgrade office";

        startButton.clicked += () => StartGame();
        upgradeButton.clicked += () => Upgrade();

        SetPrice();
    }

    private void Upgrade()
    {
        if (!confirm)
        {
            if (balance >= price)
            {
                confirmText = "Click to confirm upgrade";
            }
            else 
            {
                confirmText = "Not enough money";
            }
            confirm = true;
            timer = 0.0f;
        }
        else
        {
            if (balance >= price)
            {
                balance -= price;
                SceneController.officeLevel += 1;
                SetPrice();
                confirm = false;
            }
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
        Instantiate(loadingScreen);
        Destroy(gameObject);
    }

    void Update()
    {
        if (confirm) 
        {
            timer += Time.deltaTime;
            float seconds = timer % 60;
            upgradeButton.text = confirmText + " (" + Mathf.RoundToInt(secondsToConfirm - seconds).ToString() + ")";
            if (Mathf.RoundToInt(seconds) >= secondsToConfirm)
            {
                confirm = false;
            }
        }
        else
        {
            upgradeButton.text = "Upgrade office (€" + price.ToString() + ")";
        }
        balanceText.text = "Your balance: €" + balance.ToString();
        officeLevelText.text = "Current office level: " + SceneController.officeLevel.ToString();
    }

    private void SetPrice()
    {
        price = SceneController.officeLevel * 1000;
    }
}

