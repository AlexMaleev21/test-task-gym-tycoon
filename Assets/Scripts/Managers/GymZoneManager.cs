using System.Collections.Generic;
using UnityEngine;

/* Manages a single gym zone logic. Contains save system methods */
public class GymZoneManager : MonoBehaviour, ISaveable<LockedZoneSaveData>
{
    public string zoneName;
    public float unlockCost = 100f;
    public LockedGymZoneController lockedZoneObject;
    public GymMachineSpawner spawner;
    public List<GymMachineData> zoneData;

    [HideInInspector] public bool isUnlocked = false;

    public void Initialize()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        lockedZoneObject.OnClicked += OpenPurchaseMenu;
    }

    /* shows purchase menu UI if zone is locked */
    private void OpenPurchaseMenu()
    {
        if (!isUnlocked)
        {
            ZonePurchaseUI.Instance.OpenMenu(this);
        }
    }

    private void Start()
    {
        if (!isUnlocked)
        {
            lockedZoneObject.gameObject.SetActive(true);
        }
        else
        {
            lockedZoneObject.gameObject.SetActive(false);
        }
    }

    /* Handles unlocking the zone and spawns initial machines */
    public void UnlockZone()
    {
        if (!CurrencyManager.Instance.SpendMoney(unlockCost))
        {
            Debug.Log("Not enough money to buy this zone!");
            return;
        }

        isUnlocked = true;
        if (lockedZoneObject != null)
            lockedZoneObject.gameObject.SetActive(false);

        List<MachineSetup> secondZoneSetups = spawner.GetSetupsByData(zoneData);

        spawner.CreateButtonsForMachines(secondZoneSetups);
    }

    /* Returns the save data */
    public LockedZoneSaveData GetSaveData()
    {
        return new LockedZoneSaveData
        {
            zoneName = gameObject.name,
            isUnlocked = isUnlocked
        };
    }

    /* Loads the zone state from saved data */
    public void LoadFromSaveData(LockedZoneSaveData data)
    {
        isUnlocked = data.isUnlocked;

        if (isUnlocked)
        {
            lockedZoneObject.gameObject.SetActive(false);
        }
        else
        {
            lockedZoneObject.gameObject.SetActive(true);
        }
    }
}
