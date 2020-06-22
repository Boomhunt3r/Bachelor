using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public partial class VagrantBehaviour : MonoBehaviour
{
    private void Villager()
    {
        m_Render.color = Color.gray;
        m_Target = m_VillagerPoint;

        if (m_Hammer == null)
        {
            m_Bow = GameObject.FindGameObjectWithTag("Hammer");
        }
        else if (m_Bow == null)
        {
            m_Bow = GameObject.FindGameObjectWithTag("Bow");
        }
        else if (m_Sword == null)
        {
            m_Sword = GameObject.FindGameObjectWithTag("Sword");
        }

        if (m_Hammer != null)
        {
            if (Vector2.SqrMagnitude((Vector2)m_Hammer.transform.position - m_Rigid.position) <= 45)
            {
                Debug.Log("In Range");
                m_Target = m_Hammer;
            }
        }
        else if (m_Bow != null)
        {
            if (Vector2.SqrMagnitude((Vector2)m_Bow.transform.position - m_Rigid.position) <= 45)
            {
                Debug.Log("In Range");
                m_Bow = m_Coin;
            }
        }
        else if (m_Sword != null)
        {
            if (Vector2.SqrMagnitude((Vector2)m_Sword.transform.position - m_Rigid.position) <= 45)
            {
                Debug.Log("In Range");
                m_Sword = m_Coin;
            }
        }
    }
}
