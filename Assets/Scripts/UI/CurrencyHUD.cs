using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* Handles currency top panel elements */
public class CurrencyHUD : MonoBehaviour
{
    public TextMeshProUGUI softCurrencyText;
    public TextMeshProUGUI premiumCurrencyText;

    private void OnEnable()
    {
        if (CurrencyManager.Instance != null)
            CurrencyManager.Instance.OnCurrencyChanged += UpdateHUD;
    }

    private void OnDisable()
    {
        if (CurrencyManager.Instance != null)
            CurrencyManager.Instance.OnCurrencyChanged -= UpdateHUD;
    }

    private void Start()
    {
        UpdateHUD();
    }

    /* Displays up to date currency amount*/
    private void UpdateHUD()
    {
        softCurrencyText.text = $"{CurrencyManager.Instance.softCurrency:0}";
        premiumCurrencyText.text = $"{CurrencyManager.Instance.premiumCurrency:0}";
    }
}

