using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public float healTimer = 0f;

    public SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        healTimer = Mathf.Clamp(healTimer, 0, 50);
        UpdateHealthUI();

    }

    public void UpdateHealthUI()
    {
        // Debug.Log(health);
        
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if(fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentageComplete = lerpTimer / chipSpeed;
            percentageComplete = percentageComplete * percentageComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentageComplete);
        }
        if(fillF < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentageComplete = lerpTimer / chipSpeed;
            percentageComplete = percentageComplete * percentageComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentageComplete);
        }

        healTimer -= Time.deltaTime;

        if (healTimer <= 0 && health < 100)
        {
            RestoreHealth(1);
        }

        if (health <= 0)
        {
            sceneController = GameObject.FindGameObjectWithTag("Office").GetComponent<SceneController>();
            sceneController.endGame = true;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        healTimer = 10f;
    }

    public void RestoreHealth(float heal)
    {
        health += heal;
        lerpTimer = 0f;
    }

    public void ResetHealth()
    {
        health = maxHealth;
        lerpTimer = 0f;
    }
}
