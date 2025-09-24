using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

/* Holds combined product data including recipes and shakes */
public class CombinedProductData
{
    public IngredientData ingredientData;
    public ShakeData shakeData;
}

/* Manages saving and loading the game, including gym machines, zones, inventory, and currency */
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public List<GymZoneManager> zones;
    public List<IngredientData> ingredients;
    public List<ShakeData> shakes;

    public static List<CombinedProductData> combinedProducts;
    private static string savePath => Path.Combine(Application.persistentDataPath, "save.json");

    public void Initialize()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    /* Loads the save from JSON */
    public void LoadSave()
    {
        Load(GymMachinesManager.machines, InventoryManager.Instance, CurrencyManager.Instance, zones);
    }

    /* Saves the current game state to JSON */
    public void SaveGame()
    {
        Save(GymMachinesManager.machines, InventoryManager.Instance, CurrencyManager.Instance, zones);
    }

    /* Converts current game state to JSON and saves */
    public void Save(List<GymMachineManager> machines, InventoryManager inventory, CurrencyManager currency, List<GymZoneManager> zones)
    {
        GameSaveData saveData = new GameSaveData();

        foreach (var machine in machines)
            saveData.machines.Add(machine.GetSaveData());

        saveData.inventory = inventory.GetSaveData();
        saveData.softCurrency = currency.softCurrency;

        foreach (var zone in zones)
            saveData.zones.Add(zone.GetSaveData());

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);
    }

    /* Reads JSON and applies it to game objects */
    public void Load(List<GymMachineManager> machines, InventoryManager inventory, CurrencyManager currency, List<GymZoneManager> zones)
    {
        if (!File.Exists(savePath)) return;

        string json = File.ReadAllText(savePath);
        GameSaveData saveData = JsonUtility.FromJson<GameSaveData>(json);

        foreach (var machineSave in saveData.machines)
        {
            var machine = machines.Find(m => m.data.machineName == machineSave.machineName);
            if (machine != null)
                machine.LoadFromSaveData(machineSave);
        }

        inventory.LoadFromSaveData(saveData.inventory);

        currency.softCurrency = saveData.softCurrency;
        currency.NotifyCurrencyChanged();

        foreach (var zoneSave in saveData.zones)
        {
            var zone = zones.Find(m => m.zoneName == zoneSave.zoneName);
            if (zone != null)
                zone.LoadFromSaveData(zoneSave);
        }
    }

    /* Automatically saves on application quit */
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    /* Automatically saves when changing scenes */
    private void OnSceneChanged(Scene previous, Scene next)
    {
        SaveGame();
    }

#if UNITY_EDITOR
    /* Saves when player exits play mode */
    private void OnEnable()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeChanged;
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            SaveGame();
        }
    }
#endif
}
