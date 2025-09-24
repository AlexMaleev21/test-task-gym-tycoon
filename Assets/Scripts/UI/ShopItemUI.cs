using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

/*handles singular shop item cell*/
public class ShopItemUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    public Button buyButton;

    public void Setup(IngredientData product, Action onBuy)
    {
        nameText.text = product.productName;
        priceText.text = $"{product.price} money";
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => onBuy?.Invoke());
    }
}

