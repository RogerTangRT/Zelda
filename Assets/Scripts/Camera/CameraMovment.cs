using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMovment : MonoBehaviour
{
    #region Variables
    [Header ("Position Variables")]
    /// <summary>
    /// Para onde a câmera deverá se mover. Usualmente o Player
    /// </summary>
    public Transform m_Target;
    /// <summary>
    /// Suavização usada na função Lerp. Geralmente (0.05)
    /// </summary>
    public float m_Smoothing;
    /// <summary>
    /// Posição máxima da sala que a câmera não poderá passar.
    /// </summary>
    public Vector2 m_MaxPosition;
    /// <summary>
    /// Posição mínima da sala que a câmera não poderá passar.
    /// </summary>
    public Vector2 m_MinPosition;

   // [Header("Aninator")]
    //public Animator m_Animator;

    [Header("Position Reset")]
    public VectorValue m_CameraMin;
    public VectorValue m_CameraMax;
    #endregion

    #region GoToPosition
    /// <summary>
    /// GoToPosition. Cria um vetor com a nova posição mantendo a posição Z da câmera na sua posição original
    /// </summary>
    /// <param name="target">Target Position</param>
    /// <returns>Nova posição Vector3 com o Z da câmera inalterado</returns>
    Vector3 GoToPosition(Vector3 target)
    {
        return new Vector3(target.x, target.y, transform.position.z);
    }
    #endregion

    #region Start and Update
    /// <summary>
    /// Inicia a posição da câmera sem altera a posição Z.
    /// </summary>
    void Start()
    {
        m_MaxPosition = m_CameraMax.m_InitialValue;
        m_MinPosition = m_CameraMin.m_InitialValue;

        //m_Animator = GetComponent<Animator>();
        // Posiciona a câmera onde está a posição do personagem (Player).
        // Esta inicialização é utilizada para quando sai de outras cenas. A posição do personagem é iniciada com um valor de onde a câmera deverá se iniciada.
        transform.position = GoToPosition(m_Target.position);
    }

    // Update is called once per frame
    // LateUpdate is called after all Update functions have been called. 
    // This is useful to order script execution. For example a follow camera should always be implemented in LateUpdate because it tracks objects that might have moved inside
    void LateUpdate()
    {
        // Verifica se o alvo (player) andou
        if (transform.position != m_Target.position)
        {
            // Cria um novo vertor 3D com a posição Z da camera na sua posição original (-10) que está em transform.position.z
            Vector3 targetPosition = GoToPosition(m_Target.position);

            // Calcula a nova posição da câmera. Clamp efetua uma interpolação linear entre os dois valores.
            // Calcula a posição intermedirária dos dois valores a cada frame, dando um efeito de retardo da câmera
            targetPosition.x = Mathf.Clamp(targetPosition.x, m_MinPosition.x, m_MaxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, m_MinPosition.y, m_MaxPosition.y);
            targetPosition.z = transform.position.z;

            transform.position = Vector3.Lerp(transform.position, targetPosition, m_Smoothing);
        }
    }
    #endregion
}
