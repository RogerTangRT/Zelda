using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Non Player Character NPC que fica dormindo e persegue o player quando está no range de alcance
public class NPC_Log : Enemy
{
    #region Variables
    /// <summary>
    /// Raio mínimo para ir de encontro ao personagem
    /// </summary>
    public float m_ChaseRadius;
    /// <summary>
    /// Raio de ataque
    /// </summary>
    public float m_AttackRadius;
    /// <summary>
    /// Posição Inicial do Personagem. Não utilizado
    /// </summary>
    // public Transform m_HomePosition;
    public Animator m_Animator;

    /// <summary>
    /// Target a ser perseguido. Player
    /// </summary>
    protected PlayerMovment m_Target;
    #endregion

    /// <summary>
    /// Inicia o NPC como Idle. Obtém a referência do Player
    /// </summary>
    public override void Start()
    {
        base.Start();
        m_currentState = EnemyState.idle;
        m_Animator = GetComponent<Animator>();
        GameObject Player = GameObject.Find("Player");
        m_Target = Player.GetComponent<PlayerMovment>();
    }

    // Update is called once per frame
    // Fixed Update. Usa o Loop de física 30 vezes por segundo.
    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
    {
        // Se o Player está entre a distância de perseguição e o raio de atque vai na direção do player
        if (Vector3.Distance(m_Target.transform.position, transform.position) <= m_ChaseRadius &&
            Vector3.Distance(m_Target.transform.position, transform.position) > m_AttackRadius &&
            m_currentState != EnemyState.stagger &&
            m_Target.m_currentState != PlayerMovment.PlayerState.die)
        {
            m_Animator.SetBool("NearPlayer", false);

            if (!m_Animator.GetBool("NearPlayer"))
            {
                // Acorda o NPC
                WakeUp(true);
                // Calcula a direção do Player
                Vector3 temp = Vector3.MoveTowards(transform.position, m_Target.transform.position, m_MoveSpeed * Time.deltaTime);
                // Vira para a direção do Player
                ChangeAnimDireciotn(temp - transform.position);
                // Vai na direção do player
                if (m_Rigidbody2D != null)
                    m_Rigidbody2D.MovePosition(temp);
            }
        }
        else
        {
            if (Vector3.Distance(m_Target.transform.position, transform.position) <= m_AttackRadius)
                m_Animator.SetBool("NearPlayer", true);
            else
                CharacterOutOfRange();
        }
    }
    protected virtual void CharacterOutOfRange()
    {
        // Apenas volta a dormir se a distancia for maior do que o raio de ataque.
        if (Vector3.Distance(m_Target.transform.position, transform.position) > m_ChaseRadius)
            // Volta a dormir
            WakeUp(false);
        else
            // Para a animação de andar
            m_Animator.SetBool("NearPlayer", true);
    }
    protected void WakeUp(bool wakeUp)
    {
        m_Animator.SetBool("wakeUp", wakeUp);
    }
    private void SetAnimationDirectionMove(Vector2 direction)
    {
        m_Animator.SetFloat("moveX", direction.x);
        m_Animator.SetFloat("moveY", direction.y);
    }
    protected void ChangeAnimDireciotn(Vector2 direction)
    {
        // Verifica a direçao que o NPC está indo (direita, acima, abaixo ou esquerda)
        // Quando as direçoes forem iguais não faz nada.

        // Andando na horizontal
        if (Math.Abs(direction.x) > Math.Abs(direction.y))
        {
            // Andando para direita
            if (direction.x > 0)
                SetAnimationDirectionMove(Vector2.right);
            else
                // Andando para esquerda
                SetAnimationDirectionMove(Vector2.left);
        }
        else
        {
            // Andando na Vertical
            if (Math.Abs(direction.x) < Math.Abs(direction.y))
            {
                // Andando para cima
                if (direction.y > 0)
                    SetAnimationDirectionMove(Vector2.up);
                else
                    // Andando para baixo
                    SetAnimationDirectionMove(Vector2.down);
            }
        }

    }
}
