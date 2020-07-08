using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance { get; private set; }

    #region SerializeField
    [SerializeField]
    private GameObject m_CraftingUI;
    [SerializeField]
    private Button m_CraftSword;
    [SerializeField]
    private TextMeshProUGUI m_CraftSwordText;
    [SerializeField]
    private Button m_CraftShield;
    [SerializeField]
    private TextMeshProUGUI m_CraftShieldText;
    [SerializeField]
    private Button m_CraftHelmet;
    [SerializeField]
    private TextMeshProUGUI m_CraftHelmetText;
    [SerializeField]
    private Button m_CraftPlate;
    [SerializeField]
    private TextMeshProUGUI m_CraftPlateText;
    [SerializeField]
    private Button m_CraftBoots;
    [SerializeField]
    private TextMeshProUGUI m_CraftBootsText;
    [SerializeField]
    private Slider m_PaySlider;
    [SerializeField]
    private TextMeshProUGUI m_SliderText;
    [SerializeField]
    private TextMeshProUGUI m_NotificationText;
    #endregion

    #region private Variables
    private ECraftingType m_CraftingType;
    private bool m_IsCrafting = false;

    #region Price Variables
    private int m_SwordWoodPrice;
    private int m_SwordStonePrice;
    private int m_SwordIronPrice;

    private int m_ShieldWoodPrice;
    private int m_ShieldStonePrice;
    private int m_ShieldIronPrice;

    private int m_HelmetStonePrice;
    private int m_HelmetIronPrice;

    private int m_PlateStonePrice;
    private int m_PlateIronPrice;

    private int m_BootsStonePrice;
    private int m_BootsIronPrice;
    #endregion
    #endregion

    #region private const Variables
    private const int m_WoodPrice = 0;
    private const int m_StonePrice = 0;
    private const int m_IronPrice = 0;
    #endregion

    #region Properties
    public bool IsCrafting { get => m_IsCrafting; set => m_IsCrafting = value; }
    #endregion

    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
        m_CraftingType = ECraftingType.NONE;

        m_SwordWoodPrice  = m_WoodPrice * 3;
        m_SwordStonePrice = 0;
        m_SwordIronPrice  = 0;

        m_ShieldWoodPrice  = m_WoodPrice * 3;
        m_ShieldStonePrice = 0;
        m_ShieldIronPrice  = 0;

        m_HelmetStonePrice = m_StonePrice * 2;
        m_HelmetIronPrice = 0;

        m_PlateStonePrice = m_StonePrice * 4;
        m_PlateIronPrice = 0;

        m_BootsStonePrice = m_StonePrice * 2;
        m_BootsIronPrice = 0;

        m_CraftingUI.SetActive(false);

        m_CraftSwordText.text  = "";
        m_CraftShieldText.text = "";
        m_CraftHelmetText.text = "";
        m_CraftPlateText.text  = "";
        m_CraftBootsText.text  = "";

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsCrafting)
            return;

        switch (Inventory.Instance.Sword)
        {
            case EPlayerUpgrade.NONE:
                if (Inventory.Instance.Wood < m_SwordWoodPrice)
                {
                    m_CraftSword.GetComponent<Image>().color = Color.red;
                }
                else
                {
                    m_CraftSword.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.WOOD:
                if (Inventory.Instance.Wood < m_SwordWoodPrice && Inventory.Instance.Stone < m_SwordStonePrice)
                {
                    m_CraftSword.GetComponent<Image>().color = Color.red;
                }
                else
                {
                    m_CraftSword.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.STONE:
                if (Inventory.Instance.Wood < m_SwordWoodPrice && Inventory.Instance.Iron < m_SwordIronPrice)
                {
                    m_CraftSword.GetComponent<Image>().color = Color.red;
                }
                else
                {
                    m_CraftSword.GetComponent<Image>().color = Color.green;
                }
                break;
            default:
                break;
        }
        switch (Inventory.Instance.Shield)
        {
            case EPlayerUpgrade.NONE:
                if (Inventory.Instance.Wood < m_ShieldWoodPrice)
                {
                    m_CraftShield.GetComponent<Image>().color = Color.red;
                }
                else
                {
                    m_CraftShield.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.WOOD:
                if (Inventory.Instance.Wood < m_ShieldWoodPrice && Inventory.Instance.Stone < m_ShieldStonePrice)
                {
                    m_CraftShield.GetComponent<Image>().color = Color.red;
                }
                else
                {
                    m_CraftShield.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.STONE:
                if (Inventory.Instance.Wood < m_ShieldWoodPrice
                    && Inventory.Instance.Stone < m_ShieldStonePrice
                    && Inventory.Instance.Iron < m_ShieldIronPrice)
                {
                    m_CraftShield.GetComponent<Image>().color = Color.red;
                }
                else
                {
                    m_CraftShield.GetComponent<Image>().color = Color.green;
                }
                break;
            default:
                break;
        }
        switch (Inventory.Instance.Helmet)
        {
            case EPlayerUpgrade.NONE:
                if (Inventory.Instance.Stone < m_HelmetStonePrice)
                {
                    m_CraftHelmet.GetComponent<Image>().color = Color.red;
                }
                else
                {
                    m_CraftHelmet.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.STONE:
                if (Inventory.Instance.Iron < m_HelmetIronPrice)
                {
                    m_CraftHelmet.GetComponent<Image>().color = Color.red;
                }
                else
                {
                    m_CraftHelmet.GetComponent<Image>().color = Color.green;
                }
                break;
            default:
                break;
        }
        switch (Inventory.Instance.Plate)
        {
            case EPlayerUpgrade.NONE:
                if (Inventory.Instance.Stone < m_PlateStonePrice)
                {
                    m_CraftPlate.GetComponent<Image>().color = Color.red;
                }
                else
                {
                    m_CraftPlate.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.STONE:
                if (Inventory.Instance.Iron < m_PlateIronPrice)
                {
                    m_CraftPlate.GetComponent<Image>().color = Color.red;
                }
                else
                {
                    m_CraftPlate.GetComponent<Image>().color = Color.green;
                }
                break;
            default:
                break;
        }
        switch (Inventory.Instance.Boots)
        {
            case EPlayerUpgrade.NONE:
                if (Inventory.Instance.Iron < m_BootsIronPrice)
                {
                    m_CraftBoots.GetComponent<Image>().color = Color.red;
                }
                else
                {
                    m_CraftBoots.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.STONE:
                if (Inventory.Instance.Iron < m_BootsIronPrice)
                {
                    m_CraftBoots.GetComponent<Image>().color = Color.red;
                }
                else
                {
                    m_CraftBoots.GetComponent<Image>().color = Color.green;
                }
                break;
            default:
                break;
        }

    }
    #endregion

    #region public Function
    public void Craft()
    {
        EPlayerUpgrade NewUpgrade;

        switch (m_CraftingType)
        {
            case ECraftingType.NONE:
                m_NotificationText.text = "Nothing Selected.";
                break;
            case ECraftingType.SWORD:
                NewUpgrade = Upgrade(m_CraftingType, Inventory.Instance.Sword);
                Inventory.Instance.Sword = NewUpgrade;
                break;
            case ECraftingType.SHIELD:
                NewUpgrade = Upgrade(m_CraftingType, Inventory.Instance.Shield);
                Inventory.Instance.Shield = NewUpgrade;
                break;
            case ECraftingType.HELMET:
                NewUpgrade = Upgrade(m_CraftingType, Inventory.Instance.Helmet);
                Inventory.Instance.Helmet = NewUpgrade;
                break;
            case ECraftingType.PLATE:
                NewUpgrade = Upgrade(m_CraftingType, Inventory.Instance.Plate);
                Inventory.Instance.Plate = NewUpgrade;
                break;
            case ECraftingType.BOOTS:
                NewUpgrade = Upgrade(m_CraftingType, Inventory.Instance.Boots);
                Inventory.Instance.Boots = NewUpgrade;
                break;
            default:
                break;
        }

        m_IsCrafting = false;
        m_CraftingUI.SetActive(false);
    }
    public void Sword()
    {
        m_CraftingType = ECraftingType.SWORD;
    }
    public void Shield()
    {
        m_CraftingType = ECraftingType.SHIELD;
    }
    public void Helmet()
    {
        m_CraftingType = ECraftingType.HELMET;
    }
    public void Plate()
    {
        m_CraftingType = ECraftingType.PLATE;
    }
    public void Boots()
    {
        m_CraftingType = ECraftingType.BOOTS;
    }
    #endregion

    #region Private Functions
    private EPlayerUpgrade Upgrade(ECraftingType _Type, EPlayerUpgrade _CurrentUpgrade)
    {

        if (_Type != ECraftingType.SWORD || _Type != ECraftingType.SHIELD)
        {
            switch (_CurrentUpgrade)
            {
                case EPlayerUpgrade.NONE:
                    _CurrentUpgrade = EPlayerUpgrade.STONE;
                    break;
                case EPlayerUpgrade.STONE:
                    _CurrentUpgrade = EPlayerUpgrade.IRON;
                    break;
                default:
                    break;
            }
        }
        switch (_CurrentUpgrade)
        {
            case EPlayerUpgrade.NONE:
                _CurrentUpgrade = EPlayerUpgrade.WOOD;
                break;
            case EPlayerUpgrade.WOOD:
                _CurrentUpgrade = EPlayerUpgrade.STONE;
                break;
            case EPlayerUpgrade.STONE:
                _CurrentUpgrade = EPlayerUpgrade.IRON;
                break;
            default:
                break;
        }

        return _CurrentUpgrade;
    }
    #endregion
}
