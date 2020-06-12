using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door : Interactable
{
    public enum DoorType
    {
        key,
        enemy,
        button
    }
    [Header("Door Variables")]
    public DoorType m_DoorType;
    public bool m_Open = false;
    public Inventory m_PlayerInventory;
    public SpriteRenderer m_SpriteRendererDoor;
    public BoxCollider2D m_physiscsCollider;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public void Open()
    {
        // Turn Off door's sprite renderer
        m_SpriteRendererDoor.enabled = false;
        //Set open True;
        m_Open = true;
        // Turn off BoxCollider
        m_physiscsCollider.enabled = false;

        m_ContextOff.Raise();
        m_UseContext = false;
    }
    public override void Update()
    {
        base.Update();
    }
    public override void KeyPressed()
    {
        if(m_PlayerInRange && m_DoorType == DoorType.key)
        {
            // Does the player have key?
            if(m_PlayerInventory.m_NumberOfKeys > 0)
            {
                // Remove Player Key
                m_PlayerInventory.m_NumberOfKeys--;
                // If so call the open Method
                Open();
            }
        }
    }
}
