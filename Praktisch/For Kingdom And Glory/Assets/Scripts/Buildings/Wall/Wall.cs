using UnityEngine;

public class Wall : MonoBehaviour
{
    #region private Serialize Variables
    [SerializeField]
    private EBuildingUpgrade m_Building;
    [SerializeField]
    private float m_MaxHitPoints = 25;
    [SerializeField]
    private GameObject m_Sprite;
    [SerializeField]
    private Sprite[] m_Sprites;
    [SerializeField]
    private GameObject m_BuildUI;
    [SerializeField]
    private GameObject m_DefendPoint;
    [SerializeField]
    private SpriteRenderer m_Render;
    [SerializeField]
    private ESpawnerSide m_Side;
    #endregion

    #region private Variables
    private GameObject m_WallObj;
    private BoxCollider2D m_Collider;
    private int m_CoinCost = 0;
    private int m_WoodCost = 0;
    private int m_StoneCost = 0;
    private int m_IronCost = 0;
    private float m_Timer = 0.0f;
    private float m_MaxTimer = 1.5f;
    private float m_CurrentHitPoints;
    private bool m_Build = false;
    private bool m_Payed = false;
    private bool m_BeingBuild = false;
    private bool m_BuilderBuilding = false;
    private bool m_isActive = false;
    #endregion

    #region private const
    /// <summary>
    /// 
    /// </summary>
    private const int m_CostPerUpgrade = 2;
    #endregion

    #region MyRegion
    public bool Build { get => m_Build; set { m_Build = value; m_BuildUI.SetActive(m_Build); } }
    public float MaxHitPoints { get => m_MaxHitPoints; set => m_MaxHitPoints = value; }
    public float CurrentHitPoints { get => m_CurrentHitPoints; set => m_CurrentHitPoints = value; }
    public EBuildingUpgrade Building { get => m_Building; set => m_Building = value; }
    public bool IsActive { get => m_isActive; set => m_isActive = value; }
    public GameObject WallObj { get => m_WallObj; set => m_WallObj = value; }
    public int CoinCost { get => m_CoinCost; set => m_CoinCost = value; }
    public int WoodCost { get => m_WoodCost; set => m_WoodCost = value; }
    public int StoneCost { get => m_StoneCost; set => m_StoneCost = value; }
    public int IronCost { get => m_IronCost; set => m_IronCost = value; }
    public GameObject DefendPoint { get => m_DefendPoint; set => m_DefendPoint = value; }
    #endregion

    // Start is called before the first frame update
    #region Unity Functions
    void Start()
    {
        // Deactivate UI
        m_BuildUI.SetActive(false);
        // Set Resource Cost for first State
        CoinCost = 2;
        // At Start not Build so None
        m_Building = EBuildingUpgrade.NONE;

        m_Collider = GetComponent<BoxCollider2D>();

        if (m_Side == ESpawnerSide.RIGHT)
            m_Render.flipX = true;
    }

    void Update()
    {
        if (m_BuilderBuilding)
        {
            if (m_BeingBuild)
            {
                m_Timer += Time.deltaTime;
            }
        }

        if (m_Timer >= m_MaxTimer)
        {
            m_BeingBuild = false;
            m_Timer = 0.0f;
        }
    }
    #endregion

