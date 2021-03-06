﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    #region Variables
    [Header("Value running in game")]
    public Vector2 m_InitialValue;
    [Header("Value by default when starting")]
    public Vector2 m_DefaultValue;
    #endregion

    public void OnAfterDeserialize()
    {
        m_InitialValue = m_DefaultValue;
    }

    public void OnBeforeSerialize()
    {
        //throw new System.NotImplementedException();
    }
}
