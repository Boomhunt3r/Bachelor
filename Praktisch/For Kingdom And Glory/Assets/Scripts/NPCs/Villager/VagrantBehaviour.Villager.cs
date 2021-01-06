using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class VagrantBehaviour : MonoBehaviour
{
    private List<GameObject> m_Bows = new List<GameObject>();
    private List<GameObject> m_Hammers = new List<GameObject>();
    private GameObject m_CurrentBow;
    private GameObject m_CurrentHammer;

    #region Update function
    private void Villager()
    {
        this.gameObject.tag = "Villager";

        if(!m_ChangedSkin)
        {
            m_Animation.Skeleton.SetSkin("V1");
            m_ChangedSkin = true;
        }

        if (m_Bows.Count == 0 && m_Hammers.Count == 0)
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

            if (m_Hammers.Count > 0)
            {
                // Set Hammer to Target
                m_CurrentHammer = GetClosestHammer(m_Hammers);
                // Set Tool in Range true
                m_HammerInRange = true;
            }
            if (m_Bows.Count > 0)
            {
                // Set Bow to Target
                m_CurrentBow = GetClosestBow(m_Bows);
                m_BowInRange = true;
            }
        }

        if (m_BowInRange == true && m_HammerInRange == true || 
            m_Bows.Count > 0 && m_Hammers.Count > 0)
        {
            m_Target = GetClosestTool(m_Bows, m_Hammers);
        }
        else if (m_BowInRange == true && m_HammerInRange == false ||
                 m_Bows.Count > 0 && m_Hammers.Count == 0)
        {
            m_Target = GetClosestBow(m_Bows);
        }
        else if (m_HammerInRange == true && m_BowInRange == false ||
                 m_Hammers.Count > 0 && m_Bows.Count == 0)
        {
            m_Target = GetClosestHammer(m_Hammers);
        }

    }
    #endregion

    #region private Functions
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

    private GameObject GetClosestTool(List<GameObject> _Bow, List<GameObject> _Hammer)
    {
        GameObject TargetB = null;
        GameObject TargetH = null;
        GameObject Target = null;
        Vector2 CurrentPos = transform.position;
        float MinDist = Mathf.Infinity;
        float DistB = 0.0f;
        float DistH = 0.0f;
        float Dist = 0.0f;

        for (int i = 0; i < _Bow.Count; i++)
        {
            if (_Bow[i] == null)
                continue;

            Dist = Vector2.Distance(_Bow[i].transform.position, this.transform.position);

            if(Dist < MinDist)
            {
                TargetB = _Bow[i];
                MinDist = Dist;
                DistB = Dist;
            }
        }

        MinDist = Mathf.Infinity;
        Dist = 0.0f;

        for (int i = 0; i < _Hammer.Count; i++)
        {
            if (_Hammer[i] == null)
                continue;

            Dist = Vector2.Distance(_Hammer[i].transform.position, this.transform.position);

            if(Dist < MinDist)
            {
                TargetH = _Hammer[i];
                MinDist = Dist;
                DistH = Dist;
            }
        }

        if (DistB < DistH)
        {
            Target = TargetB;
        }
        else if (DistH < DistB)
        {
            Target = TargetB;
        }
        else if (DistB == DistH)
        {
            Target = TargetB;
        }

        return Target;
    }
    #endregion

    #region public functions
    public void AddToBowList(GameObject _Bow)
    {
        m_Bows.Add(_Bow);
    }

    public void RemoveFromBowList(GameObject _Bow)
    {
        if (m_Bows.Count > 0)
        {
            for (int i = 0; i < m_Bows.Count; i++)
            {
                if (_Bow == m_Bows[i])
                    m_Bows.Remove(_Bow);
            }
        }
    }

    public void AddToHammerList(GameObject _Hammer)
    {
        m_Hammers.Add(_Hammer);
    }

    public void RemoveFromHammerList(GameObject _Hammer)
    {
        if(m_Hammers.Count > 0)
        {
            for (int i = 0; i < m_Hammers.Count; i++)
            {
                if (_Hammer == m_Hammers[i])
                    m_Hammers.Remove(_Hammer);
            }
        }
    }
    #endregion
}
