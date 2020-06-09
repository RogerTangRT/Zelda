using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este Objeto não é associado a nada da cena.

// Faz aparecer no Menu do Unity para ser criado como uma nova variável
[CreateAssetMenu]
public class SignalList : ScriptableObject
{
    public List<SignalListener> m_Listeners = new List<SignalListener>();
    public void Raise()
    {
        foreach (SignalListener listener in m_Listeners)
        {
            listener.OnSignalRaised();
        }
    }
    public void RegisterListener(SignalListener listener)
    {
        m_Listeners.Add(listener);
    }
    public void DeRegisterListener(SignalListener listener)
    {
        m_Listeners.Remove(listener);
    }
}
