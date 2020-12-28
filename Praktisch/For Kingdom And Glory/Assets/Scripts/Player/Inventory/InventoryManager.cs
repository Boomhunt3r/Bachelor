using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    #region SerializeField
    [SerializeField]
    private GameObject m_Inventory;
    [SerializeField]
    private Image m_HelmetImage;
    [SerializeField]
    private Image m_PlateImage;
    [SerializeField]
    private Image m_BootsImage;
    [SerializeField]
    private Image m_WeaponImage;
    [SerializeField]
    private Sprite[] m_HelmetImages;
    [SerializeField]
    private Sprite[] m_PlateImages;
    [SerializeField]
    private Sprite[] m_BootsImages;
    [SerializeField]
    private Sprite[] m_WeaponImages;
    #endregion

    #region private Variables
    private bool m_Active = false;
    #endregion

    #region Properties
    public bool Active { get => m_Active; set => m_Active = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        m_Inventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        m_Inventory.SetActive(m_Active);

        if (m_Active)
            UpdateInv();
    }

    private void UpdateInv()
    {
        switch (Inventory.Instance.Helmet)
        {
            case EPlayerUpgrade.NONE:
                m_HelmetImage.color = new Color(0.0f, 0.0f, 0f, 0f);
                break;
            case EPlayerUpgrade.STONE:
                m_HelmetImage.color = Color.white;
                m_HelmetImage.sprite = m_HelmetImages[0];
                break;
            case EPlayerUpgrade.IRON:
                m_HelmetImage.color = Color.white;
                m_HelmetImage.sprite = m_HelmetImages[1];
                break;
            default:
                break;
        }

        switch (Inventory.Instance.Plate)
        {
            case EPlayerUpgrade.NONE:
                m_PlateImage.color = new Color(0.0f, 0.0f, 0f, 0f);
                break;
            case EPlayerUpgrade.STONE:
                m_PlateImage.color = Color.white;
                m_PlateImage.sprite = m_PlateImages[0];
                break;
            case EPlayerUpgrade.IRON:
                m_PlateImage.color = Color.white;
                m_PlateImage.sprite = m_PlateImages[1];
                break;
            default:
                break;
        }

        switch (Inventory.Instance.Boots)
        {
            case EPlayerUpgrade.NONE:
                m_BootsImage.color = new Color(0.0f, 0.0f, 0f, 0f);
                break;
            case EPlayerUpgrade.STONE:
                m_BootsImage.color = Color.white;
                m_BootsImage.sprite = m_BootsImages[0];
                break;
            case EPlayerUpgrade.IRON:
                m_BootsImage.color = Color.white;
                m_BootsImage.sprite = m_BootsImages[1];
                break;
            default:
                break;
        }

        switch (Inventory.Instance.Bow)
        {
            case EPlayerUpgrade.NONE:
                m_WeaponImage.color = new Color(0.0f, 0.0f, 0f, 0f);
                break;
            case EPlayerUpgrade.WOOD:
                m_WeaponImage.color = Color.white;
                m_WeaponImage.sprite = m_WeaponImages[0];
                break;
            case EPlayerUpgrade.STONE:
                m_WeaponImage.color = Color.white;
                m_WeaponImage.sprite = m_WeaponImages[1];
                break;
            case EPlayerUpgrade.IRON:
                m_WeaponImage.color = Color.white;
                m_WeaponImage.sprite = m_WeaponImages[2];
                break;
            default:
                break;
        }
    }
}
