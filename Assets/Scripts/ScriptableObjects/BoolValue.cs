using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{
    #region Veriables
    /// <summary>
    /// Este valor nunca é alterado no jogo, sendo assim a cada início do jogo o valor m_RuntimeValue é carregado para seu valor original
    /// </summary>
    public bool m_InitialValue;
    #endregion

    // Não mostra variável no inspector
    [HideInInspector]
    public bool m_RuntimeValue;

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


