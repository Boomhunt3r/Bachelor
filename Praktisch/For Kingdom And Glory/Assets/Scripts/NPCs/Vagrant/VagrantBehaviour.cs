using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class VagrantBehaviour : MonoBehaviour
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
    private GameObject m_Coin;
    private GameObject m_Target;
    private Rigidbody2D m_Rigid;
    private List<Vector2> m_Directions = new List<Vector2>();
    private Vector2 m_CurrentVeloc;
    private int m_CurrentDirection;
    private ENPCStatus m_Status;
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
        m_Waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        //m_Directions.Add(new Vector2(1, 0));
        //m_Directions.Add(new Vector2(-1, 0));
        //m_CurrentDirection = Random.Range(0, m_Directions.Count);
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

        //Debug.Log($"Cur Way: {m_CurrentWaypoint}");
        //Debug.Log($"Max Way: {m_Path.vectorPath.Count}");

       /* if (m_EndPathReached == true)
        {
            if(m_CurrentDirection == 0)
            {
                m_Target = m_Waypoints[1];
            }
            else if (m_CurrentDirection == 1)
            {
                m_Target = m_Waypoints[0];
            }
        }*/

        Vector2 Direction = ((Vector2)m_Path.vectorPath[m_CurrentWaypoint] - m_Rigid.position).normalized;
        Vector2 force = Direction * m_Speed * Time.deltaTime;

        m_Rigid.AddForce(force);

        float Distance = Vector2.Distance(m_Rigid.position, m_Path.vectorPath[m_CurrentWaypoint]);

        if (Distance < m_NextWaypointDist)
        {
            m_CurrentWaypoint++;
        }

        if (m_Rigid.velocity.x >= 0.0f)
        {
            m_Sprite.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (m_Rigid.velocity.x <= 0.0f)
        {
            m_Sprite.localScale = new Vector3(1f, 1f, 1f);
        }


        if (m_Coin == null)
        {
            m_Coin = GameObject.FindGameObjectWithTag("Coin");
            m_Target = m_Waypoints[m_CurrentDirection];
        }

        if (m_Coin != null)
        {
            Debug.Log(Vector2.SqrMagnitude(this.transform.position - m_Coin.transform.position));
            if (Vector2.SqrMagnitude((Vector2)m_Coin.transform.position - m_Rigid.position) <= 45)
            {
                Debug.Log("In Range");
                m_Target = m_Coin;
            }
        }
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);

        }
    }
}
