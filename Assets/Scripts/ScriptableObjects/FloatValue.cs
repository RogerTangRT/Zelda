using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este Objeto não é associado a nada da cena.

// Faz aparecer no Menu do Unity para ser criado como uma nova variável
[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float m_InitialValue;

    // Não mostra no inspector
    [HideInInspector]
    public float m_RuntimeValue;

    public void OnAfterDeserialize()
    {
        m_RuntimeValue = m_InitialValue;
    }

    public void OnBeforeSerialize()
    {
        //throw new System.NotImplementedException();
    }
}
