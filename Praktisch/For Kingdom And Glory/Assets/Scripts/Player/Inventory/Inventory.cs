using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    #region Serializefield
    [SerializeField]
    private int m_Coins;
    [SerializeField]
    private TextMeshProUGUI m_CoinsText;
    [SerializeField]
    private int m_Wood;
    [SerializeField]
    private TextMeshProUGUI m_WoodText;
    [SerializeField]
    private int m_Stone;
    [SerializeField]
    private TextMeshProUGUI m_StoneText;
    [SerializeField]
    private int m_Iron;
    [SerializeField]
    private TextMeshProUGUI m_IronText;
    [SerializeField]
    private GameObject m_UI;
    #endregion

    #region private Variables
    [SerializeField]
    private EPlayerUpgrade m_Bow;
    private EPlayerUpgrade m_Helmet;
    private EPlayerUpgrade m_Plate;
    private EPlayerUpgrade m_Boots;
    private bool m_ShowUI = true;
    #endregion

    #region Properties
    /// <summary>
    /// Amount of Coins in Inventory
    /// </summary>
    public int Coins { get => m_Coins; set => m_Coins = value; }
    /// <summary>
    /// Amount of Wood in Inventory
    /// </summary>
    public int Wood { get => m_Wood; set => m_Wood = value; }
    /// <summary>
    /// Amount of Stone in Inventory
    /// </summary>
    public int Stone { get => m_Stone; set => m_Stone = value; }
    /// <summary>
    /// Amount of Iron in Inventory
    /// </summary>
    public int Iron { get => m_Iron; set => m_Iron = value; }
    /// <summary>
    /// 
    /// </summary>
    public EPlayerUpgrade Bow { get => m_Bow; set => m_Bow = value; }
    /// <summary>
    /// Player Helmet
    /// </summary>
    public EPlayerUpgrade Helmet { get => m_Helmet; set => m_Helmet = value; }
    /// <summary>
    /// Player Plate
    /// </summary>
    public EPlayerUpgrade Plate { get => m_Plate; set => m_Plate = value; }
    /// <summary>
    /// Player Boots
    /// </summary>
    public EPlayerUpgrade Boots { get => m_Boots; set => m_Boots = value; }
    /// <summary>
    /// 
    /// </summary>
    public bool ShowUI { get => m_ShowUI; set => m_ShowUI = value; }
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Set Everything at the beginning to None
        Bow = EPlayerUpgrade.NONE;
        Helmet = EPlayerUpgrade.NONE;
        Plate = EPlayerUpgrade.NONE;
        Boots = EPlayerUpgrade.NONE;

        Instance = this;
    }

    void Update()
    {
        m_UI.SetActive(ShowUI);

        if (!GameManager.Instance.IsAlive)
            return;

        UpdateUI();
    }

    private void UpdateUI()
    {
        m_CoinsText.text = $"{Coins}";
        m_WoodText.text  = $"{Wood}";
        m_StoneText.text = $"{Stone}";
        m_IronText.text  = $"{Iron}";

    }
}
