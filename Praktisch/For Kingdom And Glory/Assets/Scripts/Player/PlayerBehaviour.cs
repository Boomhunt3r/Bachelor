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

    private int m_CoinsInInventory = 50;

    private bool m_Build = false;

    private bool m_CanBuild = false;
    #endregion

    public int CoinsInInventory { get => m_CoinsInInventory; set => m_CoinsInInventory = value; }
    public bool CanBuild { get => m_CanBuild; set => m_CanBuild = value; }

    // Start is called before the first frame update
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = Input.GetAxis("Horizontal") * Vector2.right * m_MovementSpeed;
        m_Rigid.velocity = dir;

        if (Input.GetKeyUp(KeyCode.S))
        {
            if (m_Build == false)
            {
                if (CoinsInInventory > 0)
                {
                    if (dir.x < 0)
                        Instantiate(m_CoinPrefab, m_CoinSpawnRight.position, Quaternion.identity);
                    if (dir.x > 0)
                        Instantiate(m_CoinPrefab, m_CoinSpawnLeft.position, Quaternion.identity);

                    if (dir.x == 0)
                        Instantiate(m_CoinPrefab, m_CoinSpawnRight.position, Quaternion.identity);

                    CoinsInInventory--;
                }
            }
        }

        if (CanBuild)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                BuildingTest.Instance.Build = true;
            }
        }
        Debug.Log(CoinsInInventory);
        //Debug.Log(CanBuild);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            m_CoinsInInventory++;
        }
    }
}
