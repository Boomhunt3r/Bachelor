using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour Instance { get; private set; }

    #region SerzializeField
    [SerializeField]
    private float m_MovementSpeed = 5.0f;
    [SerializeField]
    [Range(50, 200)]
    private int m_Health = 50;
    [SerializeField]
    private Transform m_CoinSpawnRight;
    [SerializeField]
    private Transform m_CoinSpawnLeft;
    [SerializeField]
    private GameObject m_CoinPrefab;
    #endregion

    #region private Variables
    private Rigidbody2D m_Rigid;

    private bool m_Build = false;

    private bool m_CanBuild = false;

    private bool m_IsBuilding = false;

    private bool m_CanBuyBows = false;

    private bool m_IsBuing = false;

    private bool m_CanCraft = false;

    private bool m_IsCrafting = false;

    private bool m_CanTown = false;

    private bool m_IsTown = false;
    #endregion

    #region Properties
    public bool CanBuild { get => m_CanBuild; set => m_CanBuild = value; }
    public bool CanBuyBows { get => m_CanBuyBows; set => m_CanBuyBows = value; }
    public bool CanCraft { get => m_CanCraft; set => m_CanCraft = value; }
    public bool CanTown { get => m_CanTown; set => m_CanTown = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = Input.GetAxis("Horizontal") * Vector2.right * m_MovementSpeed * Time.deltaTime;
        m_Rigid.velocity = dir;

        if (Input.GetKeyUp(KeyCode.S))
        {
            if (m_Build == false)
            {
                if (Inventory.Instance.Coins > 0)
                {
                    if (dir.x < 0)
                        Instantiate(m_CoinPrefab, m_CoinSpawnRight.position, Quaternion.identity);
                    if (dir.x > 0)
                        Instantiate(m_CoinPrefab, m_CoinSpawnLeft.position, Quaternion.identity);
                    if (dir.x == 0)
                        Instantiate(m_CoinPrefab, m_CoinSpawnRight.position, Quaternion.identity);

                    Inventory.Instance.Coins--;
                }
            }
        }

        if (CanBuild || CanBuyBows || CanCraft || CanTown)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (CanBuild)
                {
                    if (m_IsBuilding == false)
                    {
                        Wall.Instance.Build = true;
                        m_IsBuilding = true;
                    }
                    else if (m_IsBuilding == true)
                    {
                        Wall.Instance.Build = false;
                        m_IsBuilding = false;
                    }
                }
                else if (CanBuyBows)
                {
                    if (m_IsBuing == false)
                    {
                        Archery.Instance.Buy = true;
                        m_IsBuing = true;
                    }
                    else if (m_IsBuing == true)
                    {
                        Archery.Instance.Buy = false;
                        m_IsBuing = false;
                    }
                }
                else if (CanCraft)
                {
                    if (m_IsCrafting == false)
                    {
                        CraftingSystem.Instance.IsCrafting = true;
                        m_IsCrafting = true;
                    }
                    else if (m_IsCrafting == true)
                    {
                        CraftingSystem.Instance.IsCrafting = false;
                        m_IsCrafting = false;
                    }
                }
                else if(CanTown)
                {
                    if (m_IsTown == false)
                    {
                        TownBuilding.Instance.IsActive = true;
                        m_IsTown = true;
                    }
                    else if (m_IsTown == true)
                    {
                        TownBuilding.Instance.IsActive = false;
                        m_IsTown = false;
                    }
                }
            }
        }
    }

    #region Collision Function
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            VagrantBehaviour.Instance.RemoveCoin();
            Destroy(collision.gameObject);
            Inventory.Instance.Coins++;
        }
    }
    #endregion

    public void GetDamage(int _Amount)
    {
        m_Health -= _Amount;

        if(m_Health >= 0)
        {
            GameManager.Instance.IsAlive = false;
        }
    }

}
