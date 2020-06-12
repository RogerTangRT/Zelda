using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinTextManager : MonoBehaviour
{
    public Inventory m_PlayerInventory;
    public TextMeshProUGUI m_TextCoinsDisplay;
    public void UpdateCoinCount()
    {
        m_TextCoinsDisplay.text = "" + m_PlayerInventory.m_Coins;
    }
}
