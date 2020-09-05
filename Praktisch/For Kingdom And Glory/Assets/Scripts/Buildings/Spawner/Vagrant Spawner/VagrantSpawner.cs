using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VagrantSpawner : MonoBehaviour
{
    #region Serializefield
    [SerializeField]
    private int m_VagrantToSpawn = 2;

    [SerializeField]
    [Range(0, 720)]
    private float m_SpawnTime = 60.0f;

    [SerializeField]
    private GameObject m_VagrantPrefab;
    #endregion

    #region private Virables
    private float m_Timer = 0.0f;
    private bool m_CanSpawn = true;
    private GameObject m_Vagrant;
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsAlive)
            return;

        if (GameManager.Instance.IsPaused)
            return;

        m_Timer += Time.deltaTime;
        if (m_Timer >= m_SpawnTime && m_CanSpawn)
        {
            for (int i = 0; i < m_VagrantToSpawn; i++)
            {
                m_Vagrant = Instantiate(m_VagrantPrefab, new Vector3(this.gameObject.transform.position.x, 0.0f, 0.15f), Quaternion.identity);
                GameManager.Instance.SpawnedVagrants.Add(m_Vagrant);
            }
            m_Timer = 0.0f;
        }
        if (!m_CanSpawn)
            m_Timer = 0.0f;
    }

    #region private Trigger collision Function
    private void OnTriggerEnter2D(Collider2D _Collision)
    {
        if (_Collision.CompareTag("Vagrant"))
        {
            m_CanSpawn = false;
        }
    }
    private void OnTriggerStay2D(Collider2D _Collision)
    {
        if (_Collision.CompareTag("Vagrant"))
        {
            m_CanSpawn = false;
        }
    }
    private void OnTriggerExit2D(Collider2D _Collision)
    {
        if (_Collision.CompareTag("Vagrant"))
        {
            m_CanSpawn = true;
        }
    }
    #endregion
}
