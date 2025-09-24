using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* Handles ingredient shop menu*/
public class IngredientShopUI : MonoBehaviour
{
    public Transform shopListContainer;    
    public GameObject shopItemPrefab;   
    public TextMeshProUGUI moneyAmount;
    //public Button showUIButton;

    private void OnEnable()
    {
        if (CurrencyManager.Instance != null)
            CurrencyManager.Instance.OnCurrencyChanged += RefreshUI;
    }

    private void OnDisable()
    {
        if (CurrencyManager.Instance != null)
            CurrencyManager.Instance.OnCurrencyChanged -= RefreshUI;
    }

    private void Start()
    {
        RefreshUI();
    }

    /*Refreshes shop items and handles buying process*/
    private void RefreshUI()
    {
        foreach (Transform child in shopListContainer)
            Destroy(child.gameObject);

        moneyAmount.text = $"Money: {CurrencyManager.Instance.softCurrency}";

        foreach (var product in IngredientShopManager.Instance.availableProducts)
        {
            var gameObject = Instantiate(shopItemPrefab, shopListContainer);
            var ui = gameObject.GetComponent<ShopItemUI>();
            ui.Setup(product, () =>
            {
                bool success = IngredientShopManager.Instance.BuyProduct(product);
                if (!success)
                    moneyAmount.text = "Not enough money!";
            });
        }
    }
}
