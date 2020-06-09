using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    public GameObject m_ContextClue;

    public void Enable()
    {
        m_ContextClue.SetActive(true);
    }
    public void Disable()
    {
        m_ContextClue.SetActive(false);
    }
}
