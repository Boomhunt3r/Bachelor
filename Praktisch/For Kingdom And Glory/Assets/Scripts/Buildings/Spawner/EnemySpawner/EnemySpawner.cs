using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Serializefield
    [SerializeField]
    private int m_Wave = 0;
    [SerializeField]
    private int m_MaxHealth = 25;
    [SerializeField]
    private GameObject m_EnemyPrefab;
    [SerializeField]
    private Transform m_Sprite;
    [SerializeField]
    private ParticleSystem m_System;
    #endregion

    #region private Variables
    private List<GameObject> m_SpawnedEnemys = new List<GameObject>();
    private GameObject m_Player;
    private ESpawnerSide m_Side;
    private int m_EnemyToSpawn;
    private int m_CurrentHealth;
    private int m_SpawnedDefender;
    private float m_SpawnTimer;
    private float m_DamageTimer;
    private bool m_Spawned = false;
    private bool m_UnderAttack = false;
    private bool m_Defending = false;
    private bool m_FirstSpawner = false;
    #endregion

    #region private const
    private const int m_NormalAttack = 2;
    private const int m_RevengeAttack = 5;
    #endregion

    #region Properties
    public List<GameObject> SpawnedEnemys { get => m_SpawnedEnemys; set => m_SpawnedEnemys = value; }
    public bool FirstSpawner { get => m_FirstSpawner; set => m_FirstSpawner = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
        m_SpawnTimer = 0.0f;
        m_DamageTimer = 0.0f;
        m_SpawnedDefender = 0;
        m_EnemyToSpawn = 0;
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_System.Pause();

        if (m_Side == ESpawnerSide.RIGHT)
            m_Sprite.localScale = new Vector3(-1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsAlive)
            return;

        if (GameManager.Instance.IsPaused)
            return;

        if (FirstSpawner == false)
            return;

        if (GameManager.Instance.IsDay && m_Spawned)
        {
            m_Spawned = false;
        }

        if (Vector2.Distance(this.gameObject.transform.position, m_Player.transform.position) <= 10.0f)
        {
            if (m_System.isStopped || m_System.isPaused)
                m_System.Play();
        }

        if (Vector2.Distance(this.gameObject.transform.position, m_Player.transform.position) > 10.0f)
        {
            if (m_System.isPlaying)
                m_System.Stop();
        }

        //if (m_UnderAttack)
        //{
        //    if (m_DamageTimer >= 5.0f)
        //    {
        //        m_UnderAttack = false;

        //        for (int i = 0; i < SpawnedEnemys.Count; i++)
        //        {
        //            Destroy(SpawnedEnemys[i]);
        //        }
        //    }

        //    m_DamageTimer += Time.deltaTime;
        //}

        if (GameManager.Instance.IsNight && !GameManager.Instance.RevengeAttack && !m_Spawned && !m_UnderAttack)
        {
            Spawn(EWaveType.NORMAL);
            m_Spawned = true;
        }
        else if (GameManager.Instance.IsNight && !GameManager.Instance.RevengeAttack && m_Spawned && m_UnderAttack)
        {
            Heal();
            m_UnderAttack = false;
        }
        else if (GameManager.Instance.IsDay && !GameManager.Instance.RevengeAttack && m_UnderAttack)
        {
            if (!m_Defending)
                m_Defending = true;

            if (m_Defending)
            {
                m_SpawnTimer += Time.deltaTime;

                if (m_SpawnTimer >= 1.5f)
                {
                    Spawn(EWaveType.DEFENDING);
                    m_SpawnTimer = 0.0f;
                }
                if (m_SpawnedDefender > 5)
                {
                    m_Defending = false;
                    m_SpawnTimer = 0.0f;
                }
            }
        }
    }

    #region private Functions
    private void Spawn(EWaveType _Type)
    {
        switch (_Type)
        {
            case EWaveType.NORMAL:
                m_Wave++;

                m_EnemyToSpawn = m_Wave * m_NormalAttack;

                GameObject Enemy;

                for (int i = 0; i < m_EnemyToSpawn; i++)
                {
                    Enemy = Instantiate(m_EnemyPrefab, this.gameObject.transform.position, Quaternion.identity);
                    Enemy.GetComponent<Enemy>().Side = m_Side;
                    SpawnedEnemys.Add(Enemy);
                    GameManager.Instance.AllSpawnedEnemys.Add(Enemy);
                }
                break;
            case EWaveType.DEFENDING:
                for (int i = 0; i < 1; i++)
                {
                    Enemy = Instantiate(m_EnemyPrefab, this.gameObject.transform.position, Quaternion.identity);
                    Enemy.GetComponent<Enemy>().Side = m_Side;
                    SpawnedEnemys.Add(Enemy);
                    GameManager.Instance.AllSpawnedEnemys.Add(Enemy);
                }
                break;
            case EWaveType.REVENGE:
                m_EnemyToSpawn = m_Wave * m_RevengeAttack;

                for (int i = 0; i < m_EnemyToSpawn; i++)
                {
                    Enemy = Instantiate(m_EnemyPrefab, this.gameObject.transform.position, Quaternion.identity);
                    Enemy.GetComponent<Enemy>().Side = m_Side;
                    SpawnedEnemys.Add(Enemy);
                    GameManager.Instance.AllSpawnedEnemys.Add(Enemy);
                }
                break;
            default:
                break;
        }

    }

    private void Heal()
    {
        m_CurrentHealth = m_MaxHealth;
    }

    private void DestroySpawner()
    {
        GameManager.Instance.RemoveSpawnerFromList(m_Side, this.gameObject);

        ArcherManager.Instance.RemoveSpawner(m_Side, this.gameObject);

        Destroy(this.gameObject);
    }
    #endregion

    #region Public Functions
    public void ReceiveDamage(int _Amount)
    {
        m_CurrentHealth -= _Amount;

        m_DamageTimer = 0.0f;

        if (m_CurrentHealth <= 0)
        {
            DestroySpawner();
        }
    }

    public void RemoveEnemyFromList(GameManager _Enemy)
    {
        for (int i = 0; i < SpawnedEnemys.Count; i++)
        {
            if (_Enemy == SpawnedEnemys[i])
            {
                SpawnedEnemys.RemoveAt(i);
            }
        }
    }

    public void GetSpawnerSide(ESpawnerSide _Side)
    {
        m_Side = _Side;
    }
    #endregion
}
