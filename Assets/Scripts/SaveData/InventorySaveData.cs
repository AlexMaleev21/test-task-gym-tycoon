using System;
using System.Collections.Generic;

[Serializable]
public class IngredientSlotSave
{
    public string ingredientName; 
    public int count;
}

[Serializable]
public class ShakeSave
{
    public string shakeName; 
}

[Serializable]
public class InventorySaveData
{
    public List<IngredientSlotSave> ingredients = new List<IngredientSlotSave>();
    public List<ShakeSave> shakes = new List<ShakeSave>();
}

