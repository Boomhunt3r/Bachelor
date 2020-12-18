using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    #region private Variables
    private bool m_Cleared = false;
    private List<GameObject> m_AllEnemies = new List<GameObject>();
    #endregion

    private void Awake()
    {
        Instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsAlive)
            return;

        if(GameManager.Instance.IsDay)
        {
            if(!m_Cleared)
            {
                m_AllEnemies.Clear();
                m_Cleared = true;
            }
        }

        if(GameManager.Instance.IsNight)
        {
            m_Cleared = true;
        }
    }

    public void ClearList()
    {
        m_AllEnemies.Clear();
    }

    public void AddToList(GameObject _Enemy)
    {
        if (_Enemy == null)
            return;

        m_AllEnemies.Add(_Enemy);
    }

    public void RemoveFromList(GameObject _Enemy)
    {
        if (_Enemy == null)
            return;

        if(m_AllEnemies.Count > 0)
        {
            for (int i = 0; i < m_AllEnemies.Count; i++)
            {
                if (_Enemy == m_AllEnemies[i])
                    m_AllEnemies.Remove(_Enemy);
            }
        }
    }

    public void RemoveVillagerFromList(GameObject _Villager)
    {
        if (m_AllEnemies.Count > 0)
        {
            for (int i = 0; i < m_AllEnemies.Count; i++)
            {
                m_AllEnemies[i].GetComponent<Enemy>().RemoveVillagerFromList(_Villager);
            }
        }
    }

    public void RemoveWallFromList(GameObject _Wall)
    {
        if (m_AllEnemies.Count > 0)
        {
            for (int i = 0; i < m_AllEnemies.Count; i++)
            {
                m_AllEnemies[i].GetComponent<Enemy>().RemoveWallFromList(_Wall);
            }
        }
    }
}
