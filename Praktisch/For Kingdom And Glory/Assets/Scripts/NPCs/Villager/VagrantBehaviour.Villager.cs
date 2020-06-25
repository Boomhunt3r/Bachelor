using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public partial class VagrantBehaviour : MonoBehaviour
{
    private void Villager()
    {
        m_Render.color = Color.gray;
        m_Rigid.velocity = m_CurrentVeloc;

        #region Idle Path
        if (Vector2.SqrMagnitude((Vector2)m_VillagerPoints[m_CurrentWay].transform.position - m_Rigid.position) <= 5.0f)
        {
            if (m_CurrentWaypoint == 0)
            {
                m_CurrentDirection = 1;
                m_CurrentVeloc = m_Directions[0] * m_Speed * Time.deltaTime;
                m_Target = m_VillagerPoints[m_CurrentWay];
            }
            else if (m_CurrentWaypoint == 1)
            {
                m_CurrentDirection = 0;
                m_CurrentVeloc = m_Directions[1] * m_Speed * Time.deltaTime;
                m_Target = m_VillagerPoints[m_CurrentWay];
            }
        }

        #endregion

        #region if object is not active
        if (m_Hammer == null)
        {
            m_Hammer = GameObject.FindGameObjectWithTag("Hammer");
        }
        else if (m_Bow == null)
        {
            m_Bow = GameObject.FindGameObjectWithTag("Bow");
        }
        else if (m_Sword == null)
        {
            m_Sword = GameObject.FindGameObjectWithTag("Sword");
        }
        #endregion

        #region If Object is Active
        if (m_Hammer != null)
        {
            if (Vector2.SqrMagnitude((Vector2)m_Hammer.transform.position - m_Rigid.position) <= 45)
            {
                // Set Hammer to Target
                m_Target = m_Hammer;
            }
        }
        else if (m_Bow != null)
        {
            if (Vector2.SqrMagnitude((Vector2)m_Bow.transform.position - m_Rigid.position) <= 45)
            {
                // Set Bow to Target
                m_Target = m_Bow;
            }
        }
        else if (m_Sword != null)
        {
            if (Vector2.SqrMagnitude((Vector2)m_Sword.transform.position - m_Rigid.position) <= 45)
            {
                // Set Sword to Target
                m_Target = m_Sword;
            }
        } 
        #endregion
    }
}
