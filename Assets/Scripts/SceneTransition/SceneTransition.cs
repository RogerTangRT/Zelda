using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("New Scene")]
    /// <summary>
    /// Cena a ser carregada
    /// </summary>
    public string m_SceneToLoad;
    [Header("New Camera Scene Variables")]
    /// <summary>
    /// Posição mínima para a câmera na nova cena
    /// </summary>
    public Vector2 m_CameraNewMin;
    /// <summary>
    /// Posição máxima para a câmera na nova cena
    /// </summary>
    public Vector2 m_CameraNewMax;
    /// <summary>
    /// Global contendo valor minimo da camera
    /// </summary>
    public VectorValue m_CameraMin;
    /// <summary>
    /// Global contendo valor máximo da camera
    /// </summary>
    public VectorValue m_CameraMax;

    [Header("New Scene Player Position")]
    /// <summary>
    /// Onde deverá ficar o player na nova cena
    /// </summary>
    public Vector2 m_PlayerNewPosition;
    /// <summary>
    /// Posição Global do Player armazenada
    /// </summary>
    public VectorValue m_PlayerPosition;
    /// <summary>
    /// Indica se está entrando na cena para mostrar o Player de costas
    /// </summary>
    public bool m_EnteringScene;
    private Animator m_Animator;

    [Header("Transition Variables")]
    public GameObject m_FadeInPanel;
    public GameObject m_FadeOutPanel;
    public float m_FadeWait;

    /// <summary>
    /// Grava a nova posição da camera em uma variável global para ser obtida na carga na nova cena em CameraMovment
    /// </summary>
    public void ResetCameraBounds()
    {
        // Verifica se está null, pois pode omitir este valores caso não se queira resetar a camera
        if (m_CameraMax != null)
            m_CameraMax.m_InitialValue = m_CameraNewMax;
        if (m_CameraMin != null)
            m_CameraMin.m_InitialValue = m_CameraNewMin;

        // Grava a posição do Player em uma variável Global para ser obtida em PlayeMovment
        if (m_PlayerPosition != null)
            m_PlayerPosition.m_InitialValue = m_PlayerNewPosition;
    }
    private void Start()
    {
        ResetCameraBounds();

        GameObject Player = GameObject.Find("Player");
        m_Animator = Player.GetComponent<Animator>();
        // Vira o personagem de costas nas entradas das casas e da caverna.
        // A ída para o Cena princpal não utiliza.
        if (m_EnteringScene)
        {
            m_Animator.SetFloat("moveX", 0);
            m_Animator.SetFloat("moveY", 1);
        }
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
        ResetCameraBounds();
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
