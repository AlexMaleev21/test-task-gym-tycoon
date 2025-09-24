using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

/* Handles the crafting shakes menu */
public class ShakeStationCraftingUI : MonoBehaviour
{
    [Header("UI")]
    public Transform ingredientListParent;
    public GameObject ingredientUIPrefab;
    public Transform shakeListParent;
    public GameObject shakeUIPrefab;
    public Button craftButton;
    public TextMeshProUGUI resultText;

    [Header("Available shakes")]
    public List<ShakeData> shakes;

    private ShakeData selectedShake;

    private void Start()
    {
        RefreshUI();
        craftButton.onClick.AddListener(CraftSelectedShake);
    }

    /* Rebuilds the ingredient and shake lists in the UI. Shake items are clickable */
    public void RefreshUI()
    {
        foreach (Transform child in ingredientListParent) Destroy(child.gameObject);
        foreach (Transform child in shakeListParent) Destroy(child.gameObject);

        foreach (var slot in InventoryManager.Instance.GetAllProducts())
        {
            var cell = Instantiate(ingredientUIPrefab, ingredientListParent);
            cell.GetComponent<IngredientCellUI>().Setup(slot);
        }

        foreach (var shake in shakes)
        {
            var cell = Instantiate(shakeUIPrefab, shakeListParent);
            cell.GetComponent<ShakeCellUI>().Setup(shake, () =>
            {
                if (CanCraft(shake))
                {
                    selectedShake = shake;
                    resultText.text = $"{shake.shakeName} is chosen";
                }
                else
                {
                    resultText.text = $"Not enough ingredients for {shake.shakeName}";
                }
            });
        }
    }

    /* Checks if all required ingredients are available in inventory */
    private bool CanCraft(ShakeData shake)
    {
        foreach (var ing in shake.ingredients)
        {
            int count = InventoryManager.Instance.GetAllProducts().Find(s => s.product == ing)?.count ?? 0;
            if (count <= 0) return false;
        }
        return true;
    }

    /* Crafts the selected shake if has enough ingredients */
    private void CraftSelectedShake()
    {
        if (selectedShake == null)
        {
            resultText.text = "Choose the shake!";
            return;
        }

        if (!CanCraft(selectedShake))
        {
            resultText.text = "Not enough ingredients!";
            return;
        }

        foreach (var ingredient in selectedShake.ingredients)
            InventoryManager.Instance.RemoveProduct(ingredient, 1);

        InventoryManager.Instance.AddShake(selectedShake);
        resultText.text = $"{selectedShake.shakeName} is made!";

        RefreshUI();
    }
}
