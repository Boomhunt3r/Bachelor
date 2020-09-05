using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private GameObject[] m_LeftCollider;
    [SerializeField]
    private GameObject[] m_RightCollider;

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
    [SerializeField]
    private GameObject m_PauseMenu;
    [SerializeField]
    private GameObject m_SettingMenu;
    [SerializeField]
    private AudioSource m_Source;
    #endregion

    #region private Variables
    private float m_Timer = 0.0f;
    private float m_UITimer = 0.0f;
    private int m_TotalRabbitsSpawned = 0;
    private int m_DayCount = 0;
    private bool m_IsDay = true;
    private bool m_IsNight = false;
    private bool m_AlmostNight = false;
    private bool m_IsAlive = true;
    private bool m_RevengeAttack = false;
    private bool m_IsPaused = false;
    private EGameSetting m_Setting;
    private List<GameObject> m_EnemySpawnerLeftSide = new List<GameObject>();
    private List<GameObject> m_EnemySpawnerRightSide = new List<GameObject>();
    private List<GameObject> m_AllSpawnedEnemys = new List<GameObject>();
    private List<GameObject> m_SpawnedVagrants = new List<GameObject>();
    #endregion

    #region Properties
    public bool IsNight { get => m_IsNight; set => m_IsNight = value; }
    public bool IsDay { get => m_IsDay; set => m_IsDay = value; }
    public bool IsAlive { get => m_IsAlive; set => m_IsAlive = value; }
    public int TotalRabbitsSpawned { get => m_TotalRabbitsSpawned; set => m_TotalRabbitsSpawned = value; }
    public int TotalRabbits { get => m_TotalRabbits; set => m_TotalRabbits = value; }
    public bool RevengeAttack { get => m_RevengeAttack; set => m_RevengeAttack = value; }
    public EGameSetting Setting { get => m_Setting; set => m_Setting = value; }
    public List<GameObject> AllSpawnedEnemys { get => m_AllSpawnedEnemys; set => m_AllSpawnedEnemys = value; }
    public List<GameObject> SpawnedVagrants { get => m_SpawnedVagrants; set => m_SpawnedVagrants = value; }
    public bool AlmostNight { get => m_AlmostNight; set => m_AlmostNight = value; }
    public List<GameObject> EnemySpawnerLeftSide { get => m_EnemySpawnerLeftSide; set => m_EnemySpawnerLeftSide = value; }
    public List<GameObject> EnemySpawnerRightSide { get => m_EnemySpawnerRightSide; set => m_EnemySpawnerRightSide = value; }
    public bool IsPaused { get => m_IsPaused; set => m_IsPaused = value; }
    #endregion

    private void Awake()
    {
        m_Setting = (EGameSetting)PlayerPrefs.GetInt("Setting");

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_PauseMenu.SetActive(false);
        //m_SettingMenu.SetActive(false);

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsAlive)
        {
            return;
        }

        m_PauseMenu.SetActive(IsPaused);

        if (IsPaused)
        {
            return;
        }

        m_Timer += Time.deltaTime;

        if (IsDay)
        {
            if (m_AllSpawnedEnemys.Count != 0)
            {
                for (int i = 0; i < m_AllSpawnedEnemys.Count; i++)
                {
                    Destroy(m_AllSpawnedEnemys[i]);
                }
            }

            if (m_Timer >= m_DayLength - 20)
            {
                m_AlmostNight = true;
            }

            if (m_Timer >= m_DayLength)
            {
                IsDay = false;
                m_AlmostNight = false;
                IsNight = true;
                m_Timer = 0.0f;
            }
        }
        else if (IsNight)
        {
            if (m_Timer >= m_NightLength)
            {
                m_AlmostNight = false;
                IsNight = false;
                IsDay = true;
                m_Timer = 0.0f;
            }
        }
    }

    #region private Function
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
                        Spawner.SetActive(true);

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
                    Spawner.SetActive(true);

                    if (m_EnemySpawnerRightSide.Count == 0)
                        Spawner.GetComponent<EnemySpawner>().FirstSpawner = true;

                    m_EnemySpawnerRightSide.Add(Spawner);
                }
                m_LeftCollider[1].SetActive(false);
                m_LeftCollider[2].SetActive(false);
                m_RightCollider[1].SetActive(false);
                m_RightCollider[2].SetActive(false);

                Inventory.Instance.Coins = 12;
                Inventory.Instance.Wood = 8;
                Inventory.Instance.Stone = 4;
                Inventory.Instance.Iron = 2;
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
                        Spawner.SetActive(true);

                        if (m_EnemySpawnerLeftSide.Count == 0)
                            Spawner.GetComponent<EnemySpawner>().FirstSpawner = true;

                        m_EnemySpawnerLeftSide.Add(Spawner);
                        continue;
                    }

                    Spawner = Instantiate(m_EnemySpawnerPrefab, m_EnemySpawnerPos[i].position, Quaternion.identity);
                    Spawner.GetComponent<EnemySpawner>().GetSpawnerSide(ESpawnerSide.RIGHT);
                    Spawner.SetActive(true);

                    if (m_EnemySpawnerRightSide.Count == 0)
                        Spawner.GetComponent<EnemySpawner>().FirstSpawner = true;

                    m_EnemySpawnerRightSide.Add(Spawner);
                    m_LeftCollider[0].SetActive(false);
                    m_LeftCollider[2].SetActive(false);
                    m_RightCollider[0].SetActive(false);
                    m_RightCollider[2].SetActive(false);

                    Inventory.Instance.Coins = 12;
                    Inventory.Instance.Wood = 4;
                    Inventory.Instance.Stone = 2;
                    Inventory.Instance.Iron = 0;

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
                        Spawner.SetActive(true);

                        if (m_EnemySpawnerLeftSide.Count == 0)
                            Spawner.GetComponent<EnemySpawner>().FirstSpawner = true;

                        m_EnemySpawnerLeftSide.Add(Spawner);
                        continue;
                    }

                    Spawner = Instantiate(m_EnemySpawnerPrefab, m_EnemySpawnerPos[i].position, Quaternion.identity);
                    Spawner.GetComponent<EnemySpawner>().GetSpawnerSide(ESpawnerSide.RIGHT);
                    Spawner.SetActive(true);

                    if (m_EnemySpawnerRightSide.Count == 0)
                        Spawner.GetComponent<EnemySpawner>().FirstSpawner = true;

                    m_EnemySpawnerRightSide.Add(Spawner);
                }
                m_LeftCollider[0].SetActive(false);
                m_LeftCollider[1].SetActive(false);
                m_RightCollider[0].SetActive(false);
                m_RightCollider[1].SetActive(false);

                Inventory.Instance.Coins = 12;
                Inventory.Instance.Wood = 2;
                Inventory.Instance.Stone = 0;
                Inventory.Instance.Iron = 0;
                break;
            default:
                Debug.LogWarning("Nothing");
                break;
        }


    }

    #endregion

    #region public Function
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

    public void RemoveEnemyFromList(GameObject _Enemy)
    {
        for (int i = 0; i < AllSpawnedEnemys.Count; i++)
        {
            if (_Enemy == AllSpawnedEnemys[i])
                AllSpawnedEnemys.Remove(_Enemy);
        }
    }
    #endregion

    #region UI Function
    public void ExitPauseMenu()
    {
        IsPaused = false;
        m_Source.Play();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        m_Source.Play();
    }

    public void Settings()
    {
        m_PauseMenu.SetActive(false);
        m_SettingMenu.SetActive(true);
        m_Source.Play();
    }

    public void ExitGame()
    {
        m_Source.Play();
        Application.Quit();
    }
    #endregion
}
