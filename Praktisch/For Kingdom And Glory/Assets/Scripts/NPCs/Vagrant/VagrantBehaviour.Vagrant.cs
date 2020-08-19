using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class VagrantBehaviour : MonoBehaviour
{
    private List<GameObject> m_Coins = new List<GameObject>();

    private void Vagrant()
    {
        #region Coin Behaviour

        if (m_Coins.Count == 0)
        {
            m_Coins = GameObject.FindGameObjectsWithTag("Coin").OfType<GameObject>().ToList();
            m_Target = m_Waypoints[m_CurrentDirection];

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

        }

        if (m_Coins.Count != 0)
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
            Dist = Vector2.Distance(_Coins[i].transform.position, CurrentPos);

            if (Dist < MinDist)
            {
                Target = _Coins[i];
                MinDist = Dist;
            }
        }

        return Target;
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
}
