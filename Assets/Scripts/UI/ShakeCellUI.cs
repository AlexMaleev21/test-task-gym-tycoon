using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

/* Singular cell with shake information for inventory and shake crafting menu */
public class ShakeCellUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Button selectButton;

    public void Setup(ShakeData shake, Action onClick)
    {
        nameText.text = shake.shakeName;
        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => onClick.Invoke());
    }
}