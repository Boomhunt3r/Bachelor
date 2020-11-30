using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance { get; private set; }

    #region SerializeField
    [Header("UI Settings")]
    [SerializeField]
    private GameObject m_CraftingUI;
    [SerializeField]
    private TextMeshProUGUI m_NotificationText;
    [Header("Bow")]
    [SerializeField]
    private Button m_CraftBow;
    [SerializeField]
    private TextMeshProUGUI m_CraftBowText;
    [Header("Helmet")]
    [SerializeField]
    private Button m_CraftHelmet;
    [SerializeField]
    private TextMeshProUGUI m_CraftHelmetText;
    [Header("Plate")]
    [SerializeField]
    private Button m_CraftPlate;
    [SerializeField]
    private TextMeshProUGUI m_CraftPlateText;
    [Header("Boots")]
    [SerializeField]
    private Button m_CraftBoots;
    [SerializeField]
    private TextMeshProUGUI m_CraftBootsText;
    [Header("Audio Settings")]
    [SerializeField]
    private AudioSource m_Source;
    [SerializeField]
    private AudioClip m_BowClip;
    [SerializeField]
    private AudioClip m_Smith;
    #endregion

    #region private Variables
    private ECraftingType m_CraftingType;
    private bool m_IsCrafting = false;

    #region Price Variables
    private int m_BowWoodPrice;

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
    public AudioSource Source { get => m_Source; set => m_Source = value; }
    #endregion

    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
        m_CraftingType = ECraftingType.NONE;

        m_BowWoodPrice = m_WoodPrice * 2;

        m_HelmetStonePrice = m_StonePrice * 3;
        m_HelmetIronPrice = 0;

        m_PlateStonePrice = m_StonePrice * 6;
        m_PlateIronPrice = 0;

        m_BootsStonePrice = m_StonePrice * 2;
        m_BootsIronPrice = 0;

        m_CraftingUI.SetActive(false);

        m_CraftBowText.text = "";
        m_CraftHelmetText.text = "";
        m_CraftPlateText.text = "";
        m_CraftBootsText.text = "";

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        ShowUI(IsCrafting);

        if (!IsCrafting)
            return;

        switch (Inventory.Instance.Bow)
        {
            case EPlayerUpgrade.NONE:
                m_CraftBowText.text = $"{Inventory.Instance.Wood} Wood / {m_BowWoodPrice} Wood";
                if (Inventory.Instance.Wood < m_BowWoodPrice)
                {
                    m_CraftBow.GetComponent<Image>().color = Color.red;
                }
                else if (Inventory.Instance.Wood >= m_BowWoodPrice)
                {
                    m_CraftBow.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.WOOD:
                m_CraftBowText.text = $"{Inventory.Instance.Wood} Wood  / {m_BowWoodPrice} Wood";
                if (Inventory.Instance.Wood < m_BowWoodPrice)
                {
                    m_CraftBow.GetComponent<Image>().color = Color.red;
                }
                else if (Inventory.Instance.Wood >= m_BowWoodPrice)
                {
                    m_CraftBow.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.STONE:
                m_CraftBowText.text = $"{Inventory.Instance.Wood} Wood  / {m_BowWoodPrice} Wood";
                if (Inventory.Instance.Wood < m_BowWoodPrice)
                {
                    m_CraftBow.GetComponent<Image>().color = Color.red;
                }
                else if (Inventory.Instance.Wood >= m_BowWoodPrice)
                {
                    m_CraftBow.GetComponent<Image>().color = Color.green;
                }
                break;
            case EPlayerUpgrade.IRON:
                m_CraftBow.GetComponent<Image>().color = Color.gray;
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
                else if (Inventory.Instance.Stone >= m_HelmetStonePrice)
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
                else if (Inventory.Instance.Iron >= m_HelmetIronPrice)
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
                else if (Inventory.Instance.Stone >= m_PlateStonePrice)
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
                else if (Inventory.Instance.Iron >= m_PlateIronPrice)
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
                else if (Inventory.Instance.Stone >= m_BootsStonePrice)
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
                else if (Inventory.Instance.Iron >= m_BootsIronPrice)
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
    public void Bow()
    {
        m_CraftingType = ECraftingType.BOW;
        m_Source.clip = m_BowClip;
        m_Source.Play();
        CraftItem();
    }
    public void Helmet()
    {
        m_CraftingType = ECraftingType.HELMET;
        m_Source.clip = m_Smith;
        m_Source.Play();
        CraftItem();
    }
    public void Plate()
    {
        m_CraftingType = ECraftingType.PLATE;
        m_Source.clip = m_Smith;
        m_Source.Play();
        CraftItem();
    }
    public void Boots()
    {
        m_CraftingType = ECraftingType.BOOTS;
        m_Source.clip = m_Smith;
        m_Source.Play();
        CraftItem();
    }
    public void Repair()
    {
        //PlayerBehaviour.Instance.RepairArmor();
    }
    public void Exit()
    {
        IsCrafting = false;
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
            case ECraftingType.BOW:
                NewUpgrade = Upgrade(m_CraftingType, Inventory.Instance.Bow);
                Inventory.Instance.Bow = NewUpgrade;
                break;
            case ECraftingType.HELMET:
                NewUpgrade = Upgrade(m_CraftingType, Inventory.Instance.Helmet);
                Inventory.Instance.Helmet = NewUpgrade;
                PlayerBehaviour.Instance.UpgradeArmor(m_CraftingType);
                break;
            case ECraftingType.PLATE:
                NewUpgrade = Upgrade(m_CraftingType, Inventory.Instance.Plate);
                Inventory.Instance.Plate = NewUpgrade;
                PlayerBehaviour.Instance.UpgradeArmor(m_CraftingType);
                break;
            case ECraftingType.BOOTS:
                NewUpgrade = Upgrade(m_CraftingType, Inventory.Instance.Boots);
                Inventory.Instance.Boots = NewUpgrade;
                PlayerBehaviour.Instance.UpgradeArmor(m_CraftingType);
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
                        else if (Inventory.Instance.Iron < m_BootsIronPrice)
                        {
                            m_NotificationText.text = "Not enough Resources.";
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        else if (_Type == ECraftingType.BOW)
        {
            switch (_CurrentUpgrade)
            {
                case EPlayerUpgrade.NONE:
                    if (_Type == ECraftingType.BOW)
                    {
                        if (Inventory.Instance.Wood >= m_BowWoodPrice)
                        {
                            Inventory.Instance.Wood -= m_BowWoodPrice;

                            m_BowWoodPrice = m_WoodPrice * 6;

                            m_NotificationText.text = "";

                            _CurrentUpgrade = EPlayerUpgrade.WOOD;
                        }
                        else if (Inventory.Instance.Wood < m_BowWoodPrice)
                        {
                            m_NotificationText.text = "Not Enough Resources.";
                        }

                    }
                    break;
                case EPlayerUpgrade.WOOD:
                    if (_Type == ECraftingType.BOW)
                    {
                        if (Inventory.Instance.Wood >= m_BowWoodPrice)
                        {
                            Inventory.Instance.Wood -= m_BowWoodPrice;
                            PlayerBehaviour.Instance.ShootTimer -= 1.5f;

                            m_BowWoodPrice = m_WoodPrice * 8;

                            m_NotificationText.text = "";

                            _CurrentUpgrade = EPlayerUpgrade.STONE;
                        }
                        else if (Inventory.Instance.Wood < m_BowWoodPrice)
                        {
                            m_NotificationText.text = "Not Enough Resources.";
                        }
                    }
                    break;
                case EPlayerUpgrade.STONE:
                    if (_Type == ECraftingType.BOW)
                    {
                        if (Inventory.Instance.Wood >= m_BowWoodPrice)
                        {
                            Inventory.Instance.Wood -= m_BowWoodPrice;
                            PlayerBehaviour.Instance.ShootTimer -= 1.5f;

                            m_NotificationText.text = "";
                            m_CraftBowText.text = "Max upgrade reached.";

                            _CurrentUpgrade = EPlayerUpgrade.IRON;
                        }
                        else if (Inventory.Instance.Wood < m_BowWoodPrice)
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
            else
            {
                PlayerBehaviour.Instance.CanCraft = false;
                IsCrafting = false;
            }
        }
    }
    #endregion
}
