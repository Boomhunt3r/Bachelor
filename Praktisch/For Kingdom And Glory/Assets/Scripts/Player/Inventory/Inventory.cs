using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    #region private Variables
    [SerializeField]
    private int m_Coins;
    [SerializeField]
    private int m_Wood;
    [SerializeField]
    private int m_Stone;
    [SerializeField]
    private int m_Iron;
    private EPlayerUpgrade m_Sword;
    private EPlayerUpgrade m_Shield;
    private EPlayerUpgrade m_Helmet;
    private EPlayerUpgrade m_Plate;
    private EPlayerUpgrade m_Boots;
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
    /// Player Sword
    /// </summary>
    public EPlayerUpgrade Sword { get => m_Sword; set => m_Sword = value; }
    /// <summary>
    /// Player Shield
    /// </summary>
    public EPlayerUpgrade Shield { get => m_Shield; set => m_Shield = value; }
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
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Set Everything at the beginning to None
        m_Sword  = EPlayerUpgrade.NONE;
        m_Shield = EPlayerUpgrade.NONE;
        m_Helmet = EPlayerUpgrade.NONE;
        m_Plate  = EPlayerUpgrade.NONE;
        m_Boots  = EPlayerUpgrade.NONE;

        Instance = this;       
    }
}
