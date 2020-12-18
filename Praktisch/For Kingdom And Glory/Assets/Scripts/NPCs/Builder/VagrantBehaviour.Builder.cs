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

    private float m_BuildIdleTimer = 0;

    private float m_BuildIdleTime = 7.5f;

    private bool m_BuildIdle = false;

    private void Builder()
    {
        this.gameObject.tag = "Builder";

        m_BuildWalls = GameObject.FindGameObjectsWithTag("Wall").ToList();

        m_BowVis.SetActive(false);
        m_HamVis.SetActive(true);
        m_VillVis.SetActive(false);

        if (m_BuildIdle)
        {
            if (Vector2.Distance(m_Target.transform.position, m_Rigid.position) <= 1.5f)
            {
                m_BuildIdleTimer += Time.deltaTime;

                if (m_BuildIdleTimer >= m_BuildIdleTime)
                {
                    m_BuildIdle = false;
                    m_BuildIdleTimer = 0.0f;
                }
            }
        }

        if (m_BuildWalls.Count != 0 || m_BuildWalls != null)
        {
            if (!m_ReparingWall)
            {
                for (int i = 0; i < m_BuildWalls.Count; i++)
                {
                    if (m_BuildWalls[i].GetComponent<Wall>().CurrentHitPoints < m_BuildWalls[i].GetComponent<Wall>().MaxHitPoints
                     && m_BuildWalls[i].GetComponent<Wall>().Building != EBuildingUpgrade.NONE)
                    {
                        m_Target = m_BuildWalls[i];

                        m_CurrentReparingWall = i;

                        m_ReparingWall = true;
                        m_BuildIdle = false;
                    }
                }

                if (!m_ReparingWall && !m_BuildIdle)
                {
                    m_Target = m_VillagerPoints[Random.Range(0, m_VillagerPoints.Length)];
                    m_BuildIdle = true;
                }
            }

            else if (m_ReparingWall)
            {
                if (m_CurrentlyReparing)
                {
                    if (Vector2.Distance(m_Target.transform.position, m_Rigid.position) <= 1.0f)
                    {
                        m_Rigid.velocity = new Vector2(0, 0);
                    }

                    if (m_BuildWalls[m_CurrentReparingWall].GetComponent<Wall>().CurrentHitPoints == m_BuildWalls[m_CurrentReparingWall].GetComponent<Wall>().MaxHitPoints
                    && !m_BuildIdle)
                    {
                        m_Target = m_VillagerPoints[Random.Range(0, m_VillagerPoints.Length)];
                        m_BuildIdle = true;
                        m_ReparingWall = false;
                        return;
                    }

                    if (m_BuildTime >= m_BuildTimer)
                    {
                        m_BuildWalls[m_CurrentReparingWall].GetComponent<Wall>().GetHealth(5);

                        m_BuildTimer = 0.0f;
                    }


                    m_BuildTime += Time.deltaTime;
                }
            }
        }


    }
}
