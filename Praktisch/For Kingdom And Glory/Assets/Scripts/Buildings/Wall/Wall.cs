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
    private EBuildingUpgrade m_Building;
    [SerializeField]
    private float m_MaxHitPoints = 25;
    [SerializeField]
    private Sprite[] m_Sprites;
    [SerializeField]
    private GameObject m_BuildUI;
    #endregion

    #region private Variables
    private GameObject m_WallObj;
    private SpriteRenderer m_Renderer;
    private int m_CoinCost  = 0;
    private int m_WoodCost  = 0;
    private int m_StoneCost = 0;
    private int m_IronCost  = 0;
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
    public bool Build { get => m_Build; set{ m_Build = value; m_BuildUI.SetActive(m_Build); } }
    public float MaxHitPoints { get => m_MaxHitPoints; set => m_MaxHitPoints = value; }
    public float CurrentHitPoints { get => m_CurrentHitPoints; set => m_CurrentHitPoints = value; }
    public EBuildingUpgrade Building { get => m_Building; set => m_Building = value; }
    public bool IsActive { get => m_isActive; set => m_isActive = value; }
    public GameObject WallObj { get => m_WallObj; set => m_WallObj = value; }
    public int CoinCost { get => m_CoinCost; set => m_CoinCost = value; }
    public int WoodCost { get => m_WoodCost; set => m_WoodCost = value; }
    public int StoneCost { get => m_StoneCost; set => m_StoneCost = value; }
    public int IronCost { get => m_IronCost; set => m_IronCost = value; }
    #endregion

    // Start is called before the first frame update
    #region Unity Functions
    void Start()
    {
        // Deactivate UI
        m_BuildUI.SetActive(false);
        // Set Resource Cost for first State
        CoinCost = 2;
        // At Start not Build so None
        m_Building = EBuildingUpgrade.NONE;
        // Get Sprite Renderer
        m_Renderer = GetComponent<SpriteRenderer>();

        Instance = this;
    }

    void Update()
    {
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
    public void UpgradeBuilding()
    {
        switch (m_Building)
        {
            // Status None
            case EBuildingUpgrade.NONE:
                // Increase Max Timer
                m_MaxTimer = 2.5f;
                // Set HP for Wall
                m_CurrentHitPoints = m_MaxHitPoints;
                // Set new Resource Cost
                CoinCost = 4;
                WoodCost = 6;
                // Set Renderer
                m_Renderer.color = Color.blue;
                // Set to next Upgrade Level
                m_Building = EBuildingUpgrade.PILE;
                break;
                // Status Pile
            case EBuildingUpgrade.PILE:
                // Increase Max Timer
                m_MaxTimer = 2.5f;
                // Set HP for Wall
                m_CurrentHitPoints = m_MaxHitPoints * 2;
                // Set new Resource Cost
                CoinCost = 6;
                WoodCost = 2;
                StoneCost = 6;
                // Set to next Upgrade Level
                m_Building = EBuildingUpgrade.WOOD;
                break;
            // Status Wood
            case EBuildingUpgrade.WOOD:
                m_MaxTimer = 7.5f;
                m_CurrentHitPoints = m_MaxHitPoints * 3;
                CoinCost = 10;
                StoneCost = 4;
                IronCost = 6;
                m_Building = EBuildingUpgrade.STONE;
                break;
            // Status Stone
            case EBuildingUpgrade.STONE:
                m_MaxTimer = 10.0f;
                m_CurrentHitPoints = m_MaxHitPoints * 4;
                m_Building = EBuildingUpgrade.IRON;
                break;
            case EBuildingUpgrade.IRON:
                break;
            default:
                break;
        }
    }

    private void DownGrade()
    {
        m_CurrentHitPoints = 0;

        m_Renderer.color = Color.red;

        Building = EBuildingUpgrade.NONE;
    }
    #endregion

    #region public functions
    public void GetDamage(int _Damage)
    {
        m_CurrentHitPoints -= _Damage;

        if(m_CurrentHitPoints <= 0)
        {
            DownGrade();
            Enemy.Instance.RemoveWallFromList();
        }
    }

    public void GetHealth(int _Health)
    {
        m_CurrentHitPoints += _Health;
    }
    #endregion

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
            Build = false;
        }
        if (collision.CompareTag("Builder"))
        {

            m_BuilderBuilding = false;
        }
    }
    #endregion

}