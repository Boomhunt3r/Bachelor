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
                    m_Target = m_VillagerPoints[Random.Range(0, m_VillagerPoints.Length)];
                    m_BuildIdleTimer = 0.0f;
                }
            }
        }

        if (m_CurrentlyReparing)
        {
            if (Vector2.Distance(m_Target.transform.position, m_Rigid.position) <= 1.5f)
            {
                if(m_Target.GetComponent<Wall>().CurrentHitPoints == m_Target.GetComponent<Wall>().MaxHitPoints)
                {
                    m_CurrentlyReparing = false;
                    m_Target.GetComponent<Wall>().BeingRepaired = false;
                    m_BuildIdle = true;
                    return;
                }

                m_BuildTimer += Time.deltaTime;

                if(m_BuildTimer >= m_BuildTime)
                {
                    m_Target.GetComponent<Wall>().GetHealth(1);

                    m_BuildIdleTimer = 0.0f;
                }
            }
        }
    }

    public bool CheckIfHasWallToRepair()
    {
        return m_CurrentlyReparing;
    }

    public void WallToRepair(GameObject _Wall)
    {
        m_Target = _Wall;

        m_BuildIdle = false;
        m_CurrentlyReparing = true;
    }
}
