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

    private bool m_CanBuyBows = false;

    private bool m_CanCraft = false;

    private bool m_IsCrafting = false;
    #endregion

    #region Properties
    public bool CanBuild { get => m_CanBuild; set => m_CanBuild = value; }
    public bool CanBuyBows { get => m_CanBuyBows; set => m_CanBuyBows = value; }
    public bool CanCraft { get => m_CanCraft; set => m_CanCraft = value; }
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

        if (CanBuild || CanBuyBows || CanCraft)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (CanBuild)
                    Wall.Instance.Build = true;
                else if (CanBuyBows)
                    Archery.Instance.Buy = true;
                else if (CanCraft)
                {
                    if (m_IsCrafting == false)
                    {
                        CraftingSystem.Instance.IsCrafting = true;
                        m_IsCrafting = true;
                    }
                    else if(m_IsCrafting == true)
                    {
                        CraftingSystem.Instance.IsCrafting = false;
                        m_IsCrafting = false;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            Inventory.Instance.Coins++;
        }
    }
}
