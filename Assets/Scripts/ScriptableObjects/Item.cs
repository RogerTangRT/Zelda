using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Item : ScriptableObject
{
    /// <summary>
    /// Registro de Item
    /// </summary>
    #region Variables
    public Sprite m_ItemSprite;
    public string m_ItemDescription;
    public bool m_isKey;
    #endregion
}
