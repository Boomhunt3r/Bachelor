using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    #region SerzializeField
    [SerializeField]
    private float m_MovementSpeed = 5.0f;
    [SerializeField]
    private Transform m_CoinSpawn;
    [SerializeField]
    private GameObject m_CoinPrefab;
    #endregion

    #region private Variables
    private Rigidbody2D m_Rigid;

    private int m_CoinsInInventory = 5;

    private float m_Timer = 0.0f;

    private bool m_Build = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
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
                if (m_CoinsInInventory > 0)
                {
                    Instantiate(m_CoinPrefab, m_CoinSpawn.position, Quaternion.identity);

                    m_CoinsInInventory--;
                }
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (m_CoinsInInventory == 0)
                return;

            m_Timer += Time.deltaTime;

            if (m_Timer >= 2.0f)
            {
                m_CoinsInInventory--;

                m_Timer = 0.0f;

                m_Build = true;
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "Coin")
    //    {
    //        Destroy(collision.gameObject);
    //        m_CoinsInInventory++;
    //    }
    //}
}
