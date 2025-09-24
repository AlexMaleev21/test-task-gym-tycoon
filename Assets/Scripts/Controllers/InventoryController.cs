using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*controller of inventory menu*/
public class InventoryController : MonoBehaviour
{
    public GameObject inventoryUI;
    public Button inventoryButton;
    public Button closeButton;

    private void OnEnable()
    {
        inventoryButton.onClick.AddListener(ShowUI);
        closeButton.onClick.AddListener(HideUI);
    }

    private void OnDisable()
    {
        inventoryButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
    }


    public void ShowUI()
    {
        CameraDragAndZoom.IsInputBlocked = true;
        inventoryUI.SetActive(true);
        inventoryButton.enabled = false;

    }
    public void HideUI()
    {
        CameraDragAndZoom.IsInputBlocked = false;
        inventoryUI.SetActive(false);
        inventoryButton.enabled = true;
    }
}
