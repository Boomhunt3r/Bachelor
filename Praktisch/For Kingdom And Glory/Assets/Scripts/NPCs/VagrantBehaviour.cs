using System.Collections.Generic;
using UnityEngine;

public partial class VagrantBehaviour : MonoBehaviour
{
    public static VagrantBehaviour Instance { get; private set; }

    #region SerializeField
    [SerializeField]
    protected float m_Speed = 0.5f;
    [SerializeField]
    private float m_NextWaypointDist = 3.0f;
    [SerializeField]
    [Range(1, 10)]
    private float m_ShootTimer = 1.5f;
    [SerializeField]
    [Range(0, 10)]
    private float m_IdleTimer = 1.0f;
    [SerializeField]
    private Transform m_Sprite;
    [SerializeField]
    private GameObject m_Arrow;
    [SerializeField]
    private Transform m_ThrowPoint;
    [SerializeField]
    private float m_ArrowSpeed = 1000.0f;
    [SerializeField]
    private AudioSource m_Source;
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
    /// Wall Archer is Defending
    /// </summary>
    private GameObject m_DefendingWall;
    /// <summary>
    /// Villager Waypoints
    /// </summary>
    [SerializeField]
    private GameObject[] m_VillagerPoints;
    /// <summary>
    /// Vargant Waypoints
    /// </summary>
    private GameObject[] m_Waypoints;
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
    /// <summary>
    /// Rabbits in Game
    /// </summary>
    private List<GameObject> m_Rabbits = new List<GameObject>();
    #endregion

    private Vector2 m_Direction;
    private Rigidbody2D m_Rigid;
    private SpriteRenderer m_Render;
    private List<GameObject> m_BuildWalls = new List<GameObject>();
    private ENPCStatus m_Status;
    private float m_Timer = 0.0f;
    private float m_ShootTime = 0.0f;
    private float m_Distance;
    private int m_CurrentDirection;
    private int m_CurrentReparingWall;
    private bool m_HammerInRange = false;
    private bool m_BowInRange = false;
    private bool m_ReparingWall = false;
    private bool m_Hunting = false;
    private bool m_HasJob = false;
    #endregion

    #region Properties
    public GameObject DefendingWall { get => m_DefendingWall; set => m_DefendingWall = value; }
    public List<GameObject> Rabbits { get => m_Rabbits; set => m_Rabbits = value; } 
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
        m_Render = GetComponentInChildren<SpriteRenderer>();

        m_Waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        m_VillagerPoints = GameObject.FindGameObjectsWithTag("VillagerPoint");

        m_CurrentDirection = Random.Range(0, m_Waypoints.Length);
        m_Target = m_Waypoints[m_CurrentDirection];

        m_Status = ENPCStatus.VARGANT;

        Instance = this;
    }
    private void Update()
    {
        if (!GameManager.Instance.IsAlive)
            return;

        if (m_Rigid.velocity.x >= 0.0f)
        {
            m_Sprite.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (m_Rigid.velocity.x <= 0.0f)
        {
            m_Sprite.localScale = new Vector3(1f, 1f, 1f);
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

        m_Direction = ((Vector2)m_Target.transform.position - m_Rigid.position).normalized;

        m_Rigid.velocity = m_Direction * m_Speed * Time.deltaTime;
    }

    #region private Collision Function
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!m_HasJob)
        {
            if (collision.CompareTag("Coin"))
            {
                RemoveCoin(collision.gameObject);
                Destroy(collision.gameObject);
                m_Status = ENPCStatus.VILLAGER;
            }
            if (collision.CompareTag("Hammer"))
            {
                BuilderStand.Instance.RemoveHammerFromStand(collision.gameObject);
                m_HasJob = true;
                m_Status = ENPCStatus.BUILDER;
            }
            if (collision.CompareTag("Bow"))
            {
                Archery.Instance.RemoveBowFromStand(collision.gameObject);
                ArcherManager.Instance.AddToList(this.gameObject);
                m_HasJob = true;
                m_Status = ENPCStatus.ARCHER;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            if (m_Status == ENPCStatus.BUILDER)
                m_CurrentlyReparing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            if (m_Status == ENPCStatus.BUILDER)
            {
                if (m_CurrentlyReparing == true)
                    m_CurrentlyReparing = false;
            }
        }
    }
    #endregion

    #region public Function
    public void StepDown()
    {
        switch (m_Status)
        {
            case ENPCStatus.VILLAGER:
                m_Status = ENPCStatus.VARGANT;
                break;
            case ENPCStatus.BUILDER:
                m_Status = ENPCStatus.VILLAGER;
                break;
            case ENPCStatus.ARCHER:
                m_Status = ENPCStatus.VILLAGER;
                break;
            case ENPCStatus.SWORDSMAN:
                break;
            default:
                break;
        }
    }
    #endregion
}
