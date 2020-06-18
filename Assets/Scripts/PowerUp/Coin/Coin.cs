using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUp
{
    #region Variables
    /// <summary>
    /// Inventário
    /// </summary>
    public Inventory m_PlayerInventory;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //CoinTextManager
        //UpdateCoinCount()
        // Utilizado para atulizar o Inventário de Moedas. Mostra as moedas atuais no caso de transição de cena.

        // RAISE
        // Signal:          Signal_ReceiveCoin
        // Local Signal:    ScriptableObjects/Player/Coin
        // Capturado por:   Canvas/Coin Info
        // Método:          Script/PowerUp/Coin/CoinTextManager.cs->UpdateCoinCount()
        m_PowerUpSignal.Raise();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody") && !collision.isTrigger)
        {
            m_PlayerInventory.m_Coins += 1;
            // RAISE
            // Signal:          Signal_ReceiveCoin
            // Local Signal:    ScriptableObjects/Player/Coin
            // Capturado por:   Canvas/Coin Info
            // Método:          Script/PowerUp/Coin/CoinTextManager.cs->UpdateCoinCount()
            m_PowerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
