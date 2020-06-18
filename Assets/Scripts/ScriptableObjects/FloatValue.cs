using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este Objeto não é associado a nada da cena.

// Faz aparecer no Menu do Unity para ser criado como uma nova variável
[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    #region Veriables
    /// <summary>
    /// Este valor nunca é alterado no jogo, sendo assim a cada início do jogo o valor m_RuntimeValue é carregado para seu valor original
    /// </summary>
    public float m_InitialValue;
    #endregion

    // Não mostra variável no inspector
    [HideInInspector]
    public float m_RuntimeValue;

    #region Serialization
    public void OnAfterDeserialize()
    {
        m_RuntimeValue = m_InitialValue;
    }

    public void OnBeforeSerialize()
    {
        //throw new System.NotImplementedException();
    }
    #endregion
}
