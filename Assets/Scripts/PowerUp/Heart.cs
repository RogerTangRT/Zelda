using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp
{
    public FloatValue m_PlayerHealth;
    public FloatValue m_HeartConteiners;
    public float m_AmountToIncrease;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBody") && !collision.isTrigger)
        {
            m_PlayerHealth.m_RuntimeValue += m_AmountToIncrease;
            if(m_PlayerHealth.m_InitialValue > m_HeartConteiners.m_RuntimeValue * 2)
            {
                m_PlayerHealth.m_InitialValue = m_HeartConteiners.m_RuntimeValue * 2;
            }
            m_PowerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
