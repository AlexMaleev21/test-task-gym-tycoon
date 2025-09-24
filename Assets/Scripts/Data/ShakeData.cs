using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Shake Data")]
public class ShakeData : ScriptableObject
{
    public string shakeName;
    public Sprite icon;

    [Header("Recipe Ingredients")]
    public List<IngredientData> ingredients = new List<IngredientData>();

    [Header("Buff Settings")]
    public float duration = 30f; 
    public float incomeMultiplier = 1f; 
    public float speedMultiplier = 1f;  
    public float luckyChance = 0f;

    public bool Match(List<IngredientData> selected)
    {
        if (selected.Count != ingredients.Count) return false;

        return !ingredients.Except(selected).Any() && !selected.Except(ingredients).Any();
    }
}


