using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] m_HeartsList;
    public Sprite m_Full_Heart;
    public Sprite m_HalfFull_Heart;
    public Sprite m_Empty_Heart;
    public FloatValue m_HeartConteiners;
    public FloatValue m_PlayerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }
    public void InitHearts()
    {
        // Inicializa todos corações cheios
        for (int i = 0; i < m_HeartConteiners.m_InitialValue; i++)
        {
            m_HeartsList[i].gameObject.SetActive(true);
            m_HeartsList[i].sprite = m_Full_Heart;
        }
        UpdateHearts();
    }
    public void UpdateHearts()
    {
        // Tem X corações e 2X meios corações.
        float tempHealth = m_PlayerCurrentHealth.m_RuntimeValue / 2;
        for (int i = 0; i < m_HeartConteiners.m_InitialValue; i++)
        {
            if (i <= tempHealth-1)
            {
                // Full Heart
                m_HeartsList[i].sprite = m_Full_Heart;
            }
            else
            {
                if (i >= tempHealth)
                {
                    // Empty Heart
                    m_HeartsList[i].sprite = m_Empty_Heart;
                }
                else
                {
                    // Half full Heart
                    m_HeartsList[i].sprite = m_HalfFull_Heart;
                }
            }
        }
    }
}
