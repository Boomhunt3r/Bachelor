using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance { get; private set; }

    #region Serializefield
    [SerializeField]
    private float m_Speed = 25.0f;
    [SerializeField]
    private float m_ArrowSpeed = 250.0f;
    [SerializeField]
    [Range(1, 8)]
    private float m_AttackTime = 2.5f;
    [SerializeField]
    [Range(1, 5)]
    private int m_Health = 1;
    [SerializeField]
    private GameObject m_Arrow;
    [SerializeField]
    private Transform m_Sprite;
    #endregion

    #region private Variables
    private Rigidbody2D m_Rigid;
    private GameObject m_Target;
    private GameObject m_ClosestWall;
    private GameObject m_ClosestVilliger;
    private GameObject m_ClosestBuilder;
    private GameObject m_ClosestArcher;
    private GameObject m_Player;
    private List<GameObject> m_Walls = new List<GameObject>();
    private List<GameObject> m_Villiger = new List<GameObject>();
    private List<GameObject> m_Builder = new List<GameObject>();
    private List<GameObject> m_Archer = new List<GameObject>();
    private Vector2 m_Direction;
    private float m_Timer;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();

        m_Walls = GameObject.FindGameObjectsWithTag("Wall").OfType<GameObject>().ToList();
        m_Villiger = GameObject.FindGameObjectsWithTag("Villager").OfType<GameObject>().ToList();
        m_Builder = GameObject.FindGameObjectsWithTag("Builder").OfType<GameObject>().ToList();
        m_Archer = GameObject.FindGameObjectsWithTag("Archer").OfType<GameObject>().ToList();
        m_Player = GameObject.FindGameObjectWithTag("Player");

        if (m_Walls.Count != 0)
            m_ClosestWall = GetClosestTarget(m_Walls);

        if (m_Villiger.Count != 0)
            m_ClosestVilliger = GetClosestTarget(m_Villiger);

        if (m_Builder.Count != 0)
            m_ClosestBuilder = GetClosestTarget(m_Builder);

        if (m_Villiger.Count != 0)
            m_ClosestArcher = GetClosestTarget(m_Archer);

        if (m_Villiger.Count != 0 && m_Walls.Count != 0)
            m_Target = GetClosestOverall(m_ClosestWall, m_ClosestVilliger, m_ClosestBuilder, m_ClosestArcher, m_Player);

        else if (m_Villiger.Count != 0 && m_Walls.Count == 0)
            m_Target = m_ClosestVilliger;

        else if (m_Villiger.Count == 0 && m_Walls.Count != 0)
            m_Target = m_ClosestWall;

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Walls.Count != 0)
            m_ClosestWall = GetClosestTarget(m_Walls);

        if (m_Villiger.Count != 0)
            m_ClosestVilliger = GetClosestTarget(m_Villiger);

        if (m_Builder.Count != 0)
            m_ClosestBuilder = GetClosestTarget(m_Builder);

        if (m_Archer.Count != 0)
            m_ClosestArcher = GetClosestTarget(m_Archer);

        if (m_Villiger.Count != 0 && m_Walls.Count != 0)
            m_Target = GetClosestOverall(m_ClosestWall, m_ClosestVilliger, m_ClosestBuilder, m_ClosestArcher, m_Player);

        else if (m_Villiger.Count != 0 && m_Walls.Count == 0 && m_Builder.Count == 0 && m_Archer.Count == 0)
            m_Target = m_ClosestVilliger;

        else if (m_Walls.Count != 0 && m_Villiger.Count == 0 && m_Builder.Count == 0 && m_Archer.Count == 0)
            m_Target = m_ClosestWall;

        else if (m_Builder.Count != 0 && m_Villiger.Count == 0 && m_Walls.Count == 0 && m_Archer.Count == 0)
            m_Target = m_ClosestBuilder;

        else if (m_Archer.Count != 0 && m_Villiger.Count == 0 && m_Walls.Count == 0 && m_Builder.Count == 0)
            m_Target = m_ClosestArcher;

        m_Direction = ((Vector2)m_Target.transform.position - m_Rigid.position).normalized;
        m_Rigid.velocity = m_Direction * m_Speed * Time.deltaTime;
        float Distance = Vector2.Distance(m_Rigid.position, m_Target.transform.position);

        if (Distance < 3.5f)
        {
            m_Rigid.velocity = new Vector2(0, 0);
        }

        if (m_Rigid.velocity.x == 0)
        {
            m_Timer += Time.deltaTime;

            if (m_Timer >= m_AttackTime)
                Attack();
        }
    }

    #region private Functions
    private GameObject GetClosestTarget(List<GameObject> _Target)
    {
        GameObject Target = null;
        float MinDist = Mathf.Infinity;
        Vector2 CurrentPos = transform.position;
        float Dist = 0.0f;

        for (int i = 0; i < _Target.Count; i++)
        {
            Dist = Vector2.Distance(_Target[i].transform.position, CurrentPos);

            if (Dist < MinDist)
            {
                Target = _Target[i];
                MinDist = Dist;
            }
        }

        return Target;
    }

    /// <summary>
    /// Get the Closest Target from own Position
    /// </summary>
    /// <param name="_Wall">Closest Wall</param>
    /// <param name="_Villiger">Closest Villiger</param>
    /// <param name="_Builder">Closest Builder</param>
    /// <param name="_Archer">Closest Archer</param>
    /// <param name="_Player">Player</param>
    /// <returns>Closest Target overall</returns>
    private GameObject GetClosestOverall(GameObject _Wall, GameObject _Villiger, GameObject _Builder, GameObject _Archer, GameObject _Player)
    {
        GameObject Target = null;
        Vector2 CurrentPos = transform.position;
        // Distance to Wall
        float DistW = 0.0f;
        // Distance to Villiger
        float DistV = 0.0f;
        // Distance to Builder
        float DistB = 0.0f;
        // Distance to Archer
        float DistA = 0.0f;
        // Distance to Player
        float DistP = 0.0f;

        DistW = Vector2.Distance(_Wall.transform.position, CurrentPos);
        DistV = Vector2.Distance(_Villiger.transform.position, CurrentPos);
        DistB = Vector2.Distance(_Builder.transform.position, CurrentPos);
        DistA = Vector2.Distance(_Archer.transform.position, CurrentPos);
        DistP = Vector2.Distance(_Player.transform.position, CurrentPos);

        if (DistW < DistV && DistW < DistB && DistW < DistA && DistW < DistP)
        {
            Target = _Wall;
        }
        else if (DistV <= DistW && DistV < DistB && DistV < DistA && DistV < DistP)
        {
            Target = _Villiger;
        }
        else if (DistB <= DistV && DistB <= DistW && DistB < DistA && DistB < DistP)
        {
            Target = _Builder;
        }
        else if (DistA <= DistV && DistA <= DistW && DistA <= DistB && DistA < DistP)
        {
            Target = _Archer;
        }
        else if (DistP <= DistV && DistP <= DistW && DistP <= DistB && DistP <= DistA)
        {
            Target = _Player;
        }

        return Target;
    }

    /// <summary>
    /// Attack Function
    /// </summary>
    private void Attack()
    {
        GameObject Arrow = Instantiate(m_Arrow, this.transform.position, Quaternion.identity);
        Arrow.GetComponent<Rigidbody2D>().velocity = m_Direction * m_ArrowSpeed * Time.deltaTime;
        
        m_Timer = 0.0f;
    }
    #endregion

    #region public Functions
    /// <summary>
    /// When Enemy is Hit
    /// </summary>
    /// <param name="_Amount">Amount of Damage</param>
    public void TakeDamage(int _Amount)
    {
        m_Health -= _Amount;

        if (m_Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// If Wall Destroyed
    /// Remove Current Wall From List
    /// </summary>
    public void RemoveWallFromList()
    {
        m_Walls.Remove(m_ClosestWall);
    }

    /// <summary>
    /// If Villiger Destroyed
    /// Remove From List
    /// </summary>
    public void RemoveVilligerFromList()
    {
        m_Villiger.Remove(m_ClosestVilliger);
    }
    #endregion
}
