using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_PatrolLog : NPC_Log
{
    public Transform[] m_Path;
    public int m_CurrentPoint;
    public Transform m_CurrentGoal;
    public float m_RoudingDistance;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        WakeUp(true);
    }

    // Update is called once per frame
    void Update()
    {

    }


    protected override void CharacterOutOfRange()
    {
        if (Vector3.Distance(transform.position, m_Path[m_CurrentPoint].position) > m_RoudingDistance)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, m_Path[m_CurrentPoint].position, m_MoveSpeed * Time.deltaTime);
            // Vira para a direção do Player
            ChangeAnimDireciotn(temp - transform.position);
            // Vai na direção do player
            if (m_Rigidbody2D != null)
                m_Rigidbody2D.MovePosition(temp);
        }
        else
            ChangeGoal();
    }

    private void ChangeGoal()
    {
        if (m_CurrentPoint == m_Path.Length - 1)
        {
            m_CurrentPoint = 0;
            m_CurrentGoal = m_Path[0];
        }
        else
        {
            m_CurrentPoint++;
            m_CurrentGoal = m_Path[m_CurrentPoint];
        }
    }
}
