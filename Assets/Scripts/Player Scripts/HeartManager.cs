using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Lista dos corações
    /// </summary>
    public Image[] m_HeartsList;
    /// <summary>
    /// Imagens dos corações
    /// </summary>
    public Sprite m_Full_Heart;
    public Sprite m_HalfFull_Heart;
    public Sprite m_Empty_Heart;
    /// <summary>
    /// Vida Atual
    /// </summary>
    public FloatValue m_PlayerCurrentHealth;

    public Image m_Sword;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Chama a função para inicializar e imprimir os corações
        InitHearts();
    }
    public void InitHearts()
    {
        // Inicializa todos corações cheios
        for (int i = 0; i < m_PlayerCurrentHealth.m_InitialValue / 2; i++)
        {
            m_HeartsList[i].gameObject.SetActive(true);
            m_HeartsList[i].sprite = m_Full_Heart;
        }
        // Atualiza os corações
        UpdateHearts();
    }
    public void ShowSword()
    {
        m_Sword.gameObject.SetActive(true);
    }
    /// <summary>
    /// UpdateHearts - Atualiza os Corações.
    /// 
    /// Indice vai de 0..2
    /// Ex.: 6 vidas -> 6/2 = 3 
    /// 0<=(3-1=2)(true):Full
    /// 1<=(2)(true);Full
    /// 2<=(2)(true);Full
    /// _
    /// Ex.: 5 vidas -> 5/2 = 2.5 
    /// 0<=(2,5-1=1.5)(true):Full
    /// 1<=(2.5-1=1.5)(true);Full
    /// 2<=(2.5-1=1.5)(false) => 2>=2.5(false)=>Half
    /// _
    /// Ex.: 4 vidas -> 4/2 = 2 
    /// 0<=(2-1=1)(true):Full
    /// 1<=(2-1=1)(true);Full
    /// 2<=(2-1=1)(false) => 2>=2(true)=>Empty
    /// _
    /// Ex.: 3 vidas -> 3/2 = 1.5 
    /// 0<=(0.5)(true):Full
    /// 1<=(0.5)(false) => 1>=1.5(false)=>Half
    /// 2<=(0.5)(false) => 2>=1.5(true)=>Empty
    /// _
    ///  Ex.: 2 vidas -> 2/2 = 1 
    ///  0<=(0)(true):Full
    ///  1<=(0)(false) => 1>=1(true)=>Empty
    ///  2<=(0)(false) => 2>=1(true)=>Empty
    ///  _
    ///  Ex.: 1 vidas -> 1/2 = 0.5 
    ///  0<=(0.5-1 = -0.5)(false) => 0>=0.5(false)=>Half
    ///  1<=(-0.5)(false) => 1>=0.5(true)=>Empty
    ///  2<=(-0.5)(false) => 2>=0.5(true)=>Empty
    ///  _
    ///  Ex.: 0 vidas -> 0/2 = 0 
    ///  0<=(0-1 = -1)(false) => 0>=0)(true)=>Empty
    ///  1<=(-0.5)(false) => 1>=0(true)=>Empty
    ///  2<=(-0.5)(false) => 2>=0(true)=>Empty
    /// </summary>
    public void UpdateHearts()
    {
        // Tem X corações e 2X meios corações.
        float tempHealth = m_PlayerCurrentHealth.m_RuntimeValue / 2;
        for (int i = 0; i < m_PlayerCurrentHealth.m_InitialValue / 2; i++)
        {
            if (i <= tempHealth - 1)
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
