using System.Collections.Generic;
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

        // if is Day
        if(GameManager.Instance.IsDay)
        {
            // List was not yet cleared
            if(!m_Cleared)
            {
                // Clear list
                m_AllEnemies.Clear();
                // set true
                m_Cleared = true;
            }
        }

        // if night
        if(GameManager.Instance.IsNight)
        {
            // set cleared false
            m_Cleared = false;
        }
    }

    /// <summary>
    /// Clear List function
    /// </summary>
    public void ClearList()
    {
        m_AllEnemies.Clear();
    }

    /// <summary>
    /// Add to list function
    /// </summary>
    /// <param name="_Enemy">Enemy to add</param>
    public void AddToList(GameObject _Enemy)
    {
        if (_Enemy == null)
            return;

        m_AllEnemies.Add(_Enemy);
    }

    /// <summary>
    /// Remove from list function
    /// </summary>
    /// <param name="_Enemy">Enemy to remove</param>
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

    /// <summary>
    /// Remove Villager for all Enemies function
    /// </summary>
    /// <param name="_Villager">Villager to remove</param>
    public void RemoveVillagerFromList(GameObject _Villager)
    {
        if (_Villager == null)
            return;

        if (m_AllEnemies.Count > 0)
        {
            for (int i = 0; i < m_AllEnemies.Count; i++)
            {
                m_AllEnemies[i].GetComponent<Enemy>().RemoveVillagerFromList(_Villager);
            }
        }
    }

    /// <summary>
    /// Remove Archer for all Enemies function
    /// </summary>
    /// <param name="_Archer">Archer to remove</param>
    public void RemoveFromArcherList(GameObject _Archer)
    {
        if (_Archer == null)
            return;

        if (m_AllEnemies.Count > 0)
        {
            for (int i = 0; i < m_AllEnemies.Count; i++)
            {
                m_AllEnemies[i].GetComponent<Enemy>().RemoveArcherFromList(_Archer);
                // Add to Villager list
                m_AllEnemies[i].GetComponent<Enemy>().AddToVillagerList(_Archer);
            }
        }
    }

    /// <summary>
    /// Remove Builder from all Enemies function
    /// </summary>
    /// <param name="_Builder">Builder to remove</param>
    public void RemoveFromBuilderList(GameObject _Builder)
    {
        if (_Builder == null)
            return;

        if (m_AllEnemies.Count > 0)
        {
            for (int i = 0; i < m_AllEnemies.Count; i++)
            {
                m_AllEnemies[i].GetComponent<Enemy>().RemoveBuilderFromList(_Builder);
                // Add to Villager list
                m_AllEnemies[i].GetComponent<Enemy>().AddToVillagerList(_Builder);
            }
        }
    }

    /// <summary>
    /// Remove Wall for all Enemies function
    /// </summary>
    /// <param name="_Wall">Wall to remove</param>
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
