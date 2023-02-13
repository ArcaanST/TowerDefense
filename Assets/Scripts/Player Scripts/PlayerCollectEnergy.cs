using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectEnergy : MonoBehaviour
{
    [SerializeField]
    private float collectTime = 0.4f;

    private PlayerMovement playerMovement;
    private PlayerBackpack backpack;

    private AudioSource audioSource;

    private Collider2D collidedEnergy;

    private bool canGetEnergy;

    Energy energy;
    [SerializeField]
    Collider2D PlayerCollider;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        backpack = GetComponent<PlayerBackpack>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canGetEnergy
            && backpack.currentNumberOfStoredEnergies != backpack.maxNumberOfEnergiesToStore)
        {
            TryGetEnergy();
        }
    }

    void TryGetEnergy()
    {
        if (!canGetEnergy)
            return;

        if (collidedEnergy != null)
        {
            energy = collidedEnergy.GetComponent<Energy>();
            
            if(energy.energyCount == 1)
            {
                energy.energyCount = 0;
                playerMovement.HarvestStopMovement(collectTime);
                backpack.AddEnergy(1);
                audioSource.Play();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Energy"))
        {
            canGetEnergy = true;
            collidedEnergy = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Energy"))
        {
            canGetEnergy = false;
            collidedEnergy = null;
            Destroy(collision.gameObject);
        }
    }
}
