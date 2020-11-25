using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class VagrantBehaviour : MonoBehaviour
{
    private List<GameObject> m_EnemySpawner = new List<GameObject>();
    private GameObject m_EnemyToShoot;
    private GameObject m_CurrentRabbit;
    private bool m_IsDefending = false;
    private bool m_IsAttacking = false;

    public bool IsAttacking { get => m_IsAttacking; set => m_IsAttacking = value; }
    public List<GameObject> EnemySpawner { get => m_EnemySpawner; set => m_EnemySpawner = value; }

    private void Archer()
    {
        this.gameObject.tag = "Archer";

        m_Rabbits = GameObject.FindGameObjectsWithTag("Rabbit").ToList();

        if (GameManager.Instance.IsDay)
        {
            if (!IsAttacking)
            {
                if (m_Rabbits.Count != 0)
                {
                    m_Target = GetClosestTarget(m_Rabbits);
                    m_CurrentRabbit = m_Target;

                    m_ShootTime += Time.deltaTime;

                    m_Distance = Vector2.Distance(m_Rigid.position, m_Target.transform.position);

                    if (m_Distance <= Random.Range(1.5f, 4.5f))
                    {
                        m_Rigid.velocity = new Vector2(0, 0);

                        if (m_ShootTime >= m_ShootTimer)
                        {
                            Shoot(m_Target);
                        }
                    }
                }

                if (m_Rabbits.Count == 0)
                {
                    m_Target = m_Waypoints[Random.Range(0, m_Waypoints.Length)];
                    m_Hunting = false;
                }
            }

            if (IsAttacking)
            {
                if (m_EnemySpawner.Count != 0)
                    m_Target = GetClosestTarget(m_EnemySpawner);

                if (m_Distance <= Random.Range(3f, 7.5f))
                {
                    if (m_ShootTime >= m_ShootTimer)
                    {
                        ChangeAnimation("Attack", false);
                        Shoot(m_Target);
                    }
                }
            }
        }

        if (GameManager.Instance.AlmostNight)
        {
            m_Hunting = false;
            m_IsAttacking = false;

            if (m_DefendingWall != null)
                m_Target = m_DefendingWall;

            m_Distance = Vector2.Distance(m_Rigid.position, m_Target.transform.position);

            if (m_Distance <= Random.Range(1.0f, 8.5f))
            {
                m_Rigid.velocity = new Vector2(0, 0);
                m_IsDefending = true;
            }

            return;
        }

        if (GameManager.Instance.IsNight)
        {
            if (!m_IsDefending)
            {
                m_Distance = Vector2.Distance(m_Rigid.position, m_Target.transform.position);

                if (m_Distance <= Random.Range(1.0f, 2.5f))
                {
                    m_Rigid.velocity = new Vector2(0, 0);
                    m_IsDefending = true;
                }

                return;
            }

            if (m_IsDefending)
            {
                if (GameManager.Instance.AllSpawnedEnemys.Count == 0)
                    return;

                m_EnemyToShoot = GetClosestTarget(GameManager.Instance.AllSpawnedEnemys);

                m_ShootTime += Time.deltaTime;

                m_Distance = Vector2.Distance(m_Rigid.position, m_EnemyToShoot.transform.position);

                if (m_Distance <= 7.5f)
                {
                    if (m_ShootTime >= m_ShootTimer)
                    {
                        Shoot(m_EnemyToShoot);
                        m_Source.Play();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Closest Target Function
    /// </summary>
    /// <param name="_Target">All Targets</param>
    /// <returns>Closest Target</returns>
    private GameObject GetClosestTarget(List<GameObject> _Target)
    {
        GameObject Target = null;
        float MinDist = Mathf.Infinity;
        Vector2 CurrentPos = transform.position;
        float Dist = 0.0f;

        for (int i = 0; i < _Target.Count; i++)
        {
            if (_Target[i] == null)
                continue;

            Dist = Vector2.Distance(_Target[i].transform.position, CurrentPos);

            if (Dist < MinDist)
            {
                Target = _Target[i];
                MinDist = Dist;
            }
        }

        return Target;
    }

    /// <summary>
    /// Shoot Function
    /// </summary>
    /// <param name="_Target">Target to Shoot at</param>
    private void Shoot(GameObject _Target)
    {
        m_ShootTime = 0.0f;

        float XDistance;
        XDistance = Random.Range(_Target.transform.position.x - m_ThrowPoint.position.x, m_Direction.x * 5.0f);

        float YDistance;
        YDistance = Random.Range(_Target.transform.position.y - m_ThrowPoint.position.y, 5.0f);

        float ThrowAngle;
        ThrowAngle = Mathf.Atan((YDistance + 4.905f) / XDistance);

        float TotalVelo = XDistance / Mathf.Cos(ThrowAngle);

        float XVelo;
        float YVelo;
        XVelo = TotalVelo * Mathf.Cos(ThrowAngle);
        YVelo = TotalVelo * Mathf.Sin(ThrowAngle);

        GameObject Arrow = Instantiate(m_Arrow, m_ThrowPoint.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(XVelo, YVelo);
    }

    /// <summary>
    /// Remove Rabbit Function
    /// </summary>
    public void RemoveRabbit(GameObject _Rabbit)
    {
        for (int i = 0; i < m_Rabbits.Count; i++)
        {
            if (_Rabbit == m_Rabbits[i])
                m_Rabbits.Remove(_Rabbit);
        }
    }

    /// <summary>
    /// Remove Spawner Function
    /// </summary>
    /// <param name="_Spawner">Spawner to Remove</param>
    public void RemoveSpawner(GameObject _Spawner)
    {
        m_EnemySpawner.Remove(_Spawner);
    }
}
