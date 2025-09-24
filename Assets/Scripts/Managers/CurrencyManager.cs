using System;
using UnityEngine;

/* CurrencyManager handles the player currencies.*/
public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;
    public float softCurrency = 0;
    public float premiumCurrency = 0;
    public event Action OnCurrencyChanged;
    public CurrencyHUD currencyHUD;

    public void Initialize()
    {
        if (Instance == null)
        {
            Instance = this;
            currencyHUD.gameObject.SetActive(true);
        }
        else Destroy(gameObject);
    }

    public void AddMoney(float amount)
    {
        softCurrency += amount;
        OnCurrencyChanged?.Invoke();
    }

    public bool SpendMoney(float amount)
    {
        if (softCurrency >= amount)
        {
            softCurrency -= amount;
            OnCurrencyChanged?.Invoke();
            return true;
        }
        return false;
    }

    /* Exchanges premium currency for soft currency*/
    public bool ExchangePremiumForSoft(float premiumCost, float softReward)
    {
        if (premiumCurrency >= premiumCost)
        {
            premiumCurrency -= premiumCost;
            softCurrency += softReward;

            OnCurrencyChanged?.Invoke();
            return true;
        }
        ;
        return false;
    }

    public void NotifyCurrencyChanged()
    {
        OnCurrencyChanged?.Invoke();
    }
}
