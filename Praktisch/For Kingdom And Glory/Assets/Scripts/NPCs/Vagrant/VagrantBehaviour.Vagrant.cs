using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public partial class VagrantBehaviour : MonoBehaviour
{
    private void Vagrant()
    {
        Vector2 Direction = ((Vector2)m_Path.vectorPath[m_CurrentWaypoint] - m_Rigid.position).normalized;
        Vector2 force = Direction * m_Speed * Time.deltaTime;

        m_Rigid.AddForce(force);

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


        if (m_Coin == null)
        {
            m_Coin = GameObject.FindGameObjectWithTag("Coin");
            m_Target = m_Waypoints[m_CurrentDirection];
        }

        if (m_Coin != null)
        {
            if (Vector2.SqrMagnitude((Vector2)m_Coin.transform.position - m_Rigid.position) <= 45)
            {
                Debug.Log("In Range");
                m_Target = m_Coin;
            }
        }
    }
}
