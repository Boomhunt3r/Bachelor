﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingTest : MonoBehaviour
{
    public static BuildingTest Instance { get; private set; }

    #region private Serialize Variables
    [SerializeField]
    private int m_CurrentUpgradeLevel = 1;
    [SerializeField]
    private int m_CurrentUpgradeCost = 0;
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
    private EBuildingUpgrade m_Building;
    private bool m_Build = false;
    private bool m_Payed = false;
    #endregion

    #region private const
    /// <summary>
    /// 
    /// </summary>
    private const int m_MaxUpgrade = 3;

    /// <summary>
    /// 
    /// </summary>
    private const int m_CostPerUpgrade = 2;
    #endregion

    public bool Build { get => m_Build; set => m_Build = value; }

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
        Instance = this;
    }

    void Update()
    {
        // Open Function with Current Bool status
        ShowUI(Build);
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
                // Set to next Upgrade Level
                m_Building = EBuildingUpgrade.WOOD;

                break;
            // Status Wood
            case EBuildingUpgrade.WOOD:
                m_CurrentUpgradeCost += m_CostPerUpgrade;
                m_PaySlider.value = 0;
                m_PaySlider.maxValue = m_CurrentUpgradeCost;
                m_SliderText.text = $"0 / {m_PaySlider.maxValue}";
                m_Building = EBuildingUpgrade.STONE;

                break;
            // Status Stone
            case EBuildingUpgrade.STONE:
                m_CurrentUpgradeCost += m_CostPerUpgrade;
                m_PaySlider.value = 0;
                m_PaySlider.maxValue = m_CurrentUpgradeCost;
                m_SliderText.text = $"0 / {m_PaySlider.maxValue}";
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
        PlayerBehaviour.Instance.CoinsInInventory -= _Amount;
    }

    /// <summary>
    /// ShowUI function
    /// </summary>
    /// <param name="_Build">boolean to show or hide</param>
    private void ShowUI(bool _Build)
    {
        // If true
        if (_Build)
        {
            // Show
            m_BuildUI.SetActive(true);
        }
        // else
        else
        {
            // Hide
            m_BuildUI.SetActive(false);
            return;
        }
    }

    #region Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerBehaviour.Instance.CanBuild = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerBehaviour.Instance.CanBuild = false;
            Build = false;
        }
    }
    #endregion

    #endregion

    #region UI Functions
    public void BuildButton()
    {
        if (m_PaySlider.value == m_CurrentUpgradeCost && m_PaySlider.value <= PlayerBehaviour.Instance.CoinsInInventory)
        {
            if (m_Building == EBuildingUpgrade.IRON)
            {
                m_Text.text = "Highest Upgrade Reached";
                Build = false;
                m_PaySlider.value = 0;
                return;
            }
            m_Text.text = "";
            RemoveCoins((int)m_PaySlider.value);
            UpgradeBuilding();
            Build = false;
        }
        else if( m_PaySlider.value != m_CurrentUpgradeCost && m_Building != EBuildingUpgrade.IRON)
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