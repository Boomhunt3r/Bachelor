using System.Collections.Generic;
using System.Linq;
using Spine.Unity;
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
    private GameObject m_Monster;
    [SerializeField]
    private SkeletonAnimation m_Animation;
    [SerializeField]
    private Transform m_ThrowPoint;
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
    private float m_AnimationTimer;
    private float m_AnimationTime = 1.0f;
    private float m_Range = 0.0f;
    private bool m_Loop = true;
    [SerializeField]
    private ESpawnerSide m_Side;
    #endregion

    #region Properties
    public ESpawnerSide Side { get => m_Side; set => m_Side = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();

        m_Walls = GameObject.FindGameObjectsWithTag("Wall").ToList();
        m_Villiger = GameObject.FindGameObjectsWithTag("Villager").ToList();
        m_Builder = GameObject.FindGameObjectsWithTag("Builder").ToList();
        m_Archer = GameObject.FindGameObjectsWithTag("Archer").ToList();
        m_Player = GameObject.FindGameObjectWithTag("Player");

        if (Side == ESpawnerSide.RIGHT)
            m_Monster.transform.localScale = new Vector3(-m_Monster.transform.localScale.x, m_Monster.transform.localScale.y, 1);

        if (m_Walls.Count != 0)
            m_ClosestWall = GetClosestWall(m_Walls);

        if (m_Villiger.Count != 0)
            m_ClosestVilliger = GetClosestTarget(m_Villiger);

        if (m_Builder.Count != 0)
            m_ClosestBuilder = GetClosestTarget(m_Builder);

        if (m_Villiger.Count != 0)
            m_ClosestArcher = GetClosestTarget(m_Archer);

        m_Target = GetClosestOverall(m_ClosestWall, m_ClosestVilliger, m_ClosestBuilder, m_ClosestArcher, m_Player);

        m_Animation.Initialize(true);

        m_Range = Random.Range(1.0f, 4.5f);

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsAlive)
            return;

        if (GameManager.Instance.IsPaused)
        {
            m_Rigid.velocity = new Vector2(0, 0);
            return;
        }

        if (m_Walls.Count != 0 && m_Walls != null)
            m_ClosestWall = GetClosestWall(m_Walls);

        if (m_Villiger.Count != 0 && m_Villiger != null)
            m_ClosestVilliger = GetClosestTarget(m_Villiger);

        if (m_Builder.Count != 0 && m_Builder != null)
            m_ClosestBuilder = GetClosestTarget(m_Builder);

        if (m_Archer.Count != 0 && m_Archer != null)
            m_ClosestArcher = GetClosestTarget(m_Archer);

        m_Target = GetClosestOverall(m_ClosestWall, m_ClosestVilliger, m_ClosestBuilder, m_ClosestArcher, m_Player);

        m_Direction = ((Vector2)m_Target.transform.position - m_Rigid.position).normalized;
        m_Rigid.velocity = m_Direction * m_Speed * Time.deltaTime;

        if (m_Rigid.velocity.x == 0.0f)
        {
            if (m_AnimationTimer >= m_AnimationTime)
            {
                ChangeAnimation("Idle", true);
                m_AnimationTimer = 0.0f;
            }
        }

        switch (Side)
        {
            case ESpawnerSide.LEFT:
                if (m_Rigid.velocity.x > 0.0f)
                {
                    if (m_AnimationTimer >= m_AnimationTime)
                    {
                        ChangeAnimation("Walk", true);
                        m_AnimationTimer = 0.0f;
                    }
                }
                break;
            case ESpawnerSide.RIGHT:
                if (m_Rigid.velocity.x < 0.0f)
                {
                    if (m_AnimationTimer >= m_AnimationTime)
                    {
                        ChangeAnimation("Walk", true);
                        m_AnimationTimer = 0.0f;
                    }
                }
                break;
            default:
                break;
        }

        float Distance = Vector2.Distance(m_Rigid.position, m_Target.transform.position);

        if (Distance < m_Range)
        {
            m_Rigid.velocity = new Vector2(0, 0);

            m_Timer += Time.deltaTime;

            if (m_AnimationTimer >= m_AnimationTime)
            {
                ChangeAnimation("Idle", true);
                m_AnimationTimer = 0.0f;
            }

            if (m_Timer >= m_AttackTime)
            {
                ChangeAnimation("Attack", false);
                Attack();
            }
        }

        m_AnimationTimer += Time.deltaTime;
    }


    #region private Functions
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_Target"></param>
    /// <returns>Closest Target</returns>
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
    /// 
    /// </summary>
    /// <param name="_Target">Wall List</param>
    /// <returns>Closest Wall</returns>
    private GameObject GetClosestWall(List<GameObject> _Target)
    {
        GameObject Target = null;
        float MinDist = Mathf.Infinity;
        Vector2 CurrentPos = transform.position;
        float Dist = 0.0f;

        for (int i = 0; i < _Target.Count; i++)
        {

            if (_Target[i].GetComponent<Wall>().Building == EBuildingUpgrade.NONE)
                continue;

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

        if (_Wall == null)
            DistW = Mathf.Infinity;
        else if (_Wall != null)
            DistW = Vector2.Distance(_Wall.transform.position, CurrentPos);

        if (_Villiger == null)
            DistV = Mathf.Infinity;
        else if (_Villiger != null)
            DistV = Vector2.Distance(_Villiger.transform.position, CurrentPos);

        if (_Builder == null)
            DistB = Mathf.Infinity;
        else if (_Builder != null)
            DistB = Vector2.Distance(_Builder.transform.position, CurrentPos);

        if (_Archer == null)
            DistA = Mathf.Infinity;
        else if (_Archer != null)
            DistA = Vector2.Distance(_Archer.transform.position, CurrentPos);

        if (_Player == null)
            DistP = Mathf.Infinity;
        else if (_Player != null)
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
        float XDistance;
        XDistance = Random.Range(m_Target.transform.position.x - m_ThrowPoint.position.x, m_Direction.x * 5.0f);

        float YDistance;
        YDistance = Random.Range(m_Target.transform.position.y - m_ThrowPoint.position.y, 5.0f);

        float ThrowAngle;
        ThrowAngle = Mathf.Atan((YDistance + 4.905f) / XDistance);

        float TotalVelo = XDistance / Mathf.Cos(ThrowAngle);

        float XVelo;
        float YVelo;
        XVelo = TotalVelo * Mathf.Cos(ThrowAngle);
        YVelo = TotalVelo * Mathf.Sin(ThrowAngle);

        GameObject Arrow = Instantiate(m_Arrow, m_ThrowPoint.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(XVelo, YVelo);

        m_Timer = 0.0f;
    }

    /// <summary>
    /// Change Animation Function
    /// </summary>
    /// <param name="_Name">Animation name</param>
    /// <param name="_Loop">If Animation should be looped</param>
    private void ChangeAnimation(string _Name, bool _Loop)
    {
        if (m_Animation == null)
        {
            Debug.LogWarning("No Animator");
            return;
        }

        if (_Name == "Idle" || _Name == "Attack")
            m_AnimationTime = 1.5f;

        if (_Name == "Walk")
            m_AnimationTime = 1.0f;

        if (_Name == "Dead")
            m_Animation.AnimationState.SetAnimation(0, _Name, _Loop);

        m_Animation.AnimationState.SetAnimation(0, _Name, _Loop);
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
            GameManager.Instance.RemoveEnemyFromList(this.gameObject);

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
