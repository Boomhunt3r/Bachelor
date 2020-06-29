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
    #endregion

    #region Properties
    public bool IsNight { get => m_IsNight; set => m_IsNight = value; }
    public bool IsDay { get => m_IsDay; set => m_IsDay = value; }
    public bool IsAlive { get => m_IsAlive; set => m_IsAlive = value; }
    public int TotalRabbitsSpawned { get => m_TotalRabbitsSpawned; set => m_TotalRabbitsSpawned = value; }
    public int TotalRabbits { get => m_TotalRabbits; set => m_TotalRabbits = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsAlive)
        {
            return;
        }

        m_Timer += Time.deltaTime;

        if (IsDay)
        {
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
}
