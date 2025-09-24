using System.Collections.Generic;
using UnityEngine;

/* contains all gym zone managers (right now for saving purposes)*/
public class LockedGymZonesManager : MonoBehaviour
{
    public static LockedGymZonesManager Instance;

    public List<GymZoneManager> zones = new List<GymZoneManager>();
    public ZonePurchaseUI purchaseController;

    public void Initialize()
    {
        if (Instance == null)
        {
            Instance = this;
            foreach(var zone in zones)
            {
                zone.Initialize();
            }
        }
        else Destroy(gameObject);
    }
}

