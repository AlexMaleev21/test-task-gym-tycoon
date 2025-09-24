using UnityEngine;
using System.Collections.Generic;

/* Handles the ingredient shop logic */
public class IngredientShopManager : MonoBehaviour
{
    public static IngredientShopManager Instance;

    [Header("Shop products")]
    public List<IngredientData> availableProducts;

    public void Initialize()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /* Attempts to buy a product and returns true if purchase succeeds, false otherwise */
    public bool BuyProduct(IngredientData product)
    {
        if (CurrencyManager.Instance.SpendMoney(product.price))
        {
            InventoryManager.Instance.AddProduct(product, 1);
            return true;
        }
        else
        {
            return false;
        }
    }
}
