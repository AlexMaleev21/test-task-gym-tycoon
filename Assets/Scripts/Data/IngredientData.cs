using UnityEngine;

[CreateAssetMenu(menuName = "StoreProductData")]
public class IngredientData : ScriptableObject
{
    public string productName;
    public Sprite icon;
    public int price;
}
