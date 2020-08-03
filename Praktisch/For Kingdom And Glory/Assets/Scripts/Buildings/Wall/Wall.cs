using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Wall : MonoBehaviour
{
    public static Wall Instance { get; private set; }

    #region private Serialize Variables
    [SerializeField]
    private EBuildingUpgrade m_Building;
    [SerializeField]
    private int m_CurrentUpgradeCost = 0;
    [SerializeField]
    private float m_MaxHitPoints = 50;
    [SerializeField]
    private Sprite[] m_Sprites;
    [SerializeField]
    private GameObject m_BuildUI;
    [SerializeField]
    private Slider m_PaySlider;
    [SerializeField]
    private TextMeshProUGUI m_SliderText;
    [SerializeField]
    private TextMeshProUGUI m_Text;
    #endregion

    #region private Variables
    private SpriteRenderer m_Renderer;
    private float m_Timer = 0.0f;
    private float m_MaxTimer = 1.5f;
    [SerializeField]
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
    public bool Build { get => m_Build; set => m_Build = value; }
    public float MaxHitPoints { get => m_MaxHitPoints; set => m_MaxHitPoints = value; }
    public float CurrentHitPoints { get => m_CurrentHitPoints; set => m_CurrentHitPoints = value; }
    public EBuildingUpgrade Building { get => m_Building; set => m_Building = value; }
    public bool IsActive { get => m_isActive; set => m_isActive = value; }
    #endregion

    // Start is called before the first frame update
    #region Unity Functions
    void Start()
    {
        // Set Current Cost to Cost per Upgrade
        m_CurrentUpgradeCost = m_CostPerUpgrade;
        // Deactivate UI
        m_BuildUI.SetActive(false);
        // Slider maxValue = UpgradeCost
        m_PaySlider.maxValue = m_CurrentUpgradeCost;
        // At Start not Build so None
        m_Building = EBuildingUpgrade.NONE;
        // Set Slider text
        m_SliderText.text = $"0 / {m_PaySlider.maxValue}";
        // Get Sprite Renderer
        m_Renderer = GetComponent<SpriteRenderer>();

        Instance = this;
    }

    void Update()
    {
        // Open Function with Current Bool status
        ShowUI(Build);

        if (!IsActive)
            return;

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
    private void UpgradeBuilding()
    {
        switch (m_Building)
        {
            // Status None
            case EBuildingUpgrade.NONE:
                // Increase Upgrade Cost
                m_CurrentUpgradeCost += m_CostPerUpgrade;
                // Reset Slider Handle to 0
                m_PaySlider.value = 0;
                // Increase Max Value
                m_PaySlider.maxValue = m_CurrentUpgradeCost;
                // Update Slider Text to new MaxValue
                m_SliderText.text = $"0 / {m_PaySlider.maxValue}";
                // Increase Max Timer
                m_MaxTimer = 2.5f;
                // Set HP for Wall
                m_CurrentHitPoints = m_MaxHitPoints;
                // Set to next Upgrade Level
                m_Building = EBuildingUpgrade.WOOD;
                // Set Renderer
                m_Renderer.color = Color.blue;
                // Set Active True
                IsActive = true;
                break;
            // Status Wood
            case EBuildingUpgrade.WOOD:
                m_CurrentUpgradeCost += m_CostPerUpgrade;
                m_PaySlider.value = 0;
                m_PaySlider.maxValue = m_CurrentUpgradeCost;
                m_SliderText.text = $"0 / {m_PaySlider.maxValue}";
                m_MaxTimer = 7.5f;
                m_CurrentHitPoints = m_MaxHitPoints * 2;
                m_MaxHitPoints = m_MaxHitPoints * 2;
                m_Building = EBuildingUpgrade.STONE;

                break;
            // Status Stone
            case EBuildingUpgrade.STONE:
                m_CurrentUpgradeCost += m_CostPerUpgrade;
                m_PaySlider.value = 0;
                m_PaySlider.maxValue = m_CurrentUpgradeCost;
                m_SliderText.text = $"0 / {m_PaySlider.maxValue}";
                m_MaxTimer = 10.0f;
                m_CurrentHitPoints = m_MaxHitPoints * 3;
                m_MaxHitPoints = m_MaxHitPoints * 3;
                m_Building = EBuildingUpgrade.IRON;

                break;
            case EBuildingUpgrade.IRON:
                m_PaySlider.value = 0;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Remove Coins Function
    /// </summary>
    /// <param name="_Amount">Amount Removed from Inventory</param>
    private void RemoveCoins(int _Amount)
    {
        Inventory.Instance.Coins -= _Amount;
    }

    /// <summary>
    /// ShowUI function
    /// </summary>
    /// <param name="_Build">boolean to show or hide</param>
    private void ShowUI(bool _Build)
    {
        m_BuildUI.SetActive(_Build);
    }

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
            m_PaySlider.value = 0;
            Build = false;
        }
        if (collision.CompareTag("Builder"))
        {

            m_BuilderBuilding = false;
        }
    }
    #endregion

    #endregion

    #region public functions
    public void GetDamage(int _Damage)
    {
        m_CurrentHitPoints -= _Damage;

        if(m_CurrentHitPoints <= 0)
        {
            this.gameObject.SetActive(false);
            Enemy.Instance.RemoveWallFromList();
        }
    }

    public void GetHealth(int _Health)
    {
        m_CurrentHitPoints += _Health;
    }
    #endregion

    #region UI Functions
    public void BuildButton()
    {
        if (m_PaySlider.value == m_CurrentUpgradeCost && m_PaySlider.value <= Inventory.Instance.Coins)
        {
            if (m_Building == EBuildingUpgrade.IRON)
            {
                m_Text.text = "Highest Upgrade Reached";
                Build = false;
                m_PaySlider.value = 0;
                return;
            }
            m_Text.text = "";
            m_BeingBuild = true;
            RemoveCoins((int)m_PaySlider.value);
            UpgradeBuilding();
            Build = false;
        }
        else if (m_PaySlider.value != m_CurrentUpgradeCost && m_Building != EBuildingUpgrade.IRON || Inventory.Instance.Coins <= m_PaySlider.value)
        {
            m_Payed = false;
            m_Text.text = "Not enough Coins!";
        }
    }
    public void UpdateText()
    {
        m_SliderText.text = $"{m_PaySlider.value} / {m_PaySlider.maxValue}";
    }

    public void ExitButton()
    {
        Build = false;
    }
    #endregion
}