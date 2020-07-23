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

        //InvokeRepeating("UpdatePath", 0.0f, 0.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_Path == null)
            return;
    }

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
