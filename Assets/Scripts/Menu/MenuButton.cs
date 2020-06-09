using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerDownHandler
{
    public GameObject m_PainelMenu;

    public void ResumeGame()
    {
        m_PainelMenu.SetActive(false);
        Time.timeScale = 1;
    }
    /// <summary>
    /// Sai do Jogo
    /// </summary>
    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        m_PainelMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
