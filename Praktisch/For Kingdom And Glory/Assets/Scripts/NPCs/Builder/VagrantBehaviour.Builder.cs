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
    /// <summary>
    /// Build Time
    /// </summary>
    private float m_BuildIdleTimer = 0;
    /// <summary>
    /// Build Timer
    /// </summary>
    private float m_BuildIdleTime = 7.5f;
    /// <summary>
    /// Idle State
    /// </summary>
    private bool m_BuildIdle = true;

    private void Builder()
    {
        m_BuildWalls = GameObject.FindGameObjectsWithTag("Wall").ToList();

        if (m_BuildIdle)
        {
            if (Vector2.Distance(m_Target.transform.position, m_Rigid.position) <= 1.5f)
            {
                m_BuildIdleTimer += Time.deltaTime;

                m_IsIdle = true;
                m_Rigid.velocity = new Vector2(0, 0);

                if (m_BuildIdleTimer >= m_BuildIdleTime)
                {
                    m_Target = m_VillagerPoints[Random.Range(0, m_VillagerPoints.Length)];
                    m_BuildIdleTimer = 0.0f;
                    m_IsIdle = false;
                }
            }
        }

        if (m_CurrentlyReparing)
        {
            if (Vector2.Distance(m_Target.transform.position, m_Rigid.position) <= 1.5f)
            {
                if (m_Target.GetComponent<Wall>().CurrentHitPoints == m_Target.GetComponent<Wall>().MaxHitPoints)
                {
                    m_CurrentlyReparing = false;
                    m_Target.GetComponent<Wall>().BeingRepaired = false;
                    m_BuildIdle = true;
                    m_IsIdle = false;
                    return;
                }
                m_IsIdle = true;

                m_BuildTimer += Time.deltaTime;

                if (m_BuildTimer >= m_BuildTime)
                {
                    m_Target.GetComponent<Wall>().GetHealth(1);

                    m_BuildIdleTimer = 0.0f;
                }
            }
        }
    }

    #region public Functions
    /// <summary>
    /// Check if Builder has a Wall to repair
    /// </summary>
    /// <returns>if builder has a wall to repair</returns>
    public bool CheckIfHasWallToRepair()
    {
        return m_CurrentlyReparing;
    }

    /// <summary>
    /// Give Builder Wall to repair
    /// </summary>
    /// <param name="_Wall">Wall to Repair</param>
    public void WallToRepair(GameObject _Wall)
    {
        m_Target = _Wall;

        m_BuildIdle = false;
        m_CurrentlyReparing = true;
    }
    #endregion
}
