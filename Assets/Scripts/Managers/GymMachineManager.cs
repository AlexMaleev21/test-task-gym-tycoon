using UnityEngine;

/* Manages a single gym machine logic. Contains save system methods*/
public class GymMachineManager : MonoBehaviour, ISaveable<GymMachineSaveData>
{
    public GymMachineData data;
    private GymMachineSpawner spawner;
    private float timer;

    /* Sets up the machine */
    public void Setup(GymMachineData machineData, GymMachineSpawner parentSpawner)
    {
        data = machineData;
        spawner = parentSpawner;
    }

    /* Updates the machine timer and collects profit when iteration ends */
    void Update()
    {
        if (GetTotalCount() > 0)
        {
            timer += Time.deltaTime;

            float buffedDuration = GetCycleDuration() * BuffManager.Instance.GetSpeedMultiplier();

            if (timer >= buffedDuration)
            {
                timer = 0;
                CollectProfit();
            }
        }
    }

    /* Calculates and adds profit to the player currency, applying possible shake buffs */
    void CollectProfit()
    {
        float buffedProfit = GetProfitPerCycle() * GetTotalCount() * BuffManager.Instance.GetIncomeMultiplier();

        if (Random.value < BuffManager.Instance.GetLuckyChance())
        {
            buffedProfit *= 2f;
            Debug.Log("LuckyShake proceeded!");
        }

        CurrencyManager.Instance.AddMoney(buffedProfit);
    }

    public float GetProfitPerCycle()
    {
        return data.iterationProfit * Mathf.Pow(data.incomeMultiplier, data.incomeLevel - 1);
    }

    public float GetCycleDuration()
    {
        return data.iterationDuration * Mathf.Pow(data.speedMultiplier, data.speedLevel - 1);
    }

    public int GetTotalCount()
    {
        return data.count + data.countLevel;
    }

    /* Returns save data for further JSON writing */
    public GymMachineSaveData GetSaveData()
    {
        return new GymMachineSaveData
        {
            machineName = data.machineName,
            count = data.count,
            incomeLevel = data.incomeLevel,
            speedLevel = data.speedLevel,
            countLevel = data.countLevel
        };
    }

    /* Loads machine state from saved data */
    public void LoadFromSaveData(GymMachineSaveData saveData)
    {
        if (saveData == null || saveData.machineName != data.machineName) return;

        data.count = saveData.count;
        data.incomeLevel = saveData.incomeLevel;
        data.speedLevel = saveData.speedLevel;
        data.countLevel = saveData.countLevel;
    }
}
