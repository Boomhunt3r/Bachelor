﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Wall : MonoBehaviour
{
    public static Wall Instance { get; private set; }

    #region private Serialize Variables
    [SerializeField]
    private int m_CurrentUpgradeLevel = 1;
    [SerializeField]
    private int m_CurrentUpgradeCost = 0;
    [SerializeField]
    private float m_MaxHitPoints = 5;
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
    private float m_Timer = 0.0f;
    private float m_MaxTimer = 1.5f;
    private bool m_Build = false;
    private bool m_Payed = false;
    private bool m_BeingBuild = false;
    private bool m_BuilderBuilding = false;
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
    public float MaxHitPoints { get => m_MaxHitPoints; set => m_MaxHitPoints = value; }

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
                // Set to next Upgrade Level
                m_Building = EBuildingUpgrade.WOOD;

                break;
            // Status Wood
            case EBuildingUpgrade.WOOD:
                m_CurrentUpgradeCost += m_CostPerUpgrade;
                m_PaySlider.value = 0;
                m_PaySlider.maxValue = m_CurrentUpgradeCost;
                m_SliderText.text = $"0 / {m_PaySlider.maxValue}";
                m_MaxTimer = 7.5f;
                m_Building = EBuildingUpgrade.STONE;

                break;
            // Status Stone
            case EBuildingUpgrade.STONE:
                m_CurrentUpgradeCost += m_CostPerUpgrade;
                m_PaySlider.value = 0;
                m_PaySlider.maxValue = m_CurrentUpgradeCost;
                m_SliderText.text = $"0 / {m_PaySlider.maxValue}";
                m_MaxTimer = 10.0f;
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
        }
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
            m_BeingBuild = true;
            RemoveCoins((int)m_PaySlider.value);
            UpgradeBuilding();
            Build = false;
        }
        else if (m_PaySlider.value != m_CurrentUpgradeCost && m_Building != EBuildingUpgrade.IRON)
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