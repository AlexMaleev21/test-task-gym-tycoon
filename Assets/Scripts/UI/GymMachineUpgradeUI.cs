using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* Handles the upgrading a gym machine menu*/
public class GymMachineUpgradeUI : MonoBehaviour
{
    public static GymMachineUpgradeUI Instance;

    [Header("UI Elements")]
    public GameObject menuPanel;
    public TextMeshProUGUI machineNameText;
    public Button incomeButton;
    public Button speedButton;
    public Button countButton;
    public Button closeButton;

    private GymMachineData currentData;
    private GymMachineSpawner currentSpawner;

    [Header("Levels UI")]
    public TextMeshProUGUI incomeLevelText;
    public TextMeshProUGUI speedLevelText;
    public TextMeshProUGUI countLevelText;

    [Header("Manager")]
    public GymMachineUpgradeManager gymMachineUpgradeManager;
    public void Initialize()
    {
        Instance = this;
        menuPanel.SetActive(false);

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseMenu);
    }

    /* Opens upgrade menu for the clicked on machine and sets menu buttons listeners */
    public void OpenMenu(GymMachineData data, GymMachineSpawner spawner)
    {
        CameraDragAndZoom.IsInputBlocked = true;
        currentData = data;
        currentSpawner = spawner;
        menuPanel.SetActive(true);
        machineNameText.text = data.machineName;

        incomeButton.onClick.RemoveAllListeners();
        speedButton.onClick.RemoveAllListeners();
        countButton.onClick.RemoveAllListeners();

        incomeButton.onClick.AddListener(() => gymMachineUpgradeManager.UpgradeIncome(currentData, UpdateUI));
        speedButton.onClick.AddListener(() => gymMachineUpgradeManager.UpgradeSpeed(currentData, UpdateUI));
        countButton.onClick.AddListener(() => gymMachineUpgradeManager.UpgradeCount(currentData, UpdateUI, spawner));

        UpdateUI();
    }

    /* Updates the level text for income, speed and count */
    private void UpdateUI()
    {
        incomeLevelText.text = $"Level: {currentData.incomeLevel}/{currentData.maxLevelCount}";
        speedLevelText.text = $"Level: {currentData.speedLevel}/{currentData.maxLevelCount}";
        countLevelText.text = $"Level: {currentData.countLevel}/{currentData.maxMachineCount}";
    }

    public void CloseMenu()
    {
        CameraDragAndZoom.IsInputBlocked = false;
        menuPanel.SetActive(false);
    }

}
