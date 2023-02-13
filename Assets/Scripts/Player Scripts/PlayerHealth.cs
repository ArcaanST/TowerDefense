using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject healthUI;

    private float scale;

    [SerializeField]
    private int maxHealth = 100;

    private int currentHealth;

    [SerializeField]
    GameOverUIController gameOver;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(currentHealth);

        scale = (float)currentHealth / maxHealth;

        healthUI.transform.localScale = new Vector3(scale, healthUI.transform.localScale.y, 1f);

        if (currentHealth <= 0)
            gameOver.GameOver("You Died!");
    }
}