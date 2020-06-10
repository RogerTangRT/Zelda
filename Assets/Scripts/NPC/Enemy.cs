using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    #region Enumerator. Enemy State
    public enum EnemyState
    {
        idle,
        walk,
        attack,
        stagger
    }
    #endregion

    #region Variables
    [Header("State Machine")]
    /// <summary>
    /// Estado do inimigo
    /// </summary>
    protected EnemyState m_currentState;
    [Header("Health")]
    /// <summary>
    /// FloatValue (Global) contendo o número de vidas do inimigo
    /// </summary>
    public FloatValue m_MaxHealth;
    /// <summary>
    /// Vida Atual do inimigo
    /// </summary>
    public float m_Health;
    [Header("Enemy Status")]
    /// <summary>
    /// Nome do Inimigo
    /// </summary>
    public string m_EnemyName;
    /// <summary>
    /// Velocidade do inimigo
    /// </summary>
    public float m_MoveSpeed;
    /// <summary>
    // m_Rigidbody2D do NPC. Utilizado para Zerar sua velocidade depois de um tempo afetado por uma força
    /// </summary>
    protected Rigidbody2D m_Rigidbody2D;

    /*
    /// <summary>
    /// Dano causado pelo inimigo. Ainda não utilizado. Será usado para o Inimigo voltar a sua posição inicial
    /// </summary>
    public int m_BaseAttack;
    */
   // [Header("Dead Effects")]
   // public GameObject deadEffect;
   // private float deatEffectDelay = 1f;
    

    #endregion

    #region Start and Update
    /// <summary>
    /// Pega referência do Rigidbody2D.
    /// </summary>
    public virtual void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    #endregion

    #region Health Initialization
    /// <summary>
    /// Obtém valor inicial da Vida do FloatValue (Global). Utiliza o método Awake ao invés de Start para que o valor seja atribuído a cada reinício do jogo.
    /// </summary>
    void Awake()
    {
        m_Health = m_MaxHealth.m_InitialValue;
    }
    #endregion

    #region Knock and Damage
    /// <summary>
    /// TakeDamage. Insere o Dano no Inimigo. Se a vida chegar ao fim, some com o inimigo da cena.
    /// </summary>
    /// <param name="damage">Valor do dano</param>
    void TakeDamage(float damage)
    {
        m_Health -= damage;
        if (m_Health <= 0)
        {
            // Elimina com o objeto da cena
            this.gameObject.SetActive(false);
        }
    }
    #endregion

    #region Coroutine
    /// <summary>
    /// Zera a velocidade do RigidBody (Inimigo) após o ataque para que ele não saia da cena. 
    /// A força foi inserida na classe KnockBack.
    /// </summary>
    /// <param name="RigidBody">RigidBody a ser parado</param>
    /// <param name="knockTime">Tempo em que o inimigo deverá manter-se no estado de stagger, afastando-se do player. Padrão (0.2)s</param>
    /// <returns></returns>
    IEnumerator Knock_Coroutine(float knockTime)
    {
        // O estado do Player deverá ser stagger
        // Tempo em que o inimigo deverá manter-se no estado de stagger, afastando-se do player
        yield return new WaitForSeconds(knockTime);

        if (m_Rigidbody2D != null)
            m_Rigidbody2D.velocity = Vector3.zero;

        // Volta para idle
        m_currentState = EnemyState.idle;

    }
    #endregion

    #region Public Entry Points
    public void Knock(float knockTime, float damage)
    {
        // Entra no estado stagger para evitar colisão dupla
        m_currentState = EnemyState.stagger;
        StartCoroutine(Knock_Coroutine(knockTime));
        TakeDamage(damage);
    }
    #endregion
}
