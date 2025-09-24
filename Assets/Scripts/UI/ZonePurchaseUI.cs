using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* Handles the UI for purchasing new gym zones */
public class ZonePurchaseUI : MonoBehaviour
{
    public static ZonePurchaseUI Instance;

    [Header("UI elements")]
    public GameObject zonePurchasePanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI costText;
    public Button buyButton;
    public Button cancelButton;

    private GymZoneManager currentZone;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /* Opens purchase menu for the selected zone */
    public void OpenMenu(GymZoneManager zone)
    {
        CameraDragAndZoom.IsInputBlocked = true;
        currentZone = zone;
        titleText.text = "Open new zone?";
        costText.text = $"Price: {zone.unlockCost}";
        zonePurchasePanel.gameObject.SetActive(true);

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => BuyZone());

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(() => CloseMenu());
    }

    private void BuyZone()
    {
        currentZone.UnlockZone();
        CloseMenu();
    }

    private void CloseMenu()
    {
        CameraDragAndZoom.IsInputBlocked = false;
        zonePurchasePanel.gameObject.SetActive(false);
        currentZone = null;
    }
}
