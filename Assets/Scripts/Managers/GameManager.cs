using UnityEngine;

/* Central hub for initializing and handling core managers, UI, saves */
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Core managers and UI")]
    public CurrencyManager currencyManager;
    public GymMachineSpawner machineSpawner;
    public GymMachineUpgradeUI upgradeMenu;
    public InventoryManager inventoryManager;
    public BuffManager buffManager;
    public IngredientShopManager ingredientShop;
    public LockedGymZonesManager lockedGymZonesManager;
    public SaveManager saveManager;

    /* SWITCH THIS ON TO RESET DATA */
    [Header("Game Settings")]
    public bool resetProgressOnStart;

    /* Initializes managers and loads game data */
    private void Awake()
    {
        Initialize();
        currencyManager.Initialize();
        inventoryManager.Initialize();
        buffManager.Initialize();
        upgradeMenu.Initialize();
        ingredientShop.Initialize();
        lockedGymZonesManager.Initialize();
        saveManager.Initialize();
        LoadGame();
    }

    private void Initialize()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    /* Resets all machines to their initial state */
    private void ResetGame()
    {
        foreach (var setup in machineSpawner.machines)
        {
            setup.machineData.ResetData();
        }
    }

    /* Loads saved data or resets the game depending on settings */
    private void LoadGame()
    {
        if (resetProgressOnStart)
        {
            ResetGame();
        }
        else
        {
            saveManager.LoadSave();
        }
    }

    /* save manager save method wrapper */
    public void SaveGame()
    {
        saveManager.SaveGame();
    }
}
