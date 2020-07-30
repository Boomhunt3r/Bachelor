using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 25.0f;
    [SerializeField]
    private float m_NextWaypointDist = 3.0f;
    [SerializeField]
    private Transform m_Sprite;

    private Rigidbody2D m_Rigid;
    private GameObject m_Target;
    private GameObject m_ClosestWall;
    private GameObject m_ClosestVilliger;
    private GameObject[] m_Walls;
    private GameObject[] m_Villager;

    private GridGraph m_Grid;
    private Path m_Path;
    private Seeker m_Seeker;
    private int m_CurrentWaypoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        //m_Seeker.GetComponent<Seeker>();
        m_Rigid = GetComponent<Rigidbody2D>();

        m_Walls = GameObject.FindGameObjectsWithTag("Wall");
        m_Villager = GameObject.FindGameObjectsWithTag("Villager");

        if (m_Walls.Length != 0)
            m_ClosestWall = GetClosestWall(m_Walls);

        if (m_Villager.Length != 0)
            m_ClosestVilliger = GetClosestVillager(m_Villager);

        if (m_Villager.Length != 0 && m_Walls.Length != 0)
            m_Target = GetClosestOverall(m_ClosestWall, m_ClosestVilliger);

        else if (m_Villager.Length != 0 && m_Walls.Length == 0)
            m_Target = m_ClosestVilliger;

        else if (m_Villager.Length == 0 && m_Walls.Length != 0)
            m_Target = m_ClosestWall;

        //InvokeRepeating("UpdatePath", 0.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Walls.Length != 0)
            m_ClosestWall = GetClosestWall(m_Walls);

        if (m_Villager.Length != 0)
            m_ClosestVilliger = GetClosestVillager(m_Villager);

        if (m_Villager.Length != 0 && m_Walls.Length != 0)
            m_Target = GetClosestOverall(m_ClosestWall, m_ClosestVilliger);

        else if (m_Villager.Length != 0 && m_Walls.Length == 0)
            m_Target = m_ClosestVilliger;

        else if (m_Villager.Length == 0 && m_Walls.Length != 0)
            m_Target = m_ClosestWall;

        Debug.Log(m_ClosestWall.transform.position);

        Vector2 Direction = ((Vector2)m_Target.transform.position - m_Rigid.position).normalized;
        m_Rigid.velocity = Direction * m_Speed * Time.deltaTime;
        float Distance = Vector2.Distance(m_Rigid.position, m_Target.transform.position);

        if (Distance < 3.5f)
        {
            Debug.Log("true");
            m_Rigid.velocity = new Vector2(0, 0);
        }

        if (m_Rigid.velocity.x == 0)
        {
            Attack();
        }
    }

    #region private Functions
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_Walls">Array with all Walls</param>
    /// <returns>Closest Wall</returns>
    private GameObject GetClosestWall(GameObject[] _Walls)
    {
        GameObject Target = null;
        float MinDist = Mathf.Infinity;
        Vector2 CurrentPos = transform.position;

        float Dist = 0.0f;

        for (int i = 0; i < _Walls.Length; i++)
        {
            Dist = Vector2.Distance(_Walls[i].transform.position, CurrentPos);

            if (Dist < MinDist)
            {
                Target = _Walls[i];
                MinDist = Dist;
            }
        }

        return Target;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_Villager">Array with all Villiger</param>
    /// <returns>Closest Villiger</returns>
    private GameObject GetClosestVillager(GameObject[] _Villager)
    {
        GameObject Target = null;
        float MinDist = Mathf.Infinity;
        Vector2 CurrentPos = transform.position;
        float Dist = 0.0f;

        for (int i = 0; i < _Villager.Length; i++)
        {
            Dist = Vector2.Distance(_Villager[i].transform.position, CurrentPos);

            if (Dist < MinDist)
            {
                Target = _Villager[i];
                MinDist = Dist;
            }
        }

        return Target;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_Wall">Closest Wall</param>
    /// <param name="_Villiger">Closest Villiger</param>
    /// <returns>Wall or Villiger, the nearest</returns>
    private GameObject GetClosestOverall(GameObject _Wall, GameObject _Villiger)
    {
        GameObject Target = null;
        Vector2 CurrentPos = transform.position;
        float DistW = 0.0f;
        float DistV = 0.0f;

        DistW = Vector2.Distance(_Wall.transform.position, CurrentPos);
        DistV = Vector2.Distance(_Villiger.transform.position, CurrentPos);

        if (DistW < DistV)
        {
            Target = _Wall;
        }
        else if (DistV < DistW)
        {
            Target = _Villiger;
        }
        else if (DistW == DistV)
        {
            Target = _Villiger;
        }

        return Target;
    }

    private void Attack()
    {

    }
    #endregion

    #region private Path Functions
    private void UpdatePath()
    {
        if (m_Seeker.IsDone())
            m_Seeker.StartPath(m_Rigid.position, m_Target.transform.position, OnPathComplete);
    }

    private void OnPathComplete(Path _Path)
    {
        if (!_Path.error)
        {
            m_Path = _Path;
            m_CurrentWaypoint = 0;
        }
    }
    #endregion
}
