using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CurrencyExchangeOffer")]
public class CurrencyExchangeOffers : ScriptableObject
{
    public List<CurrencyExchangeOffer> offerList;
}

[Serializable]
public class CurrencyExchangeOffer
{
    public string offerName;
    public float premiumCost;
    public float softReward;
}

