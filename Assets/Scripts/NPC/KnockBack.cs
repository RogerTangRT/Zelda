using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovment;

public class KnockBack : MonoBehaviour
{
    /// <summary>
    /// Multiplicado da Força de Atque
    /// </summary>
    public float m_Thrust;
    /// <summary>
    /// Tempo que será aplicada a força
    /// </summary>
    public float m_knockTime;
    /// <summary>
    /// Dano ao Player
    /// </summary>
    public float m_Damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Somente o Player pode quebrar potes. Se não tiver a comparação com player os NPC também poderão quebrá-los
        // Compara com Player que é o Tag da espada
        if (collision.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Pot>().Smash();
        }

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("PlayerBody"))
        {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * m_Thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                // Player ataca o personagem
                // Verifica se está colidindo com o BocColider que está com o parâmtro Trigger ligado.
                // Evita entrar 2 vezes na rotina
                if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger)
                {
                    collision.GetComponent<Enemy>().Knock(m_knockTime, m_Damage);
                }

                // Personagem ataca o player
                if (collision.gameObject.CompareTag("PlayerBody"))
                {
                    // Evita entrar em estado de stagger 2 vezes
                    collision.GetComponent<PlayerMovment>().Knock(m_knockTime, m_Damage);
                }
            }
        }
    }

}
