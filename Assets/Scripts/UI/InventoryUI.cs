using UnityEngine;
using UnityEngine.UI;

/* Manages the player's inventory menu */
public class InventoryUI : MonoBehaviour
{
    public Transform listParent;
    public GameObject shakeUIPrefab;
    public GameObject ingredientUIPrefab;

    public Button shakeTabButton;
    public Button ingredientTabButton;

    private enum TabType { Shakes, Ingredients }
    private TabType currentTab = TabType.Shakes;

    private void OnEnable()
    {
        shakeTabButton.onClick.AddListener(() => SwitchTab(TabType.Shakes));
        ingredientTabButton.onClick.AddListener(() => SwitchTab(TabType.Ingredients));
        RefreshUI();
    }

    private void OnDisable()
    {
        shakeTabButton.onClick.RemoveAllListeners();
        ingredientTabButton.onClick.RemoveAllListeners();
    }

    public void Start()
    {
        RefreshUI();
    }

    /* Switches the current tab between ingredients and shakes and updates the UI */
    private void SwitchTab(TabType tab)
    {
        currentTab = tab;
        RefreshUI();
    }

    /* Rebuilds the UI list based on the selected tab, either ingredients or shakes or used shakes */
    private void RefreshUI()
    {
        foreach (Transform child in listParent)
            Destroy(child.gameObject);

        if (currentTab == TabType.Shakes)
        {
            foreach (var shake in InventoryManager.Instance.GetAllShakes())
            {
                var go = Instantiate(shakeUIPrefab, listParent);
                go.GetComponent<ShakeCellUI>().Setup(shake, () =>
                {
                    InventoryManager.Instance.UseShake(shake);
                    RefreshUI();
                });
            }
        }
        else if (currentTab == TabType.Ingredients)
        {
            foreach (var slot in InventoryManager.Instance.GetAllProducts())
            {
                var go = Instantiate(ingredientUIPrefab, listParent);
                go.GetComponent<IngredientCellUI>().Setup(slot);
            }
        }
    }
}
