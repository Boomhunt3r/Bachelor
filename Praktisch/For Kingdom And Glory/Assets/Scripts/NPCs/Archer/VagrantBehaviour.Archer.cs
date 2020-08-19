using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class VagrantBehaviour : MonoBehaviour
{
    private List<GameObject> m_EnemySpawner = new List<GameObject>();
    private GameObject m_CurrentRabbit;
    private GameObject m_CurrentSpawner;
    private bool m_IsDefending = false;
    private bool m_IsAttacking = false;

    public bool IsAttacking { get => m_IsAttacking; set => m_IsAttacking = value; }

    private void Archer()
    {
        m_Render.color = Color.red;
        this.gameObject.tag = "Archer";

        m_Rabbits = GameObject.FindGameObjectsWithTag("Rabbit").OfType<GameObject>().ToList();

        if (GameManager.Instance.IsDay)
        {
            if (!IsAttacking)
            {
                if (m_Rabbits.Count != 0)
                {
                    if (!m_Hunting)
                    {
                        m_Target = GetClosestTarget(m_Rabbits);
                        m_CurrentRabbit = m_Target;
                    }

                    else if (m_Hunting)
                    {
                        m_ShootTime += Time.deltaTime;

                        m_Distance = Vector2.Distance(m_Rigid.position, m_Target.transform.position);

                        if (m_Distance <= Random.Range(1.5f, 4.5f))
                        {
                            m_Rigid.velocity = new Vector2(0, 0);
                        }

                        if (m_Rigid.velocity.x == 0)
                        {
                            if (m_ShootTime >= m_ShootTimer)
                            {
                                Shoot();
                            }
                        }
                    }
                }
            }

            else if (IsAttacking)
            {
                if (m_EnemySpawner.Count == 0)
                    m_EnemySpawner = GameObject.FindGameObjectsWithTag("EnemySpawner").OfType<GameObject>().ToList();

                m_Target = GetClosestTarget(m_EnemySpawner);

                if(m_Distance <= Random.Range(1.5f, 4.5f))
                {
                    m_Rigid.velocity = new Vector2(0, 0);
                }

                if(m_Rigid.velocity.x == 0)
                {
                    if(m_ShootTime >= m_ShootTimer)
                    {
                        Shoot();
                    }
                }
            }
        }

        else if (GameManager.Instance.IsNight)
        {
            m_Hunting = false;

            if (!m_IsDefending)
            {
                m_Target = GetClosestWall(m_BuildWalls);
                m_CurrentSpawner = m_Target;

                m_Distance = Vector2.Distance(m_Rigid.position, m_Target.transform.position);

                if (m_Distance <= Random.Range(2.5f, 3.5f))
                {
                    m_Rigid.velocity = new Vector2(0, 0);
                    m_IsDefending = true;
                }
                return;
            }

            m_ShootTime += Time.deltaTime;

            if (m_ShootTime >= m_ShootTimer)
            {
                Shoot();
            }
        }

    }
    private GameObject GetClosestTarget(List<GameObject> _Target)
    {
        GameObject Target = null;
        float MinDist = Mathf.Infinity;
        Vector2 CurrentPos = transform.position;
        float Dist = 0.0f;

        for (int i = 0; i < _Target.Count; i++)
        {
            if (_Target[i].GetComponent<Wall>().Building == EBuildingUpgrade.NONE)
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

    private GameObject GetClosestWall(List<GameObject> _Target)
    {
        GameObject Target = null;
        float MinDist = Mathf.Infinity;
        Vector2 CurrentPos = transform.position;
        float Dist = 0.0f;

        for (int i = 0; i < _Target.Count; i++)
        {
            Dist = Vector2.Distance(_Target[i].transform.position, CurrentPos);

            if (Dist < MinDist)
            {
                Target = _Target[i];
                MinDist = Dist;
            }
        }

        return Target;
    }

    private void Shoot()
    {
        m_ShootTime = 0.0f;

        float XDistance;
        XDistance = Random.Range(m_Target.transform.position.x - m_ThrowPoint.position.x, m_Direction.x * 5.0f);

        float YDistance;
        YDistance = Random.Range(m_Target.transform.position.y - m_ThrowPoint.position.y, 5.0f);

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

    public void RemoveRabbit()
    {
        m_Rabbits.Remove(m_CurrentRabbit);
    }
    
    public void RemoveSpawner()
    {
        m_EnemySpawner.Remove(m_CurrentSpawner);
    }
}
