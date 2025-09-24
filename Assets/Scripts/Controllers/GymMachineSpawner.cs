using System;
using UnityEngine;
using System.Collections.Generic;

/* Data structure for gym machine settings*/
[Serializable]
public class MachineSetup
{
    public GymMachineData machineData;
    public GameObject machinePrefab;
    public Transform initialSpawnPoint;
    public Vector3 machineSpawnPointsSpacing;
}

/* Spawns and placing gym machines and create gym machine buttons*/
public class GymMachineSpawner : MonoBehaviour
{
    public List<MachineSetup> machines = new List<MachineSetup>();

    public GameObject createMachineButtonObject;
    private CreateMachineButtonController controller;

    private Dictionary<GymMachineData, List<GameObject>> spawned = new Dictionary<GymMachineData, List<GameObject>>();
    public Dictionary<GymMachineData, CreateMachineButtonController> buttons = new Dictionary<GymMachineData, CreateMachineButtonController>();

    public int GetSpawnedCount(GymMachineData data)
    {
        if (!spawned.ContainsKey(data)) return 0;
        return spawned[data].Count;
    }

    private void Start()
    {
        foreach (var setup in machines)
        {
            List<GameObject> list = new List<GameObject>();
            spawned[setup.machineData] = list;

            for (int i = 0; i < setup.machineData.count; i++)
                SpawnMachine(setup, i);

            if (createMachineButtonObject != null)
            {
                GameObject buttonObj = Instantiate(createMachineButtonObject);
                CreateMachineButtonController controller = buttonObj.GetComponent<CreateMachineButtonController>();
                controller.PlaceButton(setup, this);
                buttons[setup.machineData] = controller;
            }
        }
    }

    /* Spawns gym machine considering index */
    private void SpawnMachine(MachineSetup setup, int index)
    {
        if (index >= setup.machineData.maxMachineCount) return;

        Vector3 machinePosition = setup.initialSpawnPoint.position + setup.machineSpawnPointsSpacing * index;
        GameObject obj = Instantiate(setup.machinePrefab, machinePosition, Quaternion.identity);
        spawned[setup.machineData].Add(obj);

        GymMachineManager manager = obj.GetComponent<GymMachineManager>();
        GymMachineController controller = obj.GetComponent<GymMachineController>();
        manager.Setup(setup.machineData, this);

        GymMachinesManager.machines.Add(manager);
        controller.Setup(setup.machineData, this);
    }

    /* adds and spawns machine when bought in menu*/
    public void AddMachine(GymMachineData data)
    {
        MachineSetup setup = machines.Find(m => m.machineData == data);
        if (setup == null) return;

        int currentCount = spawned[data].Count;
        if (currentCount < setup.machineData.maxMachineCount)
        {
            data.count++;
            SpawnMachine(setup, currentCount);
            if (buttons.ContainsKey(data))
                buttons[data]?.MoveToNext();
        }
        else
        {
            Debug.Log($"Max amount of {data.machineName}");
            if (buttons.ContainsKey(data))
                buttons[data]?.HideButton();
        }
    }

    /* Creates buttons for machines on initial spawn places */
    public void CreateButtonsForMachines(List<MachineSetup> setups)
    {
        foreach (var setup in setups)
        {
            if (buttons.ContainsKey(setup.machineData)) continue;

            GameObject buttonObj = Instantiate(createMachineButtonObject);
            CreateMachineButtonController controller = buttonObj.GetComponent<CreateMachineButtonController>();
            controller.PlaceButton(setup, this);
            buttons[setup.machineData] = controller;
        }
    }

    /* saves all bought machines for JSON */
    public List<MachineSetup> GetSetupsByData(List<GymMachineData> dataList)
    {
        List<MachineSetup> setups = new List<MachineSetup>();
        foreach (var data in dataList)
        {
            MachineSetup setup = machines.Find(m => m.machineData.machineName == data.machineName);
            if (setup != null)
                setups.Add(setup);
        }
        return setups;
    }
}
