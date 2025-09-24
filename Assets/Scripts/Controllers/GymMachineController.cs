using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GymMachineController : MonoBehaviour, IPointerClickHandler
{
    public GymMachineData data;
    private GymMachineSpawner spawner;

    public void Setup(GymMachineData machineData, GymMachineSpawner parentSpawner)
    {
        data = machineData;
        spawner = parentSpawner;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GymMachineUpgradeUI.Instance.OpenMenu(data, spawner);
    }
}
