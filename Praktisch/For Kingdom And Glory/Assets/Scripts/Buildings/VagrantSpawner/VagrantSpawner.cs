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
    private float m_SpawnTime = 120.0f;

    [SerializeField]
    private GameObject m_VagrantPrefab;
    #endregion

    #region private Virables
    private float m_Timer = 0.0f;
    private bool m_CanSpawn = true;
    #endregion
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_SpawnTime && m_CanSpawn)
        {
            for (int i = 0; i < m_VagrantToSpawn; i++)
            {
                Instantiate(m_VagrantPrefab, new Vector3(this.gameObject.transform.position.x,0.0f,this.transform.position.z), Quaternion.identity);
                //Debug.Log("Spawned");
            }
            m_Timer = 0.0f;
        }
        if (!m_CanSpawn)
            m_Timer = 0.0f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Vagrant"))
        {
            m_CanSpawn = false;
            //Debug.Log("drin");
        }
    }
}
