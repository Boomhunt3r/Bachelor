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
    private Slider m_PaySlider;
    [SerializeField]
    private TextMeshProUGUI m_SliderText;
    [SerializeField]
    private TextMeshProUGUI m_NotificationText;
    #endregion

    #region private Variables
    private ECraftingType m_Crafting;
    #endregion

    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
        m_Crafting = ECraftingType.NONE;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region public Function
    public void Craft()
    {
        switch (m_Crafting)
        {
            case ECraftingType.NONE:
                m_NotificationText.text = "Nothing Selected.";
                break;
            case ECraftingType.SWORD:
                switch (Inventory.Instance.Sword)
                {
                    case EPlayerUpgrade.NONE:
                        break;
                    case EPlayerUpgrade.WOOD:
                        break;
                    case EPlayerUpgrade.STONE:
                        break;
                    case EPlayerUpgrade.IRON:
                        break;
                    default:
                        break;
                }
                break;
            case ECraftingType.SHIELD:
                switch (Inventory.Instance.Shield)
                {
                    case EPlayerUpgrade.NONE:
                        break;
                    case EPlayerUpgrade.WOOD:
                        break;
                    case EPlayerUpgrade.STONE:
                        break;
                    case EPlayerUpgrade.IRON:
                        break;
                    default:
                        break;
                }
                break;
            case ECraftingType.HELMET:
                switch (Inventory.Instance.Helmet)
                {
                    case EPlayerUpgrade.NONE:
                        break;
                    case EPlayerUpgrade.WOOD:
                        break;
                    case EPlayerUpgrade.STONE:
                        break;
                    case EPlayerUpgrade.IRON:
                        break;
                    default:
                        break;
                }
                break;
            case ECraftingType.PLATE:
                switch (Inventory.Instance.Plate)
                {
                    case EPlayerUpgrade.NONE:
                        break;
                    case EPlayerUpgrade.WOOD:
                        break;
                    case EPlayerUpgrade.STONE:
                        break;
                    case EPlayerUpgrade.IRON:
                        break;
                    default:
                        break;
                }
                break;
            case ECraftingType.BOOTS:
                switch (Inventory.Instance.Boots)
                {
                    case EPlayerUpgrade.NONE:
                        break;
                    case EPlayerUpgrade.WOOD:
                        break;
                    case EPlayerUpgrade.STONE:
                        break;
                    case EPlayerUpgrade.IRON:
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    } 
    public void Sword()
    {
        m_Crafting = ECraftingType.SWORD;
    }
    public void Shield()
    {
        m_Crafting = ECraftingType.SHIELD;
    }
    public void Helmet()
    {
        m_Crafting = ECraftingType.HELMET;
    }
    public void Plate()
    {
        m_Crafting = ECraftingType.PLATE;
    }
    public void Boots()
    {
        m_Crafting = ECraftingType.BOOTS;
    }
    #endregion
}
