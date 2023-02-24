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
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if (health <= 10)
            Debug.Log(health);

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
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }

    public void RestoreHealth(float heal)
    {
        health += heal;
        lerpTimer = 0f;
    }
}
