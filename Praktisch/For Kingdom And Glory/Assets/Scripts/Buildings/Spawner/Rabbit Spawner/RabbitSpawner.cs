using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSpawner : MonoBehaviour
{
    public static RabbitSpawner Instance { get; private set; }

    #region Serializefield
    [Header("Spawner Settings")]
    [SerializeField]
    private int m_RabbitsToSpawn = 2;
    [SerializeField]
    private float m_SpawnInterval = 15.0f;
    [SerializeField]
    private int m_MaxSpawnedRabbits = 2;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject m_RabbitPrefab;
    #endregion

    #region private Variables
    private GameObject m_Rabbit;
    private float m_Timer = 0.0f;
    private int m_SpawnedRabbitAmount = 0;
    private bool m_SpawnedRabbits = false;
    private bool m_CanSpawn = true;
    #endregion

    #region Properties
    public int SpawnedRabbitAmount { get => m_SpawnedRabbitAmount; set => m_SpawnedRabbitAmount = value; } 
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_RabbitsToSpawn; i++)
        {
            m_Rabbit = Instantiate(m_RabbitPrefab, this.transform.position, Quaternion.identity);
            m_Rabbit.GetComponent<Rabbit>().Spawner = this.gameObject;
        }

        m_SpawnedRabbitAmount += m_RabbitsToSpawn;

        m_SpawnedRabbits = true;

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsAlive)
            return;

        if (GameManager.Instance.IsDay)
        {
            if (!m_CanSpawn)
                return;

            if (!m_SpawnedRabbits)
            {
                if (SpawnedRabbitAmount >= m_MaxSpawnedRabbits)
                    return;

                if(GameManager.Instance.TotalRabbitsSpawned >= GameManager.Instance.TotalRabbits)
                {
                    m_CanSpawn = false;
                }

                for (int i = 0; i < m_RabbitsToSpawn; i++)
                {
                    m_Rabbit = Instantiate(m_RabbitPrefab, this.transform.position, Quaternion.identity);
                    m_Rabbit.GetComponent<Rabbit>().Spawner = this.gameObject;
                }

                SpawnedRabbitAmount += m_RabbitsToSpawn;
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
