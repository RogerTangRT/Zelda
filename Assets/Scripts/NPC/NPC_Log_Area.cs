using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Log_Area : NPC_Log
{
    public Collider2D m_Boundary;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }
    public override void CheckDistance()
    {
        // Se o Player está entre a distância de perseguição e o raio de atque vai na direção do player
        if (Vector3.Distance(m_Target.transform.position, transform.position) <= m_ChaseRadius &&
            Vector3.Distance(m_Target.transform.position, transform.position) > m_AttackRadius &&
            m_Boundary.bounds.Contains(m_Target.transform.position) &&
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
            if (!m_Boundary.bounds.Contains(m_Target.transform.position))
            {
                m_Animator.SetBool("NearPlayer", false);
                WakeUp(false);
            }
            
            if (Vector3.Distance(m_Target.transform.position, transform.position) < m_AttackRadius)
                m_Animator.SetBool("NearPlayer", true);
        }
    }

}
