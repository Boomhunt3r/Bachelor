using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    [Range(1, 10)]
    private int m_Damage = 1;
    [SerializeField]
    private AudioSource m_Source;

    private float m_Timer = 0.0f;
    private void Update()
    {
        m_Timer += Time.deltaTime;

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
            collision.gameObject.GetComponent<Rabbit>().TakeDamage(m_Damage);
            m_Source.Play();
            Destroy(this.gameObject);
        }
        else if(collision.CompareTag("EnemySpawner"))
        {
            collision.gameObject.GetComponent<EnemySpawner>().ReceiveDamage(m_Damage);
            m_Source.Play();
            Destroy(this.gameObject);
        }
        else if(collision.CompareTag("Wall"))
        {
            m_Source.Play();
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            m_Source.Play();
            Destroy(this.gameObject);
        }
    }
    #endregion
}
