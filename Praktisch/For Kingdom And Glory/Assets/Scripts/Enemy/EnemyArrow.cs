using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    [SerializeField]
    [Range(5, 20)]
    private int m_Damage = 5;

    private Rigidbody2D m_Rigid;
    private float m_Timer = 0.0f;
    private bool m_Hit = false;

    private void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer >= 2.5f)
            Destroy(this.gameObject);

        transform.up = m_Rigid.velocity;
    }

    #region Collision Function
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            if (collision.gameObject.GetComponent<Wall>().Building == EBuildingUpgrade.NONE)
            {
                Destroy(this.gameObject);
                return;
            }

            if (!m_Hit)
            {
                collision.gameObject.GetComponent<Wall>().GetDamage(m_Damage);
                m_Hit = true;
                Destroy(this.gameObject);
            }

        }
        else if (collision.CompareTag("Player"))
        {
            if (!m_Hit)
            {
                PlayerBehaviour.Instance.GetDamage(m_Damage);
                Destroy(this.gameObject);
                m_Hit = true;
            }

        }
        else if (collision.CompareTag("Villager"))
        {
            if (!m_Hit)
            {
                collision.GetComponent<VagrantBehaviour>().StepDown();
                Destroy(this.gameObject);
                m_Hit = true;
            }

        }
        else if (collision.CompareTag("Archer"))
        {
            if (!m_Hit)
            {
                collision.GetComponent<VagrantBehaviour>().StepDown();
                Destroy(this.gameObject);
                m_Hit = true;
            }

        }
        else if (collision.CompareTag("Builder"))
        {
            if (!m_Hit)
            {
                collision.GetComponent<VagrantBehaviour>().StepDown();
                Destroy(this.gameObject);
                m_Hit = true;
            }

        }
    }
    #endregion
}
