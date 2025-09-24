using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* Handling curency exchange menu */
public class CurrencyExchangeUI : MonoBehaviour
{
    public Transform offersContainer;
    public GameObject offerButtonPrefab;
    public CurrencyExchangeOffers offersData;

    private void Start()
    {
        GenerateOfferButtons();
    }

    public void OpenMenu()
    {
        CameraDragAndZoom.IsInputBlocked = true;
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        CameraDragAndZoom.IsInputBlocked = false;
        gameObject.SetActive(false);
    }

    /* Generate cells for all aviable exchanges written in data */
    private void GenerateOfferButtons()
    {
        foreach (var offer in offersData.offerList)
        {
            GameObject buttonObj = Instantiate(offerButtonPrefab, offersContainer);
            Button button = buttonObj.GetComponent<Button>();

            TextMeshProUGUI text = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            text.text = $"{offer.offerName}\n{offer.premiumCost} -> {offer.softReward}";

            button.onClick.AddListener(() =>
            {
                if (CurrencyManager.Instance.ExchangePremiumForSoft(offer.premiumCost, offer.softReward))
                {
                    Debug.Log($"Sucessful exchange");
                }
                else
                {
                    Debug.Log($"Failed exchange. Not enough money");
                }
            });
        }
    }
}
