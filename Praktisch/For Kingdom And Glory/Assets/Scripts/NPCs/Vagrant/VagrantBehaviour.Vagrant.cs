﻿using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class VagrantBehaviour : MonoBehaviour
{
    private List<GameObject> m_Coins = new List<GameObject>();
    private bool m_VagrantIdle = true;

    private void Vagrant()
    {
        if (!m_ChangedSkin)
        {
            m_Animation.Skeleton.SetSkin("V3");
            m_ChangedSkin = true;
        }

        #region Coin Behaviour

        if (m_Coins.Count > 0)
        {
            m_VagrantIdle = false;
            m_IsIdle = false;
        }

        if (m_Coins.Count == 0)
            m_VagrantIdle = true;

        if (m_VagrantIdle)
        {
            m_Target = m_Waypoints[m_CurrentDirection];

            m_Distance = Vector2.Distance(m_Rigid.position, m_Target.transform.position);

            if (m_Distance <= 0.1f)
            {
                m_IsIdle = true;
                m_Timer += Time.deltaTime;

                m_Rigid.velocity = new Vector2(0, 0);

                if (m_Timer >= m_IdleTimer)
                {
                    m_CurrentDirection++;

                    if (m_CurrentDirection > 3)
                        m_CurrentDirection = 0;

                    m_IsIdle = false;
                    m_Timer = 0.0f;
                }
            }

        }

        if (!m_VagrantIdle)
        {
            m_Target = GetClosestCoin(m_Coins);
            m_Coin = m_Target;
        }
        #endregion
    }

    private GameObject GetClosestCoin(List<GameObject> _Coins)
    {
        GameObject Target = null;

        float MinDist = Mathf.Infinity;
        Vector2 CurrentPos = transform.position;

        float Dist = 0.0f;

        for (int i = 0; i < _Coins.Count; i++)
        {
            if (_Coins[i] == null)
                continue;

            Dist = Vector2.Distance(_Coins[i].transform.position, CurrentPos);

            if (Dist < MinDist)
            {
                Target = _Coins[i];
                MinDist = Dist;
            }
        }

        return Target;
    }

    public void AddCoin(GameObject _Coin)
    {
        m_Coins.Add(_Coin);
    }

    public void RemoveCoin(GameObject _Coin)
    {
        for (int i = 0; i < m_Coins.Count; i++)
        {
            if(_Coin == m_Coins[i])
            {
                m_Coins.Remove(_Coin);
            }
        }
    }

    public void ChangeStartWaypoint(int _Digit)
    {
        m_CurrentDirection = _Digit;
    }
}
