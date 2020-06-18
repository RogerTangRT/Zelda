using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    #region Variáveis
    public Vector2 m_CameraChange;
    public Vector3 m_PlayerChange;
    private CameraMovment m_camera;
    public bool m_NeedText;
    public string m_PlaceName;
    public GameObject m_Text;
    public Text m_PlaceText;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Referência da Câmera que guarda as posições maxiams e mínimas
        m_camera = Camera.main.GetComponent<CameraMovment>();
    }
    #region Handle Text

    #region Coroutine 
    // Corrotina responsável em apresentar o nome da área. Aguarda 4 segundos e some.
    private IEnumerator PlaceName_Coroutine()
    {
        // Mostra o Texto
        m_Text.SetActive(true);
        m_PlaceText.text = m_PlaceName;
        // Aguarda 4 segundos
        yield return new WaitForSeconds(4f);

        // Esconde o Texto
        m_Text.SetActive(false);
    }
    #endregion

    private void HandleText()
    {
        // Esta Flag indica se será apresentado o nome da área.
        if (m_NeedText)
        {
            StartCoroutine(PlaceName_Coroutine());
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Existem 2 Box Collider no Player. Sendo assim esta função dispara duas vezes.
        // Um dos box bolliders possui o flag Trigger ligado.
        // Sendo assim comparamos se o flag está ligado para não dispara mais de uma vez a transição.
        if (collision.CompareTag("PlayerBody") && !collision.isTrigger)
        {
            collision.transform.position += m_PlayerChange;
            m_camera.m_MinPosition += m_CameraChange;
            m_camera.m_MaxPosition += m_CameraChange;

            HandleText();
        }
    }
}
