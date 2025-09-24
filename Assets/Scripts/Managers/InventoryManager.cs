using System.Collections.Generic;
using UnityEngine;
using System;

/* Manages player's inventory (ingredients and shakes) */
public class InventoryManager : MonoBehaviour, ISaveable<InventorySaveData>
{
    public static InventoryManager Instance;

    [Serializable]
    public class IngredientSlot
    {
        public IngredientData product;
        public int count;

        public IngredientSlot(IngredientData product, int count)
        {
            this.product = product;
            this.count = count;
        }
    }

    public List<IngredientData> ingredients;
    public List<ShakeData> shakes;
    private List<IngredientSlot> ingredientInventory = new List<IngredientSlot>();
    private List<ShakeData> shakeInventory = new List<ShakeData>();

    public void Initialize()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /* Adds a product to the inventory */
    public void AddProduct(IngredientData product, int amount = 1)
    {
        var slot = ingredientInventory.Find(s => s.product == product);
        if (slot == null)
            ingredientInventory.Add(new IngredientSlot(product, amount));
        else
            slot.count += amount;
    }

    /* Removes a product from the inventory if there is any */
    public bool RemoveProduct(IngredientData product, int amount = 1)
    {
        var slot = ingredientInventory.Find(s => s.product == product);
        if (slot == null || slot.count < amount) return false;

        slot.count -= amount;
        if (slot.count <= 0) ingredientInventory.Remove(slot);
        return true;
    }

    public List<IngredientSlot> GetAllProducts()
    {
        return new List<IngredientSlot>(ingredientInventory);
    }

    public void AddShake(ShakeData shake)
    {
        shakeInventory.Add(shake);
    }

    public List<ShakeData> GetAllShakes()
    {
        return new List<ShakeData>(shakeInventory);
    }

    /* Uses a shake, applying its buff and removing it from inventory */
    public void UseShake(ShakeData shake)
    {
        if (shakeInventory.Contains(shake))
        {
            shakeInventory.Remove(shake);
            BuffManager.Instance.ApplyShake(shake);
        }
    }

    /* Prepares inventory data for saving */
    public InventorySaveData GetSaveData()
    {
        InventorySaveData save = new InventorySaveData();

        foreach (var slot in ingredientInventory)
        {
            save.ingredients.Add(new IngredientSlotSave
            {
                ingredientName = slot.product.productName,
                count = slot.count
            });
        }

        foreach (var shake in shakeInventory)
        {
            save.shakes.Add(new ShakeSave
            {
                shakeName = shake.shakeName
            });
        }

        return save;
    }

    /* Loads inventory data from a save data */
    public void LoadFromSaveData(InventorySaveData saveData)
    {
        ingredientInventory.Clear();
        shakeInventory.Clear();

        foreach (var ingredient in saveData.ingredients)
        {
            var ingredientSO = Resources.Load<IngredientData>($"ScriptableObjects/ProductsData/{ingredient.ingredientName}");
            if (ingredientSO != null)
                ingredientInventory.Add(new IngredientSlot(ingredientSO, ingredient.count));
            else
                Debug.LogWarning($"Ingredient not found");
        }

        foreach (var shakeSave in saveData.shakes)
        {
            var shakeSO = Resources.Load<ShakeData>($"ScriptableObjects/ShakesData/{shakeSave.shakeName}");
            if (shakeSO != null)
                shakeInventory.Add(shakeSO);
            else
                Debug.LogWarning($"Shake not found");
        }
    }
}
