using System;
using System.Collections.Generic;

[Serializable]
public class GameSaveData
{
    public List<GymMachineSaveData> machines = new List<GymMachineSaveData>();
    public InventorySaveData inventory = new InventorySaveData();
    public float softCurrency;
    public List<LockedZoneSaveData> zones = new List<LockedZoneSaveData>();
}
