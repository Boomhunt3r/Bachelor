using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    #region private Variables
    private bool m_Searched = false;
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
    }

    public void ClearList()
    {
        m_AllEnemies.Clear();
    }

    public void AddToList(GameObject _Enemy)
    {
        m_AllEnemies.Add(_Enemy);
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
        if(m_AllEnemies.Count >0)
        {
            for (int i = 0; i < m_AllEnemies.Count; i++)
            {
                m_AllEnemies[i].GetComponent<Enemy>().RemoveWallFromList(_Wall);
            }
        }
    }
}
