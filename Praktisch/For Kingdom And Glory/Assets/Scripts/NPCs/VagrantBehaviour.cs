using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public partial class VagrantBehaviour : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    protected float m_Speed = 0.5f;
    [SerializeField]
    private SkeletonAnimation m_Animation;
    [SerializeField]
    [Range(1, 10)]
    private float m_ShootTimer = 1.5f;
    [SerializeField]
    [Range(0, 10)]
    private float m_IdleTimer = 1.0f;
    [SerializeField]
    private GameObject m_Sprite;
    [SerializeField]
    private GameObject m_Arrow;
    [SerializeField]
    private Transform m_ThrowPoint;
    [SerializeField]
    private AudioSource m_Source;
    [Header("Job Visibility")]
    [SerializeField]
    private GameObject m_BowVis;
    [SerializeField]
    private GameObject m_HammerVis;

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

    private GameObject m_TownHall = null;
    #endregion

    private Vector2 m_Direction;
    private Rigidbody2D m_Rigid;
    private SpriteRenderer m_Render;
    private List<GameObject> m_BuildWalls = new List<GameObject>();
    private ENPCStatus m_Status;
    private float m_Timer = 0.0f;
    private float m_ShootTime = 0.0f;
    private float m_Distance;
    private float m_AnimationTime = 1.0f;
    private float m_AnimationTimer = 0.0f;
    private float m_Random = 0.0f;
    private int m_CurrentDirection;
    private int m_CurrentReparingWall;
    private bool m_HammerInRange = false;
    private bool m_BowInRange = false;
    private bool m_ReparingWall = false;
    private bool m_Hunting = false;
    private bool m_HasJob = false;
    private bool m_IsIdle = false;
    private bool m_ChangedSkin = false;
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
        m_TownHall = GameObject.FindGameObjectWithTag("Town");

        m_Random = Random.Range(1.0f, 4.0f);

        m_Target = m_Waypoints[m_CurrentDirection];

        m_Status = ENPCStatus.VARGANT;

        m_BowVis.SetActive(false);
        m_HammerVis.SetActive(false);
    }
    private void Update()
    {
        if (!GameManager.Instance.IsAlive)
            return;

        if (GameManager.Instance.IsPaused)
        {
            m_Rigid.velocity = new Vector2(0, 0);
            return;
        }

        if (m_Rigid.velocity.x > 0.0f || m_Rigid.velocity.x < 0.0f)
        {
            if (m_AnimationTimer >= m_AnimationTime)
            {
                ChangeAnimation("Walk", true);
                m_AnimationTimer = 0.0f;
            }
        }

        if (m_Rigid.velocity.x == 0.0f)
        {
            if (m_AnimationTimer >= m_AnimationTime)
            {
                ChangeAnimation("Idle", true);
                m_AnimationTimer = 0.0f;
            }
        }

        if (m_Direction.x > 0.0f)
        {
            m_Sprite.transform.localScale = new Vector3(Mathf.Abs(m_Sprite.transform.localScale.x), m_Sprite.transform.localScale.y, 1f);
        }
        if (m_Direction.x < 0.0f)
        {
            m_Sprite.transform.localScale = new Vector3(-Mathf.Abs(m_Sprite.transform.localScale.x), m_Sprite.transform.localScale.y, 1f);
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

        if (m_Target == null)
            return;

        m_Direction = ((Vector2)m_Target.transform.position - m_Rigid.position).normalized;

        if (!m_IsIdle)
            m_Rigid.velocity = m_Direction * m_Speed;

        m_AnimationTimer += Time.deltaTime;
    }

    #region private Functions
    /// <summary>
    /// Change Animation Function
    /// </summary>
    /// <param name="_Name">Animation name</param>
    /// <param name="_Loop">If Animation should be looped</param>
    private void ChangeAnimation(string _Name, bool _Loop)
    {
        if (m_Animation == null)
        {
            return;
        }

        if (_Name == "Idle" || _Name == "Attack")
            m_AnimationTime = 1.5f;

        if (_Name == "Walk")
            m_AnimationTime = 1.0f;


        m_Animation.AnimationState.SetAnimation(0, _Name, _Loop);
    }
    #endregion

    #region private Collision Function
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_Status == ENPCStatus.VARGANT)
        {
            if (collision.CompareTag("Coin"))
            {
                if (collision.gameObject == null)
                    return;

                if (!m_Coins.Contains(collision.gameObject))
                    return;

                VagrantManager.Instance.RemoveCoinsForAll(collision.gameObject);
                VagrantManager.Instance.RemoveFromList(this.gameObject);
                VillagerManager.Instance.AddToList(this.gameObject);
                m_ChangedSkin = false;
                Destroy(collision.gameObject);
                this.gameObject.tag = "Villager";
                m_Status = ENPCStatus.VILLAGER;
            }
        }
        if (m_Status == ENPCStatus.VILLAGER)
        {
            if (collision.CompareTag("Hammer"))
            {
                if (!m_Hammers.Contains(collision.gameObject))
                    return;

                BuilderStand.Instance.RemoveHammerFromStand(collision.gameObject);
                VillagerManager.Instance.RemoveAllHammer(collision.gameObject);
                VillagerManager.Instance.RemoveFromList(this.gameObject);
                BuilderManager.Instance.AddBuilderToList(this.gameObject);
                m_Bows.Clear();
                m_Hammers.Clear();
                m_HasJob = true;
                m_HammerVis.SetActive(true);
                this.gameObject.tag = "Builder";
                m_Status = ENPCStatus.BUILDER;
            }
            if (collision.CompareTag("Bow"))
            {
                if (!m_Bows.Contains(collision.gameObject))
                    return;

                Archery.Instance.RemoveBowFromStand(collision.gameObject);
                VillagerManager.Instance.RemoveAllBow(collision.gameObject);
                ArcherManager.Instance.AddToList(this.gameObject);
                VillagerManager.Instance.RemoveFromList(this.gameObject);
                m_Bows.Clear();
                m_Hammers.Clear();
                m_BowVis.SetActive(true);
                m_HasJob = true;
                this.gameObject.tag = "Archer";
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
    /// <summary>
    /// Step Down function
    /// Getting called when npc gets hit
    /// will move down an state when being hit
    /// will go down states until he's an vagrant again
    /// </summary>
    public void StepDown()
    {
        switch (m_Status)
        {
            case ENPCStatus.VILLAGER:
                m_Status = ENPCStatus.VARGANT;
                EnemyManager.Instance.RemoveVillagerFromList(this.gameObject);
                VagrantManager.Instance.AddToList(this.gameObject);
                this.gameObject.tag = "Vagrant";
                m_ChangedSkin = false;
                m_IsIdle = false;
                m_VagrantIdle = true;
                break;
            case ENPCStatus.BUILDER:
                m_Status = ENPCStatus.VILLAGER;
                BuilderManager.Instance.RemoveBuilderFromList(this.gameObject);
                EnemyManager.Instance.RemoveFromBuilderList(this.gameObject);
                VillagerManager.Instance.AddToList(this.gameObject);
                m_HammerVis.SetActive(false);
                this.gameObject.tag = "Villager";
                m_IsIdle = false;
                break;
            case ENPCStatus.ARCHER:
                m_Status = ENPCStatus.VILLAGER;
                ArcherManager.Instance.RemoveFromList(this.gameObject);
                EnemyManager.Instance.RemoveFromArcherList(this.gameObject);
                VillagerManager.Instance.AddToList(this.gameObject);
                this.gameObject.tag = "Villager";
                m_Enemies.Clear();
                m_BowVis.SetActive(false);
                m_IsIdle = false;
                m_IsAttacking = false;
                m_Searched = false;
                m_IsDefending = false;
                break;
            default:
                break;
        }
    }
    #endregion
}
