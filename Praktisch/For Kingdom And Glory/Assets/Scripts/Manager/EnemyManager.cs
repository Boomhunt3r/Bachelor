using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    private bool m_Searched = false;
    private List<GameObject> m_AllEnemies = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsAlive)
            return;

        if (GameManager.Instance.IsDay)
        {
            m_Searched = false;
        }

        if (GameManager.Instance.IsNight)
        {
            m_AllEnemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();

            m_Searched = true;
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
}
