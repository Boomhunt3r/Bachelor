using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    public static Rabbit Instance { get; private set; }

    #region Serializefield
    [SerializeField]
    private float m_Health = 1.0f;
    [SerializeField]
    private float m_Speed = 5.0f;
    [SerializeField]
    [Range(1, 5)]
    private int m_CoinAmount = 1;
    [SerializeField]
    private Transform m_LocalScale;
    #endregion

    #region private Variables
    private Rigidbody2D m_Rigid;
    private GameObject m_Spawner;
    private Vector2 m_CurrentVelocity;
    private float m_IdleTimer;
    private float m_Timer = 0.0f;
    private bool m_GoingForward = true;
    #endregion

    #region Properties
    public float Health { get => m_Health; set => m_Health = value; }
    public GameObject Spawner { get => m_Spawner; set => m_Spawner = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();

        m_CurrentVelocity = Vector2.right * m_Speed * Time.deltaTime;

        m_Rigid.velocity = m_CurrentVelocity;



        m_IdleTimer = Random.Range(10.0f, 85.0f);

        Instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.Instance.IsAlive)
            return;

        if (GameManager.Instance.IsPaused)
        {
            m_Rigid.velocity = new Vector2(0, 0);
            return;
        }

        m_Timer += Time.deltaTime;

        if (m_Timer >= m_IdleTimer)
        {
            if (m_GoingForward)
            {
                m_CurrentVelocity = Vector2.right * m_Speed * Time.deltaTime;
                m_GoingForward = false;
            }
            else if (!m_GoingForward)
            {
                m_CurrentVelocity = Vector2.left * m_Speed * Time.deltaTime;
                m_GoingForward = true;
            }

            m_Timer = 0.0f;
        }

        if (m_Rigid.velocity.x >= 0.0f)
            m_LocalScale.localScale = new Vector3(-1f, 1f, 1f);
        else if (m_Rigid.velocity.x <= 0.0f)
            m_LocalScale.localScale = new Vector3(1f, 1f, 1f);


        m_Rigid.velocity = m_CurrentVelocity;
    }

    public void TakeDamage(int _Amount)
    {
        m_Health -= _Amount;

        if (m_Health <= 0)
        {
            VagrantBehaviour.Instance.RemoveRabbit();

            Inventory.Instance.Coins += m_CoinAmount;

            m_Spawner.GetComponent<RabbitSpawner>().SpawnedRabbitAmount--;

            Destroy(this.gameObject);
        }
    }
}
