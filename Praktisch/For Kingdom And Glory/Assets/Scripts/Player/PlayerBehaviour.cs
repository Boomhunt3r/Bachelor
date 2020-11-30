using UnityEngine;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour Instance { get; private set; }

    #region SerzializeField
    [Header("Player Settings")]
    [SerializeField]
    private float m_MovementSpeed = 5.0f;
    [SerializeField]
    [Range(50, 200)]
    private int m_Health = 50;
    [SerializeField]
    private Transform m_Sprite;
    [SerializeField]
    private Animator m_Animator;
    [Header("Coin Settings")]
    [SerializeField]
    private GameObject m_CoinPrefab;
    [SerializeField]
    private Transform m_CoinSpawnPoint;
    [Header("Bow Settings")]
    [SerializeField]
    private GameObject m_Arrow;
    [SerializeField]
    private Transform m_ThrowPoint;
    [Header("Audio Setting")]
    [SerializeField]
    private AudioSource m_EffectSource;
    [SerializeField]
    private AudioSource m_Source;
    [SerializeField]
    private AudioClip[] m_BowClips;
    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI m_HealthText;
    [SerializeField]
    private TextMeshProUGUI m_ArmorText;
    #endregion

    #region private Variables
    private Rigidbody2D m_Rigid;

    private SpriteRenderer m_Render;

    private GameObject m_WallObj;

    private Vector2 m_PrevDirec;

    private Vector2 m_Dir;

    private int m_BowClip = 0;

    private int m_Armor;

    private int m_MaxArmor;

    private int m_Helmet;

    private int m_Plate;

    private int m_Boots;

    private const int m_MaxHealth = 50;

    private float m_ShootTimer = 4.5f;

    private float m_Timer = 0.0f;

    private float m_DamageTimer = 0.0f;

    private float m_GetHealth = 5.0f;

    private bool m_Build = false;

    private bool m_CanBuild = false;

    private bool m_IsBuilding = false;

    private bool m_CanBuyBows = false;

    private bool m_CanBuyHammer = false;

    private bool m_IsBuing = false;

    private bool m_CanCraft = false;

    private bool m_IsCrafting = false;

    private bool m_CanTown = false;

    private bool m_IsTown = false;

    private bool m_ESCPressed = false;

    private bool m_GetDamage = false;
    #endregion

    #region Properties
    public bool CanBuild { get => m_CanBuild; set => m_CanBuild = value; }
    public bool CanBuyBows { get => m_CanBuyBows; set => m_CanBuyBows = value; }
    public bool CanCraft { get => m_CanCraft; set => m_CanCraft = value; }
    public bool CanTown { get => m_CanTown; set => m_CanTown = value; }
    public float ShootTimer { get => m_ShootTimer; set => m_ShootTimer = value; }
    public int Armor { get => m_Armor; set => m_Armor = value; }
    public AudioSource EffectSource { get => m_EffectSource; set => m_EffectSource = value; }
    public AudioSource Source { get => m_Source; set => m_Source = value; }
    public bool CanBuyHammer { get => m_CanBuyHammer; set => m_CanBuyHammer = value; }
    #endregion

    int test = 3;
    private void Awake()
    {

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
        m_Render = GetComponentInChildren<SpriteRenderer>();
        m_Animator.Play("IdleAnim");
    }

    // Update is called once per frame
    void Update()
    {
        m_HealthText.text = $"{m_Health}";
        m_ArmorText.text = $"{m_Armor}";

        if (!GameManager.Instance.IsAlive)
            return;

        if (GameManager.Instance.IsPaused)
        {
            m_Rigid.velocity = new Vector2(0, 0);
            m_Animator.StopPlayback();
            m_Source.Stop();
        }

        if (!GameManager.Instance.IsPaused)
        {
            m_Dir = Input.GetAxis("Horizontal") * Vector2.right * m_MovementSpeed;
            m_Rigid.velocity = m_Dir * Time.deltaTime;
        }

        if (m_Dir.x > 0.0f)
        {
            m_Sprite.localScale = new Vector3(-1f, 1f, 1f);
            m_Animator.Play("WalkAnim");
            m_PrevDirec = new Vector2(1f, 1f);
            if (!Source.isPlaying)
                Source.Play();
        }
        else if (m_Dir.x < 0.0f)
        {
            m_Sprite.localScale = new Vector3(1f, 1f, 1f);
            m_Animator.Play("WalkAnim");
            m_PrevDirec = new Vector2(-1f, 1f);
            if (!Source.isPlaying)
                Source.Play();
        }
        if (m_Dir.x == 0)
        {
            m_Animator.Play("IdleAnim");
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            if (m_Build == false)
            {
                if (Inventory.Instance.Coins > 0)
                {
                    Instantiate(m_CoinPrefab, m_CoinSpawnPoint.position, Quaternion.identity);

                    Inventory.Instance.Coins--;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Inventory.Instance.Bow > EPlayerUpgrade.NONE)
            {
                if (m_Timer >= ShootTimer)
                {
                    if (m_BowClip > m_BowClips.Length - 1)
                        m_BowClip = 0;

                    if (m_Dir.x > 0.0f || m_Dir.x < 0.0f)
                        m_Animator.Play("AttackWalkAnim");
                    if (m_Dir.x == 0.0f)
                        m_Animator.Play("AttackIdleAnim");

                    EffectSource.clip = m_BowClips[m_BowClip];
                    Shoot(m_PrevDirec);
                    m_BowClip++;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.Instance.IsPaused)
            {
                GameManager.Instance.IsPaused = true;
            }

            else if (GameManager.Instance.IsPaused)
            {
                GameManager.Instance.IsPaused = false;
            }
        }

        #region Interact
        if (CanBuild || CanBuyBows || CanCraft || CanTown || CanBuyHammer)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (CanBuild)
                {
                    if (m_IsBuilding == false)
                    {
                        m_WallObj.GetComponent<Wall>().Build = true;
                        m_IsBuilding = true;
                    }
                    else if (m_IsBuilding == true)
                    {
                        m_WallObj.GetComponent<Wall>().Build = false;
                        m_IsBuilding = false;
                    }
                }
                else if (CanBuyBows)
                {
                    Archery.Instance.AddBowToStand();
                }
                else if (CanCraft)
                {
                    if (m_IsCrafting == false)
                    {
                        CraftingSystem.Instance.IsCrafting = true;
                        m_IsCrafting = true;
                    }
                    else if (m_IsCrafting == true)
                    {
                        CraftingSystem.Instance.IsCrafting = false;
                        m_IsCrafting = false;
                    }
                }
                else if (CanTown)
                {
                    if (m_IsTown == false)
                    {
                        TownBuilding.Instance.IsActive = true;
                        m_IsTown = true;
                    }
                    else if (m_IsTown == true)
                    {
                        TownBuilding.Instance.IsActive = false;
                        m_IsTown = false;
                    }
                }
                else if (CanBuyHammer)
                {
                    BuilderStand.Instance.AddHammerToStand();
                }
            }
        }
        #endregion

        m_Timer += Time.deltaTime;
    }

    #region private functions
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_Direction">Direction Player is looking</param>
    private void Shoot(Vector2 _Direction)
    {
        float XDistance = 0.0f;
        XDistance = Random.Range(_Direction.x * 2.5f, _Direction.x * 5.0f);

        float YDistance = 0.0f;
        YDistance = Random.Range(transform.position.y + 10.0f - m_ThrowPoint.position.y, 5.0f);

        float ThrowAngle;
        ThrowAngle = Mathf.Atan((YDistance + 4.905f) / XDistance);

        float TotalVelo = XDistance / Mathf.Cos(ThrowAngle);

        float XVelo;
        float YVelo;
        XVelo = TotalVelo * Mathf.Cos(ThrowAngle);
        YVelo = TotalVelo * Mathf.Sin(ThrowAngle);

        GameObject Arrow = Instantiate(m_Arrow, m_ThrowPoint.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(XVelo, YVelo);

        EffectSource.Play();

        m_Timer = 0.0f;
    }
    #endregion

    #region Collision Function
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            if (GameManager.Instance.SpawnedVagrants.Count == 0)
            {
                Destroy(collision.gameObject);
                Inventory.Instance.Coins++;
                return;
            }
            else
            {
                for (int i = 0; i < GameManager.Instance.SpawnedVagrants.Count; i++)
                {
                    GameManager.Instance.SpawnedVagrants[i].GetComponent<VagrantBehaviour>().RemoveCoin(collision.gameObject);
                }
                Destroy(collision.gameObject);
                Inventory.Instance.Coins++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            m_WallObj = collision.gameObject;
            WallManager.Instance.WallObj = collision.gameObject;
            TutorialManager.Instance.Tutorial(collision.gameObject.tag);
        }
        if (collision.CompareTag("Archery"))
        {
            TutorialManager.Instance.Tutorial(collision.gameObject.tag);
        }
        if (collision.CompareTag("Build"))
        {
            TutorialManager.Instance.Tutorial(collision.gameObject.tag);
        }
        if (collision.CompareTag("Smithy"))
        {
            TutorialManager.Instance.Tutorial(collision.gameObject.tag);
        }
        if (collision.CompareTag("Town"))
        {
            TutorialManager.Instance.Tutorial(collision.gameObject.tag);

        }
        if (collision.CompareTag("VagrantSpawner"))
        {
            TutorialManager.Instance.Tutorial(collision.gameObject.tag);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            m_WallObj = null;
            WallManager.Instance.WallObj = null;
        }
    }
    #endregion

    #region public functions
    // TODO: Repair Armor
    /*
    public void RepairArmor()
    {
        #region MyRegion

        switch (Inventory.Instance.Helmet)
        {
            case EPlayerUpgrade.NONE:
                break;
            case EPlayerUpgrade.STONE:
                if (Inventory.Instance.Stone >= 3)
                    Inventory.Instance.Stone -= 3;
                break;
            case EPlayerUpgrade.IRON:
                if (Inventory.Instance.Iron >= 3)
                    Inventory.Instance.Iron -= 3;
                break;
            default:
                break;
        }

        switch (Inventory.Instance.Plate)
        {
            case EPlayerUpgrade.NONE:
                break;
            case EPlayerUpgrade.STONE:
                if (Inventory.Instance.Stone >= 6)
                    Inventory.Instance.Stone -= 6;
                break;
            case EPlayerUpgrade.IRON:
                if (Inventory.Instance.Iron >= 6)
                    Inventory.Instance.Iron -= 6;
                break;
            default:
                break;
        }

        switch (Inventory.Instance.Boots)
        {
            case EPlayerUpgrade.NONE:
                break;
            case EPlayerUpgrade.STONE:
                if (Inventory.Instance.Stone >= 2)
                    Inventory.Instance.Stone -= 2;
                break;
            case EPlayerUpgrade.IRON:
                if (Inventory.Instance.Iron >= 2)
                    Inventory.Instance.Iron -= 2;
                break;
            default:
                break;
        }
        #endregion
    }*/

    public void HealPlayer()
    {
        if (m_Health < m_MaxHealth)
        {
            if (Inventory.Instance.Coins >= 2)
            {
                m_Health = m_MaxHealth;
                Inventory.Instance.Coins -= 2;
            }
        }
    }

    public void GetDamage(int _Amount)
    {
        if (m_Armor <= 0)
        {
            m_Health -= _Amount;
        }
        else if (m_Armor > 0)
        {
            m_Armor -= _Amount;
        }

        if (m_Health <= 0)
        {
            GameManager.Instance.IsAlive = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_Armor">Armor being Upgraded</param>
    public void UpgradeArmor(ECraftingType _Armor)
    {
        switch (_Armor)
        {
            case ECraftingType.HELMET:
                switch (Inventory.Instance.Helmet)
                {
                    case EPlayerUpgrade.NONE:
                        m_Helmet = 0;
                        break;
                    case EPlayerUpgrade.STONE:
                        m_Helmet = 5;
                        break;
                    case EPlayerUpgrade.IRON:
                        m_Helmet = 10;
                        break;
                    default:
                        break;
                }
                break;
            case ECraftingType.PLATE:
                switch (Inventory.Instance.Plate)
                {
                    case EPlayerUpgrade.NONE:
                        m_Plate = 0;
                        break;
                    case EPlayerUpgrade.STONE:
                        m_Plate = 10;
                        break;
                    case EPlayerUpgrade.IRON:
                        m_Plate = 20;
                        break;
                    default:
                        break;
                }
                break;
            case ECraftingType.BOOTS:
                switch (Inventory.Instance.Boots)
                {
                    case EPlayerUpgrade.NONE:
                        m_Boots = 0;
                        break;
                    case EPlayerUpgrade.STONE:
                        m_Boots = 5;
                        break;
                    case EPlayerUpgrade.IRON:
                        m_Boots = 10;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }

        m_MaxArmor = m_Helmet + m_Plate + m_Boots;

        Armor = m_Helmet + m_Plate + m_Boots;
    }
    #endregion

}
