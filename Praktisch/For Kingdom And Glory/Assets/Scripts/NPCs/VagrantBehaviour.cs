using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public partial class VagrantBehaviour : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    protected float m_Speed = 0.5f;
    [SerializeField]
    private float m_NextWaypointDist = 3.0f;
    [SerializeField]
    private GameObject[] m_Waypoints;
    [SerializeField]
    private Transform m_Sprite;
    #endregion

    #region private Variables

    #region GameObjects
    /// <summary>
    /// Coin
    /// </summary>
    private GameObject m_Coin;
    /// <summary>
    /// Target Waypoint
    /// </summary>
    private GameObject m_Target;
    /// <summary>
    /// Villager Waypoint
    /// </summary>
    private GameObject m_VillagerPoint;
    /// <summary>
    /// Bow
    /// </summary>
    private GameObject m_Bow;
    /// <summary>
    /// Hammer
    /// </summary>
    private GameObject m_Hammer;
    /// <summary>
    /// Sword
    /// </summary>
    private GameObject m_Sword;
    #endregion

    private Rigidbody2D m_Rigid;
    private SpriteRenderer m_Render;
    private List<Vector2> m_Directions = new List<Vector2>();
    private Vector2 m_CurrentVeloc;
    private ENPCStatus m_Status;
    private int m_CurrentDirection;
    private float m_Timer = 0.0f;
    private bool m_isIdle = false;
    #endregion

    #region private Pathfinding Variables
    private Path m_Path;
    private int m_CurrentWaypoint = 0;
    private bool m_EndPathReached = false;

    private Seeker m_Seeker;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
        m_Seeker = GetComponent<Seeker>();
        m_Render = GetComponentInChildren<SpriteRenderer>();
        m_Waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        m_VillagerPoint = GameObject.FindGameObjectWithTag("VillagerPoint");
        m_CurrentDirection = Random.Range(0, m_Waypoints.Length);
        m_Target = m_Waypoints[m_CurrentDirection];

        m_Status = ENPCStatus.VARGANT;

        InvokeRepeating("UpdatePath", 0.0f, 0.5f);
    }
    private void FixedUpdate()
    {
        if (m_Path == null)
            return;

        if (m_CurrentWaypoint >= m_Path.vectorPath.Count)
        {
            Debug.Log("ende");
            m_EndPathReached = true;
        }
        else
        {
            m_EndPathReached = false;
        }

        switch (m_Status)
        {
            case ENPCStatus.VARGANT:
                Vagrant();
                break;
            case ENPCStatus.VILLAGER:
                Villager();
                break;
            case ENPCStatus.BUILDER:
                Builder();
                break;
            case ENPCStatus.ARCHER:
                Archer();
                break;
            case ENPCStatus.SWORDSMAN:
                break;
            default:
                break;
        }
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

    #region private Collision Function
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            m_Target = this.gameObject;
            m_Status = ENPCStatus.VILLAGER;
        }
        if(collision.CompareTag("Hammer"))
        {
            Destroy(collision.gameObject);
            m_Target = this.gameObject;
            m_Status = ENPCStatus.BUILDER;
        }
        if (collision.CompareTag("Bow"))
        {
            Destroy(collision.gameObject);
            m_Target = this.gameObject;
            m_Status = ENPCStatus.ARCHER;
        }
        if (collision.CompareTag("Sword"))
        {
            Destroy(collision.gameObject);
            m_Target = this.gameObject;
            m_Status = ENPCStatus.SWORDSMAN;
        }
    } 
    #endregion
}
