using UnityEngine;
using UnityEngine.UI;

/*controller of ingredient shop menu*/
public class IngredientShopController : MonoBehaviour
{
    public GameObject shopUI;
    public Button shopButton;
    public Button closeButton;

    private void OnEnable()
    {
        shopButton.onClick.AddListener(ShowUI);
        closeButton.onClick.AddListener(HideUI);
    }

    private void OnDisable()
    {
        shopButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
    }
    public void ShowUI()
    {
        CameraDragAndZoom.IsInputBlocked = true;
        shopUI.SetActive(true);
        shopButton.enabled = false;
    }

    public void HideUI()
    {
        CameraDragAndZoom.IsInputBlocked = false;
        shopUI.SetActive(false);
        shopButton.enabled = true;
    }
}
