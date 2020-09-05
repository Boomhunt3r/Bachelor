using UnityEngine;
using System.Linq;

public partial class VagrantBehaviour : MonoBehaviour
{
    /// <summary>
    /// Time Until Next Step
    /// </summary>
    private float m_BuildTime = 2.5f;
    /// <summary>
    /// Timer
    /// </summary>
    private float m_BuildTimer = 0.0f;
    /// <summary>
    /// If he's currently Reparing
    /// </summary>
    private bool m_CurrentlyReparing = false;

    private void Builder()
    {
        this.gameObject.tag = "Builder";

        m_BuildWalls = GameObject.FindGameObjectsWithTag("Wall").ToList();

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

            else if (m_ReparingWall)
            {
                if (m_CurrentlyReparing)
                {
                    if(m_BuildTime >= m_BuildTimer)
                    {
                        m_BuildWalls[m_CurrentReparingWall].GetComponent<Wall>().GetHealth(5);

                        m_BuildTimer = 0.0f;
                    }

                    if (m_BuildWalls[m_CurrentReparingWall].GetComponent<Wall>().CurrentHitPoints == m_BuildWalls[m_CurrentReparingWall].GetComponent<Wall>().MaxHitPoints)
                    {
                        m_Target = m_Waypoints[0];
                        m_ReparingWall = false;
                    }

                    m_BuildTime += Time.deltaTime;
                }
            }
        }
    }
}
