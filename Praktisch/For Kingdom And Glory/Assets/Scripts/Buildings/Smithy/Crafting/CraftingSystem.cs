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
    private Button m_CraftButton;
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
    private const int m_WoodPrice = 2;
    private const int m_StonePrice = 2;
    private const int m_IronPrice = 2;
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

        m_HelmetStonePrice = m_StonePrice * 3;
        m_HelmetIronPrice  = 0;

        m_PlateStonePrice = m_StonePrice * 6;
        m_PlateIronPrice  = 0;

        m_BootsStonePrice = m_StonePrice * 2;
        m_BootsIronPrice  = 0;

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
        ShowUI(IsCrafting);

        if (!IsCrafting)
            return;

        m_CraftingUI.SetActive(true);

        switch (Inventory.Instance.Sword)
        {
            case EPlayerUpgrade.NONE:
                m_CraftSwordText.text = $"{Inventory.Instance.Wood} Wood / {m_SwordWoodPrice} Wood";
                if (Inventory.Instance.Wood < m_SwordWoodPrice)
                {
                    m_CraftSword.GetComponent<Image>().color = Color.red;
                }
                else if (Inventory.Instance.Wood >= m_SwordWoodPrice)
                {
                    m_CraftSword.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.WOOD:
                m_CraftSwordText.text = $"{Inventory.Instance.Wood} Wood  / {m_SwordWoodPrice} Wood \n" +
                                        $"{Inventory.Instance.Stone} Stone / {m_SwordStonePrice} Stone";
                if (Inventory.Instance.Wood < m_SwordWoodPrice && Inventory.Instance.Stone < m_SwordStonePrice ||
                    Inventory.Instance.Wood >= m_SwordIronPrice && Inventory.Instance.Stone < m_SwordStonePrice ||
                    Inventory.Instance.Wood < m_SwordWoodPrice && Inventory.Instance.Stone >= m_SwordStonePrice)
                {
                    m_CraftSword.GetComponent<Image>().color = Color.red;
                }
                else if (Inventory.Instance.Wood >= m_SwordWoodPrice && Inventory.Instance.Stone >= m_SwordStonePrice)
                {
                    m_CraftSword.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.STONE:
                m_CraftSwordText.text = $"{Inventory.Instance.Wood} Wood  / {m_SwordWoodPrice} Wood \n" +
                                        $"{Inventory.Instance.Iron} Stone / {m_SwordIronPrice} Stone";
                if (Inventory.Instance.Wood < m_SwordWoodPrice && Inventory.Instance.Iron < m_SwordIronPrice ||
                    Inventory.Instance.Wood >= m_SwordIronPrice && Inventory.Instance.Iron < m_SwordIronPrice ||
                    Inventory.Instance.Wood < m_SwordWoodPrice && Inventory.Instance.Iron >= m_SwordIronPrice)
                {
                    m_CraftSword.GetComponent<Image>().color = Color.red;
                }
                else if (Inventory.Instance.Wood >= m_SwordWoodPrice && Inventory.Instance.Iron >= m_SwordIronPrice)
                {
                    m_CraftSword.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.IRON:
                m_CraftSword.GetComponent<Image>().color = Color.gray;
                break;
            default:
                break;
        }

        switch (Inventory.Instance.Shield)
        {
            case EPlayerUpgrade.NONE:
                m_CraftShieldText.text = $"{Inventory.Instance.Wood} Wood / {m_ShieldWoodPrice} Wood";
                if (Inventory.Instance.Wood < m_ShieldWoodPrice)
                {
                    m_CraftShield.GetComponent<Image>().color = Color.red;
                }
                else if (Inventory.Instance.Wood >= m_ShieldWoodPrice)
                {
                    m_CraftShield.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.WOOD:
                m_CraftShieldText.text = $"{Inventory.Instance.Wood} Wood / {m_ShieldWoodPrice} Wood \n " +
                                         $"{Inventory.Instance.Stone} Stone / {m_ShieldStonePrice} Stone";
                if (Inventory.Instance.Wood < m_ShieldWoodPrice && Inventory.Instance.Stone < m_ShieldStonePrice ||
                    Inventory.Instance.Wood >= m_ShieldWoodPrice && Inventory.Instance.Stone < m_ShieldStonePrice ||
                    Inventory.Instance.Wood < m_ShieldWoodPrice && Inventory.Instance.Stone >= m_ShieldStonePrice)
                {
                    m_CraftShield.GetComponent<Image>().color = Color.red;
                }
                else if (Inventory.Instance.Wood >= m_ShieldWoodPrice && Inventory.Instance.Stone >= m_ShieldStonePrice)
                {
                    m_CraftShield.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.STONE:
                m_CraftShieldText.text = $"{Inventory.Instance.Wood} Wood / {m_ShieldWoodPrice} \n" +
                    $"{Inventory.Instance.Stone} Stone / {m_ShieldStonePrice} Stone \n" +
                    $"{Inventory.Instance.Iron} Iron / {m_ShieldIronPrice} Iron";
                if (Inventory.Instance.Wood < m_ShieldWoodPrice && Inventory.Instance.Stone < m_ShieldStonePrice && Inventory.Instance.Iron < m_ShieldIronPrice ||
                    Inventory.Instance.Wood >= m_ShieldWoodPrice && Inventory.Instance.Stone < m_ShieldStonePrice && Inventory.Instance.Iron < m_ShieldIronPrice ||
                    Inventory.Instance.Wood < m_ShieldWoodPrice && Inventory.Instance.Stone >= m_ShieldStonePrice && Inventory.Instance.Iron < m_ShieldIronPrice ||
                    Inventory.Instance.Wood < m_ShieldWoodPrice && Inventory.Instance.Stone < m_ShieldStonePrice && Inventory.Instance.Iron >= m_ShieldIronPrice ||
                    Inventory.Instance.Wood >= m_ShieldWoodPrice && Inventory.Instance.Stone >= m_ShieldStonePrice && Inventory.Instance.Iron < m_ShieldIronPrice ||
                    Inventory.Instance.Wood >= m_ShieldWoodPrice && Inventory.Instance.Stone < m_ShieldStonePrice && Inventory.Instance.Iron >= m_ShieldIronPrice ||
                    Inventory.Instance.Wood < m_ShieldWoodPrice && Inventory.Instance.Stone >= m_ShieldStonePrice && Inventory.Instance.Iron >= m_ShieldIronPrice)
                {
                    m_CraftShield.GetComponent<Image>().color = Color.red;
                }
                else if (Inventory.Instance.Wood >= m_ShieldWoodPrice && Inventory.Instance.Stone >= m_ShieldStonePrice && Inventory.Instance.Iron >= m_ShieldIronPrice)
                {
                    m_CraftShield.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.IRON:
                m_CraftShield.GetComponent<Image>().color = Color.gray;
                break;
            default:
                break;
        }

        switch (Inventory.Instance.Helmet)
        {
            case EPlayerUpgrade.NONE:
                m_CraftHelmetText.text = $"{Inventory.Instance.Stone} Stone / {m_HelmetStonePrice} Stone";
                if (Inventory.Instance.Stone < m_HelmetStonePrice)
                {
                    m_CraftHelmet.GetComponent<Image>().color = Color.red;
                }
                else if(Inventory.Instance.Stone >= m_HelmetStonePrice)
                {
                    m_CraftHelmet.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.STONE:
                m_CraftHelmetText.text = $"{Inventory.Instance.Iron} Iron / {m_HelmetIronPrice} Iron";
                if (Inventory.Instance.Iron < m_HelmetIronPrice)
                {
                    m_CraftHelmet.GetComponent<Image>().color = Color.red;
                }
                else if(Inventory.Instance.Iron >= m_HelmetIronPrice)
                {
                    m_CraftHelmet.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.IRON:
                m_CraftHelmet.GetComponent<Image>().color = Color.gray;
                break;
            default:
                break;
        }

        switch (Inventory.Instance.Plate)
        {
            case EPlayerUpgrade.NONE:
                m_CraftPlateText.text = $"{Inventory.Instance.Stone} Stone / {m_PlateStonePrice} Stone";
                if (Inventory.Instance.Stone < m_PlateStonePrice)
                {
                    m_CraftPlate.GetComponent<Image>().color = Color.red;
                }
                else if(Inventory.Instance.Stone >= m_PlateStonePrice)
                {
                    m_CraftPlate.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.STONE:
                m_CraftPlateText.text = $"{Inventory.Instance.Iron} Iron / {m_PlateIronPrice} Iron";
                if (Inventory.Instance.Iron < m_PlateIronPrice)
                {
                    m_CraftPlate.GetComponent<Image>().color = Color.red;
                }
                else if(Inventory.Instance.Iron >= m_PlateIronPrice)
                {
                    m_CraftPlate.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.IRON:
                m_CraftPlate.GetComponent<Image>().color = Color.gray;
                break;
            default:
                break;
        }

        switch (Inventory.Instance.Boots)
        {
            case EPlayerUpgrade.NONE:
                m_CraftBootsText.text = $"{Inventory.Instance.Stone} Stone / {m_BootsStonePrice} Stone";
                if (Inventory.Instance.Stone < m_BootsStonePrice)
                {
                    m_CraftBoots.GetComponent<Image>().color = Color.red;
                }
                else if(Inventory.Instance.Stone >= m_BootsStonePrice)
                {
                    m_CraftBoots.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.STONE:
                m_CraftBootsText.text = $"{Inventory.Instance.Iron} Iron / {m_BootsIronPrice} Iron";
                if (Inventory.Instance.Iron < m_BootsIronPrice)
                {
                    m_CraftBoots.GetComponent<Image>().color = Color.red;
                }
                else if(Inventory.Instance.Iron >= m_BootsIronPrice)
                {
                    m_CraftBoots.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.IRON:
                m_CraftBoots.GetComponent<Image>().color = Color.gray;
                break;
            default:
                break;
        }

    }
    #endregion

    #region public Function
    public void Sword()
    {
        m_CraftingType = ECraftingType.SWORD;
        CraftItem();
    }
    public void Shield()
    {
        m_CraftingType = ECraftingType.SHIELD;
        CraftItem();
    }
    public void Helmet()
    {
        m_CraftingType = ECraftingType.HELMET;
        CraftItem();
    }
    public void Plate()
    {
        m_CraftingType = ECraftingType.PLATE;
        CraftItem();
    }
    public void Boots()
    {
        m_CraftingType = ECraftingType.BOOTS;
        CraftItem();
    }
    #endregion

    #region Private Functions
    /// <summary>
    /// 
    /// </summary>
    private void CraftItem()
    {
        EPlayerUpgrade NewUpgrade;

        m_NotificationText.text = "";

        switch (m_CraftingType)
        {
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
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_Type">Current Type being Crafted</param>
    /// <param name="_CurrentUpgrade">Current Upgrade Level</param>
    /// <returns>New Upgrade Status</returns>
    private EPlayerUpgrade Upgrade(ECraftingType _Type, EPlayerUpgrade _CurrentUpgrade)
    {
        if (_Type == ECraftingType.HELMET || _Type == ECraftingType.PLATE || _Type == ECraftingType.BOOTS)
        {
            switch (_CurrentUpgrade)
            {
                case EPlayerUpgrade.NONE:
                    if (_Type == ECraftingType.HELMET)
                    {
                        if (Inventory.Instance.Stone >= m_HelmetStonePrice)
                        {
                            Inventory.Instance.Stone -= m_HelmetStonePrice;

                            m_HelmetIronPrice = m_IronPrice * 3;

                            m_NotificationText.text = "";

                            _CurrentUpgrade = EPlayerUpgrade.STONE;
                        }
                        else if (Inventory.Instance.Stone < m_HelmetStonePrice)
                        {
                            m_NotificationText.text = "Not enough Resources.";
                        }

                    }
                    else if (_Type == ECraftingType.PLATE)
                    {
                        if (Inventory.Instance.Stone >= m_PlateStonePrice)
                        {
                            Inventory.Instance.Stone -= m_PlateStonePrice;

                            m_PlateIronPrice = m_IronPrice * 6;

                            m_NotificationText.text = "";

                            _CurrentUpgrade = EPlayerUpgrade.STONE;
                        }
                        else if (Inventory.Instance.Stone < m_PlateStonePrice)
                        {
                            m_NotificationText.text = "Not enough Resources.";
                        }
                    }
                    else if (_Type == ECraftingType.BOOTS)
                    {
                        if (Inventory.Instance.Stone >= m_BootsStonePrice)
                        {
                            Inventory.Instance.Stone -= m_BootsStonePrice;

                            m_BootsIronPrice = m_IronPrice * 2;

                            _CurrentUpgrade = EPlayerUpgrade.STONE;
                        }
                        else if (Inventory.Instance.Stone <= m_BootsStonePrice)
                        {
                            m_NotificationText.text = "Not enough Resources.";
                        }
                    }
                    break;
                case EPlayerUpgrade.STONE:
                    if (_Type == ECraftingType.HELMET)
                    {
                        if (Inventory.Instance.Iron >= m_HelmetIronPrice)
                        {
                            Inventory.Instance.Iron -= m_HelmetIronPrice;

                            m_NotificationText.text = "";
                            m_CraftHelmetText.text = "Max upgrade reached.";

                            _CurrentUpgrade = EPlayerUpgrade.IRON;
                        }
                        else if (Inventory.Instance.Iron < m_HelmetIronPrice)
                        {
                            m_NotificationText.text = "Not enough Resources.";
                        }
                    }
                    else if (_Type == ECraftingType.PLATE)
                    {
                        if (Inventory.Instance.Iron >= m_PlateIronPrice)
                        {
                            Inventory.Instance.Iron -= m_HelmetIronPrice;

                            m_NotificationText.text = "";
                            m_CraftPlateText.text = "Max upgrade reached.";

                            _CurrentUpgrade = EPlayerUpgrade.IRON;
                        }
                        else if (Inventory.Instance.Iron < m_PlateIronPrice)
                        {
                            m_NotificationText.text = "Not enough Resources.";
                        }
                    }
                    else if (_Type == ECraftingType.BOOTS)
                    {
                        if (Inventory.Instance.Iron >= m_BootsIronPrice)
                        {
                            Inventory.Instance.Iron -= m_BootsIronPrice;

                            m_NotificationText.text = "";
                            m_CraftBootsText.text = "Max upgrade reached.";

                            _CurrentUpgrade = EPlayerUpgrade.IRON;
                        }
                        else if(Inventory.Instance.Iron < m_BootsIronPrice)
                        {
                            m_NotificationText.text = "Not enough Resources.";
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        else if (_Type == ECraftingType.SWORD || _Type == ECraftingType.SHIELD)
        {
            switch (_CurrentUpgrade)
            {
                case EPlayerUpgrade.NONE:
                    if (_Type == ECraftingType.SWORD)
                    {
                        if (Inventory.Instance.Wood >= m_SwordWoodPrice)
                        {
                            Inventory.Instance.Wood -= m_SwordWoodPrice;

                            m_SwordWoodPrice = m_WoodPrice * 2;
                            m_SwordStonePrice = m_StonePrice * 3;

                            m_NotificationText.text = "";

                            _CurrentUpgrade = EPlayerUpgrade.WOOD;
                        }
                        else if (Inventory.Instance.Wood < m_SwordWoodPrice)
                        {
                            m_NotificationText.text = "Not Enough Resources.";
                        }

                    }
                    else if (_Type == ECraftingType.SHIELD)
                    {
                        if (Inventory.Instance.Wood >= m_ShieldWoodPrice)
                        {
                            Inventory.Instance.Wood -= m_ShieldWoodPrice;

                            m_ShieldWoodPrice = m_WoodPrice * 2;
                            m_ShieldStonePrice = m_StonePrice * 3;

                            _CurrentUpgrade = EPlayerUpgrade.WOOD;

                            m_NotificationText.text = "";
                        }
                        else if (Inventory.Instance.Wood < m_ShieldWoodPrice)
                        {
                            m_NotificationText.text = "Not Enough Resources.";
                        }
                    }
                    break;
                case EPlayerUpgrade.WOOD:
                    if (_Type == ECraftingType.SWORD)
                    {
                        if (Inventory.Instance.Wood >= m_SwordWoodPrice && Inventory.Instance.Stone >= m_SwordStonePrice)
                        {
                            Inventory.Instance.Wood -= m_SwordWoodPrice;
                            Inventory.Instance.Stone -= m_SwordStonePrice;

                            m_SwordWoodPrice = m_WoodPrice * 2;
                            m_SwordIronPrice = m_IronPrice * 3;

                            m_NotificationText.text = "";

                            _CurrentUpgrade = EPlayerUpgrade.STONE;
                        }
                        else if (Inventory.Instance.Wood < m_SwordWoodPrice || Inventory.Instance.Stone < m_SwordStonePrice)
                        {
                            m_NotificationText.text = "Not Enough Resources.";
                        }
                    }
                    else if (_Type == ECraftingType.SHIELD)
                    {
                        if (Inventory.Instance.Wood >= m_ShieldWoodPrice && Inventory.Instance.Stone >= m_ShieldStonePrice)
                        {
                            Inventory.Instance.Wood -= m_ShieldWoodPrice;
                            Inventory.Instance.Stone -= m_ShieldStonePrice;

                            m_ShieldWoodPrice = m_WoodPrice;
                            m_ShieldStonePrice = m_StonePrice * 2;
                            m_ShieldIronPrice = m_IronPrice * 3;

                            _CurrentUpgrade = EPlayerUpgrade.STONE;
                        }
                        else if (Inventory.Instance.Wood < m_ShieldWoodPrice || Inventory.Instance.Stone < m_ShieldStonePrice)
                        {
                            m_NotificationText.text = "Not Enough Resources.";
                        }
                    }
                    break;
                case EPlayerUpgrade.STONE:
                    if (_Type == ECraftingType.SWORD)
                    {
                        if (Inventory.Instance.Wood >= m_SwordWoodPrice && Inventory.Instance.Iron >= m_SwordIronPrice)
                        {
                            Inventory.Instance.Wood -= m_SwordWoodPrice;
                            Inventory.Instance.Iron -= m_SwordIronPrice;

                            m_NotificationText.text = "";
                            m_CraftSwordText.text = "Max upgrade reached.";

                            _CurrentUpgrade = EPlayerUpgrade.IRON;
                        }
                        else if (Inventory.Instance.Wood < m_SwordWoodPrice || Inventory.Instance.Iron < m_SwordIronPrice)
                        {
                            m_NotificationText.text = "Not Enough Resources.";
                        }
                    }
                    else if (_Type == ECraftingType.SHIELD)
                    {
                        if (Inventory.Instance.Wood >= m_ShieldWoodPrice && Inventory.Instance.Stone >= m_ShieldStonePrice && Inventory.Instance.Iron >= m_ShieldIronPrice)
                        {
                            Inventory.Instance.Wood -= m_ShieldWoodPrice;
                            Inventory.Instance.Stone -= m_ShieldStonePrice;
                            Inventory.Instance.Iron -= m_ShieldIronPrice;

                            m_CraftShieldText.text = "Max upgrade reached.";

                            _CurrentUpgrade = EPlayerUpgrade.IRON;
                        }
                        else if (Inventory.Instance.Wood < m_ShieldWoodPrice)
                        {
                            m_NotificationText.text = "Not Enough Resources.";
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        return _CurrentUpgrade;
    }

    private void ShowUI(bool _Show)
    {
        m_CraftingUI.SetActive(_Show);
    }
    #endregion

    #region private Collision Function
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!IsCrafting)
                PlayerBehaviour.Instance.CanCraft = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (IsCrafting)
            {
                PlayerBehaviour.Instance.CanCraft = false;
                IsCrafting = false;
            }
        }
    }
    #endregion
}
