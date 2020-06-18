using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    #region Veriables
    public Item m_currentItem;
    /// <summary>
    /// Lista de Itens
    /// </summary>
    public List<Item> m_ItemList = new List<Item>();
    /// <summary>
    /// Quantidade de Chaves
    /// </summary>
    public int m_NumberOfKeys;
    /// <summary>
    /// Quantidade de Moedas
    /// </summary>
    public int m_Coins;
    #endregion

    public void AddItem(Item itemToAdd)
    {
        // Is the item a Key?
        if (itemToAdd.m_isKey)
        {
            m_NumberOfKeys++;
        }
        else
        {
            if (!m_ItemList.Contains(itemToAdd))
            {
                m_ItemList.Add(itemToAdd);
            }
        }
    }
    /// <summary>
    /// Verifica a existencia de um determinado item. Exemplo Espada
    /// </summary>
    /// <param name="item">Item a verificar</param>
    /// <returns>True. Item existe na lista false caso contrário</returns>
    public bool HasItem(Item item)
    {
        return m_ItemList.Contains(item);
    }
}
