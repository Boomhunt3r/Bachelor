using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    [SerializeField]
    [Range(5, 20)]
    private int m_Damage = 5;

    private float m_Timer = 0.0f;
    private void Update()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer >= 2.5f)
            Destroy(this.gameObject);
    }

    #region Collision Function
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            collision.gameObject.GetComponent<Wall>().GetDamage(m_Damage);

            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("Player"))
        {

        }
        else if (collision.CompareTag("Villager"))
        {
            collision.GetComponent<VagrantBehaviour>().StepDown();

            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("Archer"))
        {
            collision.GetComponent<VagrantBehaviour>().StepDown();

            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("Builder"))
        {
            collision.GetComponent<VagrantBehaviour>().StepDown();

            Destroy(this.gameObject);
        }
    }
    #endregion
}
