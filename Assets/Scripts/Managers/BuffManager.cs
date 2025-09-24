using System.Collections;
using UnityEngine;

/*Handles temporary effects applied by shakes.*/
public class BuffManager : MonoBehaviour
{
    public static BuffManager Instance;

    private float incomeMultiplier = 1f;
    private float speedMultiplier = 1f;
    private float luckyChance = 0f;

    public void Initialize()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /* Starts applying a shake buffs */
    public void ApplyShake(ShakeData shake)
    {
        StartCoroutine(ApplyBuff(shake));
    }

    /* Coroutine that applies buffs for a duration then removes */
    private IEnumerator ApplyBuff(ShakeData shake)
    {
        Debug.Log($"{shake.shakeName} activated");

        incomeMultiplier *= shake.incomeMultiplier;
        speedMultiplier *= shake.speedMultiplier;
        luckyChance += shake.luckyChance;

        yield return new WaitForSeconds(shake.duration);

        incomeMultiplier /= shake.incomeMultiplier;
        speedMultiplier /= shake.speedMultiplier;
        luckyChance -= shake.luckyChance;

        Debug.Log($"ran out of {shake.shakeName}");
    }

    public float GetIncomeMultiplier() => incomeMultiplier;

    public float GetSpeedMultiplier() => speedMultiplier;

    public float GetLuckyChance() => luckyChance;
}
