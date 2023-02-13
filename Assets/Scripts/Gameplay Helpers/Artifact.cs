using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    public int health;
    public int maxHealth;

    private AudioSource audioSource;

    private PlayerBackpack playerBackpack;

    [SerializeField]
    GameObject[] sheeps;

    private int currentSheep;
    private int maxSheep;

    float timer = 30;
    float currentTimer = 0;

    private void Awake()
    {
        maxHealth = sheeps.Length;
        maxSheep = sheeps.Length;
        currentSheep = sheeps.Length;
        health = sheeps.Length;

        audioSource = GetComponent<AudioSource>();
        playerBackpack = GameObject.FindWithTag("Player").GetComponent<PlayerBackpack>();
    }

    private void Update()
    {
        /*
        currentTimer += Time.deltaTime;
        if(currentTimer >= timer)
        {
            RiseSheep();
            currentTimer = 0;
        }
        */
    }

    public void TakeDamage(int damageAmount) 
    {
        health -= damageAmount;

        sheeps[health].SetActive(false);
        currentSheep -= 1;

        CheckHealth();
    }

    void RiseSheep()
    {
        if(currentSheep < maxSheep)
        {
            health += 1;
            sheeps[health - 1].SetActive(true);
        }
    }

    void CheckHealth()
    {
        if (health <= 0) 
        {
            health = 0;
            // show game over UI
            GameOverUIController.instance.GameOver("You Lose!");

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //if (collision.GetComponent<PlayerBackpack>().currentNumberOfStoredFruits != 0)
            //    audioSource.Play();

            //health += collision.GetComponent<PlayerBackpack>().TakeFruits();

            if (playerBackpack.currentNumberOfStoredEnergies != 0)
                audioSource.Play();

            health += playerBackpack.TakeEnergy();

            if (health > maxHealth)
                health = maxHealth;
        }
    }
}