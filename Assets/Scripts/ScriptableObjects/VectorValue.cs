using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 m_InitialValue;
    public Vector2 m_DefaultValue;

    public void OnAfterDeserialize()
    {
        m_InitialValue = m_DefaultValue;
    }

    public void OnBeforeSerialize()
    {
        //throw new System.NotImplementedException();
    }
}
