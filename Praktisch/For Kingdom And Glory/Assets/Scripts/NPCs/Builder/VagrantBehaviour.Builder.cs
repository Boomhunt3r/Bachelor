using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class VagrantBehaviour : MonoBehaviour
{
    private void Builder()
    {
        m_Render.color = Color.green;
        this.gameObject.tag = "Builder";

        m_BuildWalls = GameObject.FindGameObjectsWithTag("Wall").OfType<GameObject>().ToList();

        if (m_BuildWalls.Count != 0 || m_BuildWalls != null)
        {
            if (!m_ReparingWall)
            {
                for (int i = 0; i < m_BuildWalls.Count; i++)
                {
                    if (m_BuildWalls[i].GetComponent<Wall>().CurrentHitPoints <= m_BuildWalls[i].GetComponent<Wall>().MaxHitPoints)
                    {
                        m_Target = m_BuildWalls[i];

                        m_CurrentReparingWall = i;

                        m_ReparingWall = true;
                    }
                }
            }

            else if(m_ReparingWall)
            {
                if(m_BuildWalls[m_CurrentReparingWall].GetComponent<Wall>().CurrentHitPoints == m_BuildWalls[m_CurrentReparingWall].GetComponent<Wall>().MaxHitPoints)
                {
                    m_ReparingWall = false;
                }


            }
        }
    }
}
