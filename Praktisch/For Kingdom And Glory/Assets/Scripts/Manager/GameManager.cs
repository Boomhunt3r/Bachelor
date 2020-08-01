using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    #region Serializefield
    [Header("Game Settings")]
    [SerializeField]
    private float m_DayLength = 120.0f;
    [SerializeField]
    private float m_NightLength = 120.0f;
    [SerializeField]
    private int m_EnemySpawner = 2;
    [SerializeField]
    private int m_TotalRabbits = 150;
    [SerializeField]
    private Transform[] m_EnemySpawnerPos;
    [SerializeField]
    private Transform[] m_VagrantSpawnerPos;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject m_EnemySpawnerPrefab;
    [SerializeField]
    private GameObject m_RabbitSpawnerPrefab;

    [Header("UI")]
    [SerializeField]
    private GameObject m_DayUI;
    [SerializeField]
    private TextMeshProUGUI m_DayAnnouncer;
    #endregion

    #region private Variables
    private float m_Timer = 0.0f;
    private float m_UITimer = 0.0f;
    private int m_TotalRabbitsSpawned = 0;
    private int m_DayCount = 0;
    private bool m_IsDay = true;
    private bool m_IsNight = false;
    private bool m_IsAlive = true;
    private bool m_RevengeAttack = false;
    private EGameSetting m_Setting;
    private List<GameObject> m_EnemySpawnerLeftSide = new List<GameObject>();
    private List<GameObject> m_EnemySpawnerRightSide = new List<GameObject>();
    #endregion

    #region Properties
    public bool IsNight { get => m_IsNight; set => m_IsNight = value; }
    public bool IsDay { get => m_IsDay; set => m_IsDay = value; }
    public bool IsAlive { get => m_IsAlive; set => m_IsAlive = value; }
    public int TotalRabbitsSpawned { get => m_TotalRabbitsSpawned; set => m_TotalRabbitsSpawned = value; }
    public int TotalRabbits { get => m_TotalRabbits; set => m_TotalRabbits = value; }
    public bool RevengeAttack { get => m_RevengeAttack; set => m_RevengeAttack = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        m_Setting = (EGameSetting)PlayerPrefs.GetInt("Setting");

        Debug.Log(m_Setting);

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsAlive)
        {
            return;
        }

        m_Timer += Time.deltaTime;

        if (RevengeAttack)
        {
            IsDay = false;
            IsNight = true;
        }

        if (IsDay)
        {
            if (EnemySpawner.Instance.SpawnedEnemys.Count != 0)
            {
                if (EnemySpawner.Instance.SpawnedEnemys.Count > 0)
                {
                    for (int i = 0; i < EnemySpawner.Instance.SpawnedEnemys.Count; i++)
                    {
                        Destroy(EnemySpawner.Instance.SpawnedEnemys[i]);
                    }
                }
            }

            if (m_Timer >= m_DayLength)
            {
                IsDay = false;
                IsNight = true;
                m_Timer = 0.0f;
            }
        }
        else if (IsNight)
        {
            if (m_Timer >= m_NightLength)
            {
                IsNight = false;
                IsDay = true;
                m_DayCount++;
                UpdateDayCounter();
            }
        }
    }

    private void UpdateDayCounter()
    {
        m_DayAnnouncer.text = $"Day: {m_DayCount}";
    }

    private void StartGame()
    {
        GameObject Spawner;

        switch (m_Setting)
        {
            case EGameSetting.EASY:
                // Set Spawner and Rabbits similiar to Setting
                m_EnemySpawner = 2;
                m_TotalRabbits = 300;

                // Spawn Spawner
                for (int i = 0; i < m_EnemySpawner; i++)
                {
                    // Left Side
                    if (i % 2 != 0)
                    {
                        // Spawn
                        Spawner = Instantiate(m_EnemySpawnerPrefab, m_EnemySpawnerPos[i].position, Quaternion.identity);
                        // Set side to left
                        Spawner.GetComponent<EnemySpawner>().GetSpawnerSide(ESpawnerSide.LEFT);

                        // If List is empty
                        if (m_EnemySpawnerLeftSide.Count == 0)
                            // Spawner is First Spawner
                            Spawner.GetComponent<EnemySpawner>().FirstSpawner = true;

                        // Add to List
                        m_EnemySpawnerLeftSide.Add(Spawner);
                        continue;
                    }

                    // Right Side
                    Spawner = Instantiate(m_EnemySpawnerPrefab, m_EnemySpawnerPos[i].position, Quaternion.identity);
                    Spawner.GetComponent<EnemySpawner>().GetSpawnerSide(ESpawnerSide.RIGHT);

                    if (m_EnemySpawnerLeftSide.Count == 0)
                        Spawner.GetComponent<EnemySpawner>().FirstSpawner = true;

                    m_EnemySpawnerLeftSide.Add(Spawner);
                }

                break;
            case EGameSetting.MEDUIM:
                m_EnemySpawner = 4;
                m_TotalRabbits = 200;

                for (int i = 0; i < m_EnemySpawner; i++)
                {
                    if (i % 2 != 0)
                    {
                        Spawner = Instantiate(m_EnemySpawnerPrefab, m_EnemySpawnerPos[i].position, Quaternion.identity);
                        Spawner.GetComponent<EnemySpawner>().GetSpawnerSide(ESpawnerSide.LEFT);

                        if (m_EnemySpawnerLeftSide.Count == 0)
                            Spawner.GetComponent<EnemySpawner>().FirstSpawner = true;

                        m_EnemySpawnerLeftSide.Add(Spawner);
                        continue;
                    }

                    Spawner = Instantiate(m_EnemySpawnerPrefab, m_EnemySpawnerPos[i].position, Quaternion.identity);
                    Spawner.GetComponent<EnemySpawner>().GetSpawnerSide(ESpawnerSide.RIGHT);
                    m_EnemySpawnerLeftSide.Add(Spawner);
                }

                break;
            case EGameSetting.HARD:
                m_EnemySpawner = 6;
                m_TotalRabbits = 150;

                for (int i = 0; i < m_EnemySpawner; i++)
                {
                    if (i % 2 != 0)
                    {
                        Spawner = Instantiate(m_EnemySpawnerPrefab, m_EnemySpawnerPos[i].position, Quaternion.identity);
                        Spawner.GetComponent<EnemySpawner>().GetSpawnerSide(ESpawnerSide.LEFT);

                        if (m_EnemySpawnerLeftSide.Count == 0)
                            Spawner.GetComponent<EnemySpawner>().FirstSpawner = true;

                        m_EnemySpawnerLeftSide.Add(Spawner);
                        continue;
                    }

                    Spawner = Instantiate(m_EnemySpawnerPrefab, m_EnemySpawnerPos[i].position, Quaternion.identity);
                    Spawner.GetComponent<EnemySpawner>().GetSpawnerSide(ESpawnerSide.RIGHT);
                    m_EnemySpawnerLeftSide.Add(Spawner);
                }

                break;
            default:
                Debug.LogWarning("Nothing");
                break;
        }
    }

    /// <summary>
    /// Remove Spawner from List
    /// </summary>
    /// <param name="_Side">Which Side Spawner Was</param>
    /// <param name="_Spawner">The Spawner</param>
    public void RemoveSpawnerFromList(ESpawnerSide _Side, GameObject _Spawner)
    {
        switch (_Side)
        {
            case ESpawnerSide.LEFT:
                // Search Spawner in List
                for (int i = 0; i < m_EnemySpawnerLeftSide.Count; i++)
                {
                    // If Spawner is the same as in List
                    if (_Spawner == m_EnemySpawnerLeftSide[i])
                        // Remove Spawner
                        m_EnemySpawnerLeftSide.Remove(_Spawner);
                }

                // If there are more Spawner
                if (m_EnemySpawnerLeftSide.Count != 0)
                    // Next Spawner is First Spawner
                    m_EnemySpawnerLeftSide[0].GetComponent<EnemySpawner>().FirstSpawner = true;
                break;
            case ESpawnerSide.RIGHT:
                for (int i = 0; i < m_EnemySpawnerRightSide.Count; i++)
                {
                    if (_Spawner == m_EnemySpawnerRightSide[i])
                        m_EnemySpawnerRightSide.Remove(_Spawner);
                }

                if (m_EnemySpawnerRightSide.Count != 0)
                    m_EnemySpawnerRightSide[0].GetComponent<EnemySpawner>().FirstSpawner = true;
                break;
            default:
                break;
        }
    }
}
