using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp
{
    #region Variables
    /// <summary>
    /// Vida do Player
    /// </summary>
    public FloatValue m_PlayerHealth;
    /// <summary>
    /// Número de vidas a acrescenta para o corção obtido. Geralmente 2 metadas. 1 coração inteiro
    /// </summary>
    public float m_AmountToIncrease;
    #endregion

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBody") && !collision.isTrigger)
        {
            // Sempre incrementa o valor do coração. Geralmente 2 metades.
            m_PlayerHealth.m_RuntimeValue += m_AmountToIncrease;
            // Se ultrapassou o valor da vida máxima inicial, geralmente 6, limita para a vida máxima inicial.
            // O algoritimo vai mostrar 3 metades
            if(m_PlayerHealth.m_RuntimeValue > m_PlayerHealth.m_InitialValue)
            {
                m_PlayerHealth.m_RuntimeValue = m_PlayerHealth.m_InitialValue;
            }

            // RAISE
            // Signal:          Signal_ReceiveCoin
            // Local Signal:    ScriptableObjects/Player/Coin
            // Capturado por:   Canvas/Coin Info
            // Método:          Script/Player Scripts/HeartManager.cs->UpdateHearts()
            m_PowerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
