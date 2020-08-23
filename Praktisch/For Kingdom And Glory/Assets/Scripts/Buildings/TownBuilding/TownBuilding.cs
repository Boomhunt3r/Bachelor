using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TownBuilding : MonoBehaviour
{
    public static TownBuilding Instance { get; private set; }

    #region SerializeField
    [Header("Ressource Settings")]
    [SerializeField]
    private int m_CoinsPerDay = 1;
    [SerializeField]
    private int m_WoodPerDay = 1;
    [SerializeField]
    private int m_StonePerDay = 1;
    [SerializeField]
    private int m_IronPerDay = 1;
    [SerializeField]
    private TextMeshProUGUI m_RessourceText;

    [Header("Building Upgrades")]
    [SerializeField]
    private Sprite[] m_Sprites;
    [SerializeField]
    private EBuildingUpgrade m_Upgrade;
    [SerializeField]
    private GameObject m_TownUI;

    [Header("Pay Settings")]
    [SerializeField]
    private TextMeshProUGUI m_PayText;
    [SerializeField]
    private Image m_Coin;
    [SerializeField]
    private Image m_Wood;
    [SerializeField]
    private Image m_Stone;
    [SerializeField]
    private Image m_Iron;

    [Header("Attack Buttons")]
    [SerializeField]
    private Button m_LeftAttack;
    [SerializeField]
    private Button m_RightAttack;

    [Header("Timer")]
    [SerializeField]
    private float m_Timer = 240.0f;

    [Header("Audio")]
    [SerializeField]
    private AudioClip m_BuildSound;
    [SerializeField]
    private AudioClip m_AttackSound;
    #endregion

    #region private Variables
    private SpriteRenderer m_Renderer;
    private float m_Time = 0.0f;
    private bool m_IsActive = false;
    private bool m_AttackIsUnderWay = false;
    #endregion

    #region private const
    private const int m_Ressources = 2;
    #endregion

    #region Properties
    public bool IsActive { get => m_IsActive; set => m_IsActive = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        switch (GameManager.Instance.Setting)
        {
            case EGameSetting.EASY:
                m_CoinsPerDay = 5;
                m_WoodPerDay = 5;
                m_StonePerDay = 5;
                m_IronPerDay = 5;
                break;
            case EGameSetting.MEDUIM:
                m_CoinsPerDay = 3;
                m_WoodPerDay = 3;
                m_StonePerDay = 3;
                m_IronPerDay = 3;
                break;
            case EGameSetting.HARD:
                m_CoinsPerDay = 1;
                m_WoodPerDay = 1;
                m_StonePerDay = 1;
                m_IronPerDay = 1;
                break;
            default:
                break;
        }
        m_Upgrade = EBuildingUpgrade.NONE;

        m_Renderer = GetComponentInChildren<SpriteRenderer>();

        m_RessourceText.text = "Daily Ressources: \n" +
                               $"{m_CoinsPerDay} Coins \n" +
                               $"{m_WoodPerDay} Wood \n" +
                               $"0 Stone \n" +
                               $"0 Iron \n";

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsAlive)
            return;

        ShowUI(IsActive);

        switch (m_Upgrade)
        {
            case EBuildingUpgrade.NONE:
                m_PayText.text = $"{Inventory.Instance.Coins} Coins / 6 Coins \n" +
                                 $"{Inventory.Instance.Wood} Wood / 8 Wood";

                if (m_Time >= m_Timer)
                {
                    Inventory.Instance.Coins += m_CoinsPerDay;
                    Inventory.Instance.Wood  += m_WoodPerDay; 

                    m_Time = 0.0f;
                }

                break;
            case EBuildingUpgrade.WOOD:
                m_PayText.text = $"{Inventory.Instance.Coins} Coins / 8 Coins \n" +
                                 $"{Inventory.Instance.Stone} Stone / 10 Stone";

                if (m_Time >= m_Timer)
                {
                    Inventory.Instance.Coins += m_CoinsPerDay;
                    Inventory.Instance.Wood  += m_WoodPerDay;
                    Inventory.Instance.Stone += m_StonePerDay;

                    m_Time = 0.0f;
                }

                break;
            case EBuildingUpgrade.STONE:
                m_PayText.text = $"{Inventory.Instance.Coins} Coins / 10 Coins \n" +
                                 $"{Inventory.Instance.Iron} Iron / 14 Iron";

                if (m_Time >= m_Timer)
                {
                    Inventory.Instance.Coins += m_CoinsPerDay;
                    Inventory.Instance.Wood += m_WoodPerDay;
                    Inventory.Instance.Stone += m_StonePerDay;
                    Inventory.Instance.Iron += m_IronPerDay;

                    m_Time = 0.0f;
                }

                break;
            case EBuildingUpgrade.IRON:
                m_PayText.text = "";

                if (m_Time >= m_Timer)
                {
                    Inventory.Instance.Coins += m_CoinsPerDay;
                    Inventory.Instance.Wood  += m_WoodPerDay;
                    Inventory.Instance.Stone += m_StonePerDay;
                    Inventory.Instance.Iron  += m_IronPerDay;

                    m_Time = 0.0f;
                }

                break;
            default:
                break;
        }
    }

    #region UI Functions
    private void ShowUI(bool _Show)
    {
        m_TownUI.SetActive(_Show);
    }

    public void UpgradeBuilding()
    {
        switch (m_Upgrade)
        {
            case EBuildingUpgrade.NONE:
                if (Inventory.Instance.Coins >= 6 && Inventory.Instance.Wood >= 8)
                {
                    m_Renderer.sprite = m_Sprites[0];

                    Inventory.Instance.Coins -= 6;
                    Inventory.Instance.Wood -= 8;

                    m_CoinsPerDay += m_Ressources;
                    m_WoodPerDay  += 1;
                    m_StonePerDay  = m_Ressources;

                    m_RessourceText.text = "Daily Ressources: \n" +
                               $"{m_CoinsPerDay} Coins \n" +
                               $"{m_WoodPerDay} Wood \n" +
                               $"{m_StonePerDay} Stone \n" +
                               $"0 Iron \n";
                    m_Upgrade = EBuildingUpgrade.WOOD;
                    PlayerBehaviour.Instance.EffectSource.clip = m_BuildSound;
                    PlayerBehaviour.Instance.EffectSource.Play();
                }
                break;
            case EBuildingUpgrade.WOOD:
                if (Inventory.Instance.Coins >= 8 && Inventory.Instance.Stone >= 10)
                {
                    //m_Renderer.sprite = m_Sprites[1];

                    Inventory.Instance.Coins -= 8;
                    Inventory.Instance.Stone -= 10;

                    m_CoinsPerDay += m_Ressources;
                    m_WoodPerDay  += m_Ressources;
                    m_StonePerDay  = m_Ressources + 1;
                    m_IronPerDay   = m_Ressources;

                    m_RessourceText.text = "Daily Ressources: \n" +
                               $"{m_CoinsPerDay} Coins \n" +
                               $"{m_WoodPerDay} Wood \n" +
                               $"{m_StonePerDay} Stone \n" +
                               $"{m_IronPerDay} Iron \n";

                    m_Timer /= 2;

                    m_Upgrade = EBuildingUpgrade.STONE;
                    PlayerBehaviour.Instance.EffectSource.clip = m_BuildSound;
                    PlayerBehaviour.Instance.EffectSource.Play();
                }
                break;
            case EBuildingUpgrade.STONE:
                if (Inventory.Instance.Coins >= 10 && Inventory.Instance.Iron >= 14)
                {
                    //m_Renderer.sprite = m_Sprites[2];

                    Inventory.Instance.Coins -= 10;
                    Inventory.Instance.Iron  -= 14;

                    m_StonePerDay += m_Ressources;
                    m_IronPerDay  += m_Ressources;

                    m_RessourceText.text = "Daily Ressources: \n" +
                               $"{m_CoinsPerDay} Coins \n" +
                               $"{m_WoodPerDay} Wood \n" +
                               $"{m_StonePerDay} Stone \n" +
                               $"{m_IronPerDay} Iron \n";

                    m_Upgrade = EBuildingUpgrade.IRON;
                    PlayerBehaviour.Instance.EffectSource.clip = m_BuildSound;
                    PlayerBehaviour.Instance.EffectSource.Play();
                }
                break;
            default:
                break;
        }
    }

    public void LeftAttack()
    {
        ArcherManager.Instance.Attack(ESpawnerSide.LEFT);
        PlayerBehaviour.Instance.EffectSource.clip = m_AttackSound;
        PlayerBehaviour.Instance.EffectSource.Play();
    }

    public void RightAttack()
    {
        ArcherManager.Instance.Attack(ESpawnerSide.RIGHT);
        PlayerBehaviour.Instance.EffectSource.clip = m_AttackSound;
        PlayerBehaviour.Instance.EffectSource.Play();
    }
    #endregion

    #region Collision Function
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!IsActive)
            {
                PlayerBehaviour.Instance.CanTown = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (IsActive)
            {
                PlayerBehaviour.Instance.CanTown = false;
                IsActive = false;
            }
            else
            {
                PlayerBehaviour.Instance.CanTown = false;
                IsActive = false;
            }
        }
    }
    #endregion
}
