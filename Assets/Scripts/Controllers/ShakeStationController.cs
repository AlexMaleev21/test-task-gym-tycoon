using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*controller of shake station menu*/
public class ShakeStationController : MonoBehaviour, IPointerClickHandler
{
    public GameObject stationMenuUI;
    public ShakeStationCraftingUI stationMenu; 
    public Button closeButton;

    private void Awake()
    {
        if (stationMenu != null)
            stationMenuUI.SetActive(false);
    }

    private void OnEnable()
    {
        closeButton.onClick.AddListener(CloseMenu);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveAllListeners();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CameraDragAndZoom.IsInputBlocked = true;
        stationMenuUI.SetActive(true);
        stationMenu.RefreshUI();
    }

    private void CloseMenu()
    {
        CameraDragAndZoom.IsInputBlocked = false;
        stationMenuUI.SetActive(false);
    }
}

