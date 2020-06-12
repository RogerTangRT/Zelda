using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item m_currentItem;
    public List<Item> m_ItemList = new List<Item>();
    public int m_NumberOfKeys;
    public int m_Coins;

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
    public bool HasItem(Item item)
    {
        return m_ItemList.Contains(item);
    }
}
