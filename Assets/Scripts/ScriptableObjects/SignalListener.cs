using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    #region Variables
    public SignalList m_Signal;
    public UnityEvent m_SignalEvent;
    #endregion

    public void OnSignalRaised()
    {
        // Dispara o evento associado
        m_SignalEvent.Invoke();
    }
    private void OnEnable()
    {
        // Inser na lista
        m_Signal.RegisterListener(this);
    }
    private void OnDisable()
    {
        // Remove da lista
        m_Signal.DeRegisterListener(this);
    }
}
