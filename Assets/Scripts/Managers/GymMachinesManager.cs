using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/*contains all gym machine managers (right now for saving purposes)*/
public class GymMachinesManager : MonoBehaviour
{

    public static GymMachinesManager Instance;

    public static List<GymMachineManager> machines = new List<GymMachineManager>();
    public void Initialize()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }
}
