using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class VagrantBehaviour : MonoBehaviour
{
    private List<GameObject> m_Bows = new List<GameObject>();
    private List<GameObject> m_Hammers = new List<GameObject>();
    private GameObject m_CurrentBow;
    private GameObject m_CurrentHammer;

    private void Villager()
    {
        this.gameObject.tag = "Villager";

        if (m_BowInRange == false && m_HammerInRange == false)
        {
            m_Target = m_VillagerPoints[m_CurrentDirection];

            m_Distance = Vector2.Distance(m_Rigid.position, m_Target.transform.position);

            if (m_Distance <= 2.5f)
            {
                m_Timer += Time.deltaTime;

                m_Rigid.velocity = new Vector2(0, 0);

                if (m_Timer >= m_IdleTimer)
                {
                    m_CurrentDirection++;

                    if (m_CurrentDirection > 3)
                        m_CurrentDirection = 0;

                    m_Timer = 0.0f;
                }
            }

            if (m_Hammers.Count != 0)
            {
                // Set Hammer to Target
                m_CurrentHammer = GetClosestHammer(m_Hammers);
                // Set Tool in Range true
                m_HammerInRange = true;
            }
            if (m_Bows.Count != 0)
            {
                // Set Bow to Target
                m_CurrentBow = GetClosestBow(m_Bows);
                m_BowInRange = true;
            }
        }

        if (m_Bows.Count == 0)
        {
            m_Bows = GameObject.FindGameObjectsWithTag("Bow").ToList();
            m_Bows = GameObject.FindGameObjectsWithTag("Bow").ToList();
            m_BowInRange = false;
        }

        // If Hammer is null
        if (m_Hammers.Count == 0)
        {
            // Find Hammer Object
            m_Hammers = GameObject.FindGameObjectsWithTag("Hammer").ToList();
            // Set Tool in Range false
            m_HammerInRange = false;
        }

        if (m_BowInRange == true && m_HammerInRange == true)
        {
            m_Target = GetClosestTool(m_CurrentBow, m_CurrentHammer);
        }
        else if (m_BowInRange == true && m_HammerInRange == false)
        {
            m_Target = m_CurrentBow;
        }
        else if (m_HammerInRange == true && m_BowInRange == false)
        {
            m_Target = m_CurrentHammer;
        }

    }

    private GameObject GetClosestBow(List<GameObject> _Bows)
    {
        GameObject Target = null;
        float MinDist = Mathf.Infinity;
        Vector2 CurrentPos = transform.position;
        float Dist = 0.0f;

        for (int i = 0; i < _Bows.Count; i++)
        {
            Dist = Vector2.Distance(_Bows[i].transform.position, CurrentPos);

            if (Dist < MinDist)
            {
                Target = _Bows[i];
                MinDist = Dist;
            }
        }

        return Target;
    }

    private GameObject GetClosestHammer(List<GameObject> _Hammers)
    {
        GameObject Target = null;
        float MinDist = Mathf.Infinity;
        Vector2 CurrentPos = transform.position;
        float Dist = 0.0f;

        for (int i = 0; i < _Hammers.Count; i++)
        {
            Dist = Vector2.Distance(_Hammers[i].transform.position, CurrentPos);

            if (Dist < MinDist)
            {
                Target = _Hammers[i];
                MinDist = Dist;
            }
        }

        return Target;
    }

    private GameObject GetClosestTool(GameObject _Bow, GameObject _Hammer)
    {
        GameObject Target = null;
        Vector2 CurrentPos = transform.position;
        float DistB = 0.0f;
        float DistH = 0.0f;

        DistB = Vector2.Distance(_Bow.transform.position, CurrentPos);
        DistH = Vector2.Distance(_Hammer.transform.position, CurrentPos);

        if (DistB < DistH)
        {
            Target = _Bow;
        }
        else if (DistH < DistB)
        {
            Target = _Hammer;
        }
        else if (DistB == DistH)
        {
            Target = _Bow;
        }

        return Target;
    }
}
