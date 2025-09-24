using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

/*controller of locked gym zone object-button*/
public class LockedGymZoneController : MonoBehaviour, IPointerClickHandler
{

    public event Action OnClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked?.Invoke();
    }
}
