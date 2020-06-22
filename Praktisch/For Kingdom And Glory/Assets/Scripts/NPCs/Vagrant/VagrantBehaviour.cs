using System.Collections.Generic;
using UnityEngine;

public class VagrantBehaviour : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    protected float m_Speed = 0.5f;
    [SerializeField]
    protected GameObject[] m_Waypoints;
    #endregion

    #region protected Variables
    protected GameObject m_Coin;
    protected Rigidbody2D m_Rigid;
    protected List<Vector2> m_Directions = new List<Vector2>();
    protected Vector2 m_CurrentVeloc;
    protected int m_CurrentDirection;
    protected ENPCStatus m_Status;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
        m_Directions.Add(new Vector2(1, 0));
        m_Directions.Add(new Vector2(-1, 0));
        m_CurrentDirection = Random.Range(0, m_Directions.Count);
        m_Waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        m_Status = ENPCStatus.VARGANT;

        m_CurrentVeloc = m_Directions[m_CurrentDirection] * m_Speed;
    }
    private void Update()
    {
        m_Rigid.velocity = m_CurrentVeloc;

        if (m_Coin == null)
            m_Coin = GameObject.FindGameObjectWithTag("Coin");

        if (m_Coin != null)
        {
            Debug.Log(Vector2.SqrMagnitude(this.transform.position - m_Coin.transform.position));
            if (Vector2.SqrMagnitude(this.transform.position - m_Coin.transform.position) <= 45)
            {
                Debug.Log("In Range");
                m_CurrentVeloc = new Vector2(0, 0);
            }
        }
        else
        {
            if (this.transform.position.x <= m_Waypoints[0].transform.position.x)
            {
                m_CurrentVeloc = m_Directions[0] * m_Speed;
            }
            else if (this.transform.position.x >= m_Waypoints[1].transform.position.x)
            {
                m_CurrentVeloc = m_Directions[1] * m_Speed;
            }
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
