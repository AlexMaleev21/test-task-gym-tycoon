using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* Singular cell with ingredient information for inventory */
public class IngredientCellUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI countText;

    public void Setup(InventoryManager.IngredientSlot slot)
    {
        nameText.text = slot.product.productName;
        countText.text = $"x{slot.count}";
    }
}

