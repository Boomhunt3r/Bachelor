using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public partial class VagrantBehaviour : MonoBehaviour
{
   private void Archer()
    {
        m_Render.color = Color.red;
        this.gameObject.tag = "Archer";

        m_Rabbit = GameObject.FindGameObjectsWithTag("Rabbit");

        if(m_Rabbit.Length != 0 || m_Rabbit != null)
        {
            if(!m_Hunting)
            {
                for (int i = 0; i < m_Rabbit.Length; i++)
                {
                    if(Vector2.Distance(m_Rigid.position, ((Vector2)m_Rabbit[i].transform.position)) <= 75.0f)
                    {
                        m_Target = m_Rabbit[i];
                        m_Hunting = true;
                    }
                }
            }
        }
    }
}
