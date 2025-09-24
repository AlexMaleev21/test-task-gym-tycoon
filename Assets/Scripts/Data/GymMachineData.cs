using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Gym Machine Data")]
public class GymMachineData : ScriptableObject
{

    [Header("Initial Machine Parameters")]
    public string machineName;
    public int maxMachineCount;
    public int maxLevelCount;
    public int startCount;
    public float iterationProfit;
    public float iterationDuration;

    [Header("Modifiers")]
    public int count;
    public int incomeLevel;
    public int speedLevel;
    public int countLevel;

    [Header("Multipliers")]
    public float incomeMultiplier; 
    public float speedMultiplier;

    public void ResetData()
    {
        count = startCount;
        incomeLevel = 1;
        speedLevel = 1;
        countLevel = startCount;
    }
}