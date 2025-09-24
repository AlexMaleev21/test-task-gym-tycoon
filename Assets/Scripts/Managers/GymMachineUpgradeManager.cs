using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* Manages upgrade process */
public class GymMachineUpgradeManager : MonoBehaviour
{
    //public GymMachineData machineData;

    /* All methods calculate cost for upgrade and increases level if there is enough money*/
    public void UpgradeIncome(GymMachineData currentData, Action UpdateUI)
    {
        if (currentData.incomeLevel < currentData.maxLevelCount)
        {
            float cost = 50 * Mathf.Pow(1.5f, currentData.incomeLevel);
            if (CurrencyManager.Instance.SpendMoney(cost))
            {
                currentData.incomeLevel++;
                UpdateUI();
            }
        }
    }

    /* Upgrades machine speed if player has enough currency */
    public void UpgradeSpeed(GymMachineData currentData, Action UpdateUI)
    {
        if (currentData.speedLevel < currentData.maxLevelCount)
        {
            float cost = 100 * Mathf.Pow(1.5f, currentData.speedLevel);
            if (CurrencyManager.Instance.SpendMoney(cost))
            {
                currentData.speedLevel++;
                UpdateUI();
            }
        }
    }

    /* Upgrades machine count and spawns new machine if player has enough currency */
    public void UpgradeCount(GymMachineData currentData, Action UpdateUI, GymMachineSpawner currentSpawner)
    {
        if (currentData.countLevel < currentData.maxMachineCount)
        {
            float cost = 200 * Mathf.Pow(1.5f, currentData.countLevel);
            if (CurrencyManager.Instance.SpendMoney(cost))
            {
                currentData.countLevel++;
                currentSpawner.AddMachine(currentData);
                UpdateUI();
            }
        }
    }
}
