using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public partial class VagrantBehaviour : MonoBehaviour
{
    private void Vagrant()
    {
        #region Transform

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
                m_Target = m_Coin;
            }
        } 
        #endregion
    }
}
