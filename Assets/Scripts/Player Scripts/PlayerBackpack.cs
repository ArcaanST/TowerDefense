using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBackpack : MonoBehaviour
{
    public int maxNumberOfEnergiesToStore = 3;
    public int currentNumberOfStoredEnergies;

    [SerializeField]
    private Text backpackInfoTxt;

    private void Start()
    {
        SetBackpackInfoText(0);
    }

    public void AddEnergy(int amount)
    {
        currentNumberOfStoredEnergies += amount;

        if (currentNumberOfStoredEnergies > maxNumberOfEnergiesToStore)
            currentNumberOfStoredEnergies = maxNumberOfEnergiesToStore;

        SetBackpackInfoText(currentNumberOfStoredEnergies);
    }

    public int TakeEnergy()
    {
        int takenEnergies = currentNumberOfStoredEnergies * 20;
        currentNumberOfStoredEnergies = 0;

        SetBackpackInfoText(currentNumberOfStoredEnergies);

        return takenEnergies;
    }

    void SetBackpackInfoText(int amount)
    {
        backpackInfoTxt.text = "Backpack: " + amount + "/" + maxNumberOfEnergiesToStore;
    }
} 