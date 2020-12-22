using System.Collections.Generic;
using UnityEngine;

public class ArcherManager : MonoBehaviour
{
    public static ArcherManager Instance { get; private set; }

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
                m_ArcherLeftSide[i].GetComponent<VagrantBehaviour>().DefendingWall = WallManager.Instance.GetLeftWall();
            }
        }

        if (m_ArcherRightSide.Count != 0 || m_ArcherRightSide != null)
        {
            for (int i = 0; i < m_ArcherRightSide.Count; i++)
            {
                m_ArcherRightSide[i].GetComponent<VagrantBehaviour>().DefendingWall = WallManager.Instance.GetRightWall();
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
        if (m_Amount % 2 == 0)
        {
            m_ArcherRightSide.Add(_Archer);
        }
        else
        {
            m_ArcherLeftSide.Add(_Archer);
        }
        m_Amount++;
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

        if(m_ArcherLeftSide.Count > 0)
        {
            for (int i = 0; i < m_ArcherLeftSide.Count; i++)
            {
                if(_Archer == m_ArcherLeftSide[i])
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
                m_ArcherLeftSide[i].GetComponent<VagrantBehaviour>().IsAttacking = false;
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
                    }
                }
            }
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
    /// TBD
    /// </summary>
    public void ChangeSite()
    {

    }
    #endregion
}
