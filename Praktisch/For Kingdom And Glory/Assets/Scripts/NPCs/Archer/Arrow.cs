using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    [Range(1, 10)]
    private int m_Damage = 1;
    [SerializeField]
    private AudioSource m_Source;
    [SerializeField]
    private GameObject m_Arrow;

    private float m_Timer = 0.0f;
    private GameObject m_Parent;
    private Rigidbody2D m_Rigid;

    public GameObject Parent { get => m_Parent; set => m_Parent = value; }

    private void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        m_Timer += Time.deltaTime;

        Vector2 dir = m_Rigid.velocity;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        m_Arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (m_Timer >= 5.0f)
            Destroy(this.gameObject);
    }

    #region Collision Function
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(m_Damage);
            m_Source.Play();
            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("Rabbit"))
        {
            collision.gameObject.GetComponent<Rabbit>().TakeDamage(m_Damage, m_Parent);
            m_Source.Play();
            Destroy(this.gameObject);
        }
        else if(collision.CompareTag("EnemySpawner"))
        {
            collision.gameObject.GetComponent<EnemySpawner>().ReceiveDamage(m_Damage);
            m_Source.Play();
            Destroy(this.gameObject);
        }
        if (collision.CompareTag("Ground"))
        {
            m_Source.Play();
            Destroy(this.gameObject);
        }
    }
    #endregion
}
