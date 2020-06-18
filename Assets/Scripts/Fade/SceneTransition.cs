using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string m_SceneToLoad;
    public Vector2 m_PlayerNewPosition;
    public VectorValue m_PlayerPosition;
    [Header("New Scene Variables")]
    public Vector2 m_CameraNewMin;
    public Vector2 m_CameraNewMax;
    public VectorValue m_CameraMin;
    public VectorValue m_CameraMax;
    [Header("Transition Variables")]
    public GameObject m_FadeInPanel;
    public GameObject m_FadeOutPanel;
    public float m_FadeWait;


    /// <summary>
    /// Grava a nova posição da camera em uma variável global para ser obtida na carga na nova cena em CameraMovment
    /// </summary>
    public void ResetCameraBounds()
    {
        if (m_CameraMax != null)
            m_CameraMax.m_InitialValue = m_CameraNewMax;
        if (m_CameraMin != null)
            m_CameraMin.m_InitialValue = m_CameraNewMin;
    }
    private void Start()
    {
        ResetCameraBounds();
        // Grava a posição do Player em uma variaável Global para ser obtida em PlayeMovment
        m_PlayerPosition.m_InitialValue = m_PlayerNewPosition;
    }
    public void Awake()
    {
        // Inicia o fadein. Some com a cena. Preto
        if (m_FadeInPanel != null)
        {
            GameObject panel = Instantiate(m_FadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            // Destroi painel depois de 1 segundo
            Destroy(panel, 1);
        }
    }

    #region Coroutine
    public IEnumerator FadeCoroutine()
    {
        // Inicia o Fadeout. aparece com a cena
        if (m_FadeOutPanel != null)
        {
            Instantiate(m_FadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(m_FadeWait);
        
        // Aguarda a cena carregar
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(m_SceneToLoad);
        if (asyncOperation != null)
        {
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }
    }
    #endregion
    /// <summary>
    /// Grava a posição que o player deverá ir 
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody") && collision.isTrigger)
        {
            StartCoroutine(FadeCoroutine());
        }
    }
}
