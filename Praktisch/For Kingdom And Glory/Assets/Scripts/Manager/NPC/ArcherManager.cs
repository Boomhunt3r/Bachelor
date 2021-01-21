using System.Collections.Generic;
using UnityEngine;

public class ArcherManager : MonoBehaviour
{
    public static ArcherManager Instance { get; private set; }

    #region SerializeField
    [SerializeField]
    private GameObject m_LeftDef;
    [SerializeField]
    private GameObject m_RightDef;
    #endregion

    #region private List
    private List<GameObject> m_ArcherLeftSide = new List<GameObject>();
    private List<GameObject> m_ArcherRightSide = new List<GameObject>();
    #endregion

    #region private Variables
    private int m_Amount = 1;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsAlive)
            return;

        if (GameManager.Instance.IsPaused)
            return;

        if (m_ArcherLeftSide.Count != 0 || m_ArcherLeftSide != null)
        {
            for (int i = 0; i < m_ArcherLeftSide.Count; i++)
            {
                if (WallManager.Instance.GetLeftWall() != null)
                    m_ArcherLeftSide[i].GetComponent<VagrantBehaviour>().DefendingWall = WallManager.Instance.GetLeftWall();
                else if (WallManager.Instance.GetLeftWall() == null)
                    m_ArcherLeftSide[i].GetComponent<VagrantBehaviour>().DefendingWall = m_LeftDef;
            }
        }

        if (m_ArcherRightSide.Count != 0 || m_ArcherRightSide != null)
        {
            for (int i = 0; i < m_ArcherRightSide.Count; i++)
            {
                if (WallManager.Instance.GetRightWall() != null)
                    m_ArcherRightSide[i].GetComponent<VagrantBehaviour>().DefendingWall = WallManager.Instance.GetRightWall();
                else if(WallManager.Instance.GetRightWall() == null)
                    m_ArcherRightSide[i].GetComponent<VagrantBehaviour>().DefendingWall = m_RightDef;
            }
        }
    }

    #region public Function
    /// <summary>
    /// Add Archer to ArcherList
    /// </summary>
    /// <param name="_Archer">Archer to add</param>
    public void AddToList(GameObject _Archer)
    {
        m_Amount++;
        if (m_Amount % 2 == 0)
        {
            m_ArcherRightSide.Add(_Archer);
            Hunting(m_ArcherRightSide);
        }
        else if (m_Amount % 2 != 0)
        {
            m_ArcherLeftSide.Add(_Archer);
            Hunting(m_ArcherLeftSide);
        }
    }

    /// <summary>
    /// Remove Archer from ArcherList
    /// </summary>
    /// <param name="_Archer">Archer to remove</param>
    public void RemoveFromList(GameObject _Archer)
    {
        if (_Archer == null)
            return;

        if (m_ArcherRightSide.Count > 0)
        {
            for (int i = 0; i < m_ArcherRightSide.Count; i++)
            {
                if (_Archer == m_ArcherRightSide[i])
                {
                    m_ArcherRightSide.Remove(_Archer);
                    m_Amount--;
                    return;
                }
            }
        }

        if (m_ArcherLeftSide.Count > 0)
        {
            for (int i = 0; i < m_ArcherLeftSide.Count; i++)
            {
                if (_Archer == m_ArcherLeftSide[i])
                {
                    m_ArcherLeftSide.Remove(_Archer);
                    m_Amount--;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Remove Spawner from SpawnerList
    /// </summary>
    /// <param name="_Side">Spawner side</param>
    /// <param name="_Spawner">Spawner</param>
    public void RemoveSpawner(ESpawnerSide _Side, GameObject _Spawner)
    {
        if (_Side == ESpawnerSide.LEFT)
        {
            for (int i = 0; i < m_ArcherLeftSide.Count; i++)
            {
                m_ArcherLeftSide[i].GetComponent<VagrantBehaviour>().RemoveSpawner(_Spawner);
                m_ArcherLeftSide[i].GetComponent<VagrantBehaviour>().IsAttacking = false;
            }
        }
        if (_Side == ESpawnerSide.RIGHT)
        {
            for (int i = 0; i < m_ArcherRightSide.Count; i++)
            {
                m_ArcherRightSide[i].GetComponent<VagrantBehaviour>().RemoveSpawner(_Spawner);
                m_ArcherRightSide[i].GetComponent<VagrantBehaviour>().IsAttacking = false;
            }
        }
    }

    /// <summary>
    /// Attack function
    /// </summary>
    /// <param name="_Side">Side being attacked</param>
    public void Attack(ESpawnerSide _Side)
    {
        if (_Side == ESpawnerSide.LEFT)
        {
            if (GameManager.Instance.EnemySpawnerLeftSide.Count > 0)
            {
                if (m_ArcherLeftSide.Count > 0)
                {
                    for (int i = 0; i < m_ArcherLeftSide.Count; i++)
                    {
                        m_ArcherLeftSide[i].GetComponent<VagrantBehaviour>().EnemySpawner = GameManager.Instance.EnemySpawnerLeftSide;
                        m_ArcherLeftSide[i].GetComponent<VagrantBehaviour>().IsAttacking = true;
                        m_ArcherLeftSide[i].GetComponent<VagrantBehaviour>().Attack();
                    }
                }
            }
        }
        if (_Side == ESpawnerSide.RIGHT)
        {
            if (GameManager.Instance.EnemySpawnerRightSide.Count > 0)
            {
                if (m_ArcherRightSide.Count > 0)
                {
                    for (int i = 0; i < m_ArcherRightSide.Count; i++)
                    {
                        m_ArcherRightSide[i].GetComponent<VagrantBehaviour>().EnemySpawner = GameManager.Instance.EnemySpawnerRightSide;
                        m_ArcherRightSide[i].GetComponent<VagrantBehaviour>().IsAttacking = true;
                        m_ArcherRightSide[i].GetComponent<VagrantBehaviour>().Attack();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Hunting Function
    /// </summary>
    /// <param name="_Archers">List to go through</param>
    private void Hunting(List<GameObject> _Archers)
    {
        int Amount = 1;

        for (int i = 0; i < _Archers.Count; i++)
        {
            // if modulo is uneven
            if (Amount % 2 != 0)
                _Archers[i].GetComponent<VagrantBehaviour>().Hunter = true; // Archer is an Hunter
            if (Amount % 2 == 0)
                _Archers[i].GetComponent<VagrantBehaviour>().Hunter = false;

            // increase amount
            Amount++;
        }
    }

    /// <summary>
    /// Stop Hunting function for archer
    /// </summary>
    /// <param name="_Archer">Archer to stop</param>
    /// <param name="_Rabbit">Rabbit to remove</param>
    public void HuntingStop(GameObject _Archer, GameObject _Rabbit)
    {
        if (_Archer == null)
            return;

        _Archer.GetComponent<VagrantBehaviour>().RemoveRabbit(_Rabbit);
    }

    /// <summary>
    /// Add Enemies to Archer function
    /// </summary>
    /// <param name="_Enemy">Enemy to add</param>
    /// <param name="_Side">Side to add to</param>
    public void AddEnemies(GameObject _Enemy, ESpawnerSide _Side)
    {
        if (_Enemy == null)
            return;

        if (_Side == ESpawnerSide.LEFT)
        {
            if (m_ArcherLeftSide.Count > 0)
            {
                for (int i = 0; i < m_ArcherLeftSide.Count; i++)
                {
                    m_ArcherLeftSide[i].GetComponent<VagrantBehaviour>().AddEnemy(_Enemy);
                }
            }
        }
        if (_Side == ESpawnerSide.RIGHT)
        {
            if (m_ArcherRightSide.Count > 0)
            {
                for (int i = 0; i < m_ArcherRightSide.Count; i++)
                {
                    m_ArcherRightSide[i].GetComponent<VagrantBehaviour>().AddEnemy(_Enemy);
                }
            }
        }
    }

    /// <summary>
    /// Remove Enemies from Archer
    /// </summary>
    /// <param name="_Enemy">Enemy to remove</param>
    /// <param name="_Side">Side to remove from</param>
    public void RemoveEnemies(GameObject _Enemy, ESpawnerSide _Side)
    {
        if (_Enemy == null)
            return;

        if (_Side == ESpawnerSide.LEFT)
        {
            if (m_ArcherLeftSide.Count > 0)
            {
                for (int i = 0; i < m_ArcherLeftSide.Count; i++)
                {
                    m_ArcherLeftSide[i].GetComponent<VagrantBehaviour>().RemoveEnemy(_Enemy);
                }
            }
        }
        if (_Side == ESpawnerSide.RIGHT)
        {
            if (m_ArcherRightSide.Count > 0)
            {
                for (int i = 0; i < m_ArcherRightSide.Count; i++)
                {
                    m_ArcherRightSide[i].GetComponent<VagrantBehaviour>().RemoveEnemy(_Enemy);
                }
            }
        }
    }
    #endregion
}
