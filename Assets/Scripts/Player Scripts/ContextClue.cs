using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    #region variables
    /// <summary>
    /// Ponto de Interrogação em cima do Player
    /// </summary>
    public GameObject m_ContextClue;
    #endregion

    /// <summary>
    /// Ativa a Imagem acima do player
    /// </summary>
    public void Enable()
    {
        m_ContextClue.SetActive(true);
    }
    /// <summary>
    /// Desativa a Imagem acima do player
    /// </summary>
    public void Disable()
    {
        m_ContextClue.SetActive(false);
    }
}
