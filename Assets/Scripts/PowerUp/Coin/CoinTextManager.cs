using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinTextManager : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Inventário do Jogador. Para obter o número do moedas
    /// </summary>
    public Inventory m_PlayerInventory;
    /// <summary>
    /// Refererencia do Texto do CANVAS
    /// </summary>
    public TextMeshProUGUI m_TextCoinsDisplay;
    #endregion

    void Start()
    {
        // Utilizado para entrada em novas cenas
        UpdateCoinCount();
    }

    public void UpdateCoinCount()
    {
        m_TextCoinsDisplay.text = "" + m_PlayerInventory.m_Coins;
    }
}
