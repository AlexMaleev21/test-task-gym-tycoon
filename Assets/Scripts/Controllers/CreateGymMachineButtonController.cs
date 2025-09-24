using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* Controller of button that opens gym machine manipulation menu */
public class CreateMachineButtonController : MonoBehaviour, IPointerClickHandler
{
    private MachineSetup currentSetup;
    private GymMachineSpawner spawner;

    /* Button spawning, placement and initialization */
    public void PlaceButton(MachineSetup setup, GymMachineSpawner spawner)
    {
        currentSetup = setup;
        this.spawner = spawner;
        int currentCount = spawner.GetSpawnedCount(setup.machineData);

        if (currentCount < setup.machineData.maxMachineCount)
        {
            transform.position = setup.initialSpawnPoint.position + setup.machineSpawnPointsSpacing * currentCount;
            gameObject.SetActive(true);
        }
        else
        {
            HideButton();
        }
    }

    public void HideButton()
    {
        gameObject.SetActive(false);
    }

    /* Moving button to the next spawning point */
    public void MoveToNext()
    {
        PlaceButton(currentSetup, spawner);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GymMachineUpgradeUI.Instance.OpenMenu(currentSetup.machineData, spawner);
    }
}
