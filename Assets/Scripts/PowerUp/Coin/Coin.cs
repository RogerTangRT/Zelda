using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUp
{
    public Inventory m_PlayerInventory;
    //public FloatValue m_PlayerHealth;
    //public FloatValue m_HeartConteiners;
    //public float m_AmountToIncrease;

    // Start is called before the first frame update
    void Start()
    {
        m_PowerUpSignal.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody") && !collision.isTrigger)
        {
            m_PlayerInventory.m_Coins += 1;
            m_PowerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
