using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSpawner : MonoBehaviour
{
    #region Serializefield
    [Header("Spawner Settings")]
    [SerializeField]
    private int m_RabbitsToSpawn = 2;
    [SerializeField]
    private float m_SpawnInterval = 15.0f;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject m_RabbitPrefab;
    #endregion

    #region private Variables
    private float m_Timer = 0.0f;
    private bool m_SpawnedRabbits = false;
    private bool m_CanSpawn = true;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_RabbitsToSpawn; i++)
        {
            Instantiate(m_RabbitPrefab, this.transform.position, Quaternion.identity);
        }

        m_SpawnedRabbits = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsDay)
        {
            if (!m_CanSpawn)
                return;

            if (!m_SpawnedRabbits)
            {
                if(GameManager.Instance.TotalRabbitsSpawned >= GameManager.Instance.TotalRabbits)
                {
                    m_CanSpawn = false;
                }

                for (int i = 0; i < m_RabbitsToSpawn; i++)
                {
                    Instantiate(m_RabbitPrefab, this.transform.position, Quaternion.identity);
                }

                GameManager.Instance.TotalRabbitsSpawned += m_RabbitsToSpawn;
                m_SpawnedRabbits = true;
            }

            if(m_Timer >= m_SpawnInterval)
            {
                m_SpawnedRabbits = false;
            }
            m_Timer += Time.deltaTime;
        }
    }
}
