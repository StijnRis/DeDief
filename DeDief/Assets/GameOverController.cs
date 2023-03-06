using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameOverController : UIController
{
    public int moneyValue = 0;
    public int everStolenValue = 0;
    private Label valueText;
    private Label everStolenText;

    public GameObject loadingScreen;

    protected override void UISetup()
    {
        // sceneController.player.GetComponent<PlayerHealth>().ResetHealth();
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button menuButton = root.Q<Button>("MenuButton");
        Button playButton = root.Q<Button>("PlayButton");
        valueText = root.Q<Label>("ValueText");
        everStolenText = root.Q<Label>("EverStolenText");

        moneyValue = SceneController.totalValue;
        everStolenValue = SceneController.totalEverStolen;
        SetMoneyValue(moneyValue, everStolenValue);

        menuButton.clicked += () => MainMenu();
        playButton.clicked += () => PlayAgain();
    }

    public void SetMoneyValue(int moneyValue, int everStolenValue)
    {
        valueText.text = "Total value of inventory: €" + moneyValue.ToString();
        everStolenText.text = "Total value ever stolen: €" + everStolenValue.ToString();
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