    #region private Functions
    /// <summary>
    /// Upgrade Building Funcrion
    /// </summary>
    public void UpgradeBuilding()
    {
        switch (m_Building)
        {
            // Status None
            case EBuildingUpgrade.NONE:
                // Increase Max Timer
                m_MaxTimer = 2.5f;
                // Set HP for Wall
                m_CurrentHitPoints = m_MaxHitPoints;
                // Set new Resource Cost
                CoinCost = 4;
                WoodCost = 6;
                // Set Renderer
                m_Render.sprite = m_Sprites[0];
                // Set to next Upgrade Level
                m_Building = EBuildingUpgrade.PILE;
                // Set Collider
                if (m_Side == ESpawnerSide.LEFT)
                {
                    m_Collider.offset = new Vector2(-0.5948417f, -0.07487202f);
                    m_Collider.size = new Vector2(3.300609f, 2.919441f);
                }
                if (m_Side == ESpawnerSide.RIGHT)
                {
                    m_Collider.offset = new Vector2(0.6057814f, -0.07487202f);
                    m_Collider.size = new Vector2(3.300608f, 2.919441f);
                }
                break;
            // Status Pile
            case EBuildingUpgrade.PILE:
                // Increase Max Timer
                m_MaxTimer = 2.5f;
                // Set HP for Wall
                m_CurrentHitPoints = m_MaxHitPoints * 2;
                // Set new Resource Cost
                CoinCost = 6;
                WoodCost = 2;
                StoneCost = 6;
                m_Render.sprite = m_Sprites[1];
                // Set to next Upgrade Level
                m_Building = EBuildingUpgrade.WOOD;
                if (m_Side == ESpawnerSide.LEFT)
                {
                    m_Collider.offset = new Vector2(-0.60507f, 0.2860298f);
                    m_Collider.size = new Vector2(2.107347f, 3.583926f);
                }
                if (m_Side == ESpawnerSide.RIGHT)
                {
                    m_Collider.offset = new Vector2(-0.6166524f, 0.2518417f);
                    m_Collider.size = new Vector2(2.088355f, 3.572869f);
                }
                break;
            // Status Wood
            case EBuildingUpgrade.WOOD:
                m_MaxTimer = 7.5f;
                m_CurrentHitPoints = m_MaxHitPoints * 3;
                CoinCost = 10;
                StoneCost = 4;
                IronCost = 6;
                m_Render.sprite = m_Sprites[2];
                m_Building = EBuildingUpgrade.STONE;
                if (m_Side == ESpawnerSide.LEFT)
                {
                    m_Collider.offset = new Vector2(-0.7101793f, 0.7487187f);
                    m_Collider.size = new Vector2(2.098838f, 4.566622f);
                }
                if (m_Side == ESpawnerSide.RIGHT)
                {
                    m_Collider.offset = new Vector2(-0.7516569f, 0.7816358f);
                    m_Collider.size = new Vector2(2.260916f, 4.575138f);
                }
                break;
            // Status Stone
            case EBuildingUpgrade.STONE:
                m_MaxTimer = 10.0f;
                m_CurrentHitPoints = m_MaxHitPoints * 4;
                m_Render.sprite = m_Sprites[3];
                m_Building = EBuildingUpgrade.IRON;
                if (m_Side == ESpawnerSide.LEFT)
                {
                    m_Collider.offset = new Vector2(-0.7097751f, 0.7816358f);
                    m_Collider.size = new Vector2(2.232993f, 4.575138f);
                }
                if (m_Side == ESpawnerSide.RIGHT)
                {
                    m_Collider.offset = new Vector2(0.6932794f, 0.7816359f);
                    m_Collider.size = new Vector2(2.386559f, 4.575138f);
                }
                break;
            case EBuildingUpgrade.IRON:
                break;
            default:
                break;
        }
    }

    private void DownGrade()
    {
        m_CurrentHitPoints = 0;

        //m_Render.sprite = ;

        Building = EBuildingUpgrade.NONE;
    }
    #endregion

    #region public functions
    public void GetDamage(int _Damage)
    {
        m_CurrentHitPoints -= _Damage;

        if (m_CurrentHitPoints <= 0)
        {
            DownGrade();
            Enemy.Instance.RemoveWallFromList();
        }
    }

    public void GetHealth(int _Health)
    {
        m_CurrentHitPoints += _Health;
    }
    #endregion

    #region Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!m_BeingBuild)
                PlayerBehaviour.Instance.CanBuild = true;
        }
        if (collision.CompareTag("Builder"))
        {
            if (m_BeingBuild)
                m_BuilderBuilding = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!m_BeingBuild)
                PlayerBehaviour.Instance.CanBuild = true;
            else if (m_BeingBuild)
            {
                PlayerBehaviour.Instance.CanBuild = false;
                m_Build = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerBehaviour.Instance.CanBuild = false;
            Build = false;
        }
        if (collision.CompareTag("Builder"))
        {

            m_BuilderBuilding = false;
        }
    }
    #endregion

}