using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este Objeto não é associado a nada da cena.

// Faz aparecer no Menu do Unity para ser criado como uma nova variável
[CreateAssetMenu]
public class SignalList : ScriptableObject
{
    #region Variables
    // Lista de Sinais
    public List<SignalListener> m_Listeners = new List<SignalListener>();
    #endregion

    /// <summary>
    /// Dispara todos os eventos associados
    /// </summary>
    public void Raise()
    {
        foreach (SignalListener listener in m_Listeners)
        {
            listener.OnSignalRaised();
        }
    }
    /// <summary>
    /// Registra evento. Adiciona na Lista
    /// </summary>
    /// <param name="listener"></param>
    public void RegisterListener(SignalListener listener)
    {
        m_Listeners.Add(listener);
    }
    /// <summary>
    /// Desregistra o evento. Remove da lista
    /// </summary>
    /// <param name="listener"></param>
    public void DeRegisterListener(SignalListener listener)
    {
        m_Listeners.Remove(listener);
    }
}
