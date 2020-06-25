using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public partial class VagrantBehaviour : MonoBehaviour
{
    private void Vagrant()
    {
        m_Rigid.velocity = m_CurrentVeloc;

        #region Idle Path
        if (Vector2.SqrMagnitude((Vector2)m_Waypoints[m_CurrentDirection].transform.position - m_Rigid.position) <= 5.0f)
        {
            if (m_CurrentDirection == 0)
            {
                m_CurrentDirection = 1;
                m_CurrentVeloc = m_Directions[m_CurrentDirection] * m_Speed * Time.deltaTime;
                m_Target = m_VillagerPoints[m_CurrentWay];
            }
            else if (m_CurrentDirection == 1)
            {
                m_CurrentDirection = 0;
                m_CurrentVeloc = m_Directions[m_CurrentDirection] * m_Speed * Time.deltaTime;
                m_Target = m_VillagerPoints[m_CurrentWay];
            }
        }
        #endregion

        #region Transform
        float Distance = Vector2.Distance(m_Rigid.position, m_Path.vectorPath[m_CurrentWaypoint]);
        if (Distance < m_NextWaypointDist)
        {
            m_CurrentWaypoint++;
        }

        if (m_Rigid.velocity.x >= 0.0f)
        {
            m_Sprite.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (m_Rigid.velocity.x <= 0.0f)
        {
            m_Sprite.localScale = new Vector3(1f, 1f, 1f);
        }
        #endregion

        #region Coin Behaviour

        if (m_Coin == null)
        {
            m_Coin = GameObject.FindGameObjectWithTag("Coin");
            m_Target = m_Waypoints[m_CurrentDirection];
        }

        if (m_Coin != null)
        {
            if (Vector2.SqrMagnitude((Vector2)m_Coin.transform.position - m_Rigid.position) <= 45)
            {
                if (m_CurrentDirection == 0)
                {
                    m_CurrentVeloc = m_Directions[m_CurrentDirection];
                }
                else if (m_CurrentDirection == 1)
                {
                    m_CurrentVeloc = m_Directions[0];
                }
                m_Target = m_Coin;
            }
        } 
        #endregion
    }
}
