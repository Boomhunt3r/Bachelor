using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class VagrantBehaviour : MonoBehaviour
{
    private List<GameObject> m_EnemySpawner = new List<GameObject>();
    private List<GameObject> m_Enemies = new List<GameObject>();
    private GameObject m_EnemyToShoot;
    private GameObject m_CurrentRabbit;
    private GameObject m_IdlePoint = null;
    private float m_Cooldown = 1.5f;
    private float m_CooldownTimer;
    private bool m_BCooldown = false;
    private bool m_IsDefending = false;
    private bool m_IsAttacking = false;
    private bool m_Searched = false;
    private bool m_Hunter = false;

    public bool IsAttacking { get => m_IsAttacking; set => m_IsAttacking = value; }
    public List<GameObject> EnemySpawner { get => m_EnemySpawner; set => m_EnemySpawner = value; }
    public bool Hunter { get => m_Hunter; set => m_Hunter = value; }

    private void Archer()
    {
        this.gameObject.tag = "Archer";

        m_Rabbits = GameObject.FindGameObjectsWithTag("Rabbit").ToList();

        if (IsAttacking)
        {
            if (m_EnemySpawner.Count != 0 && m_Enemies.Count == 0)
                m_Target = GetClosestTarget(m_EnemySpawner);

            if (m_Enemies.Count > 0)
                m_Target = GetClosestTarget(m_Enemies, m_EnemySpawner);

            if (Vector2.Distance(m_Rigid.position, m_Target.transform.position) > 7.5f)
            {
                m_IsIdle = false;
            }

            if (Vector2.Distance(m_Rigid.position, m_Target.transform.position) <= 7.5f)
            {
                m_Rigid.velocity = new Vector2(0, 0);
                m_ShootTime += Time.deltaTime;

                m_IsIdle = true;

                if (m_ShootTime >= m_ShootTimer)
                {
                    ChangeAnimation("Attack", false);
                    Shoot(m_Target);
                }
            }
        }

        if (GameManager.Instance.IsDay && !m_IsAttacking)
        {
            m_Searched = false;

            if (!IsAttacking)
            {
                if (!m_Hunting && !m_BCooldown)
                {
                    m_IdlePoint = m_TownHall;
                    m_Target = m_IdlePoint;
                    m_BCooldown = true;
                }

                if (m_BCooldown)
                {
                    if (Vector2.Distance(m_Rigid.position, m_Target.transform.position) <= m_Random)
                    {
                        m_Rigid.velocity = new Vector2(0, 0);
                        m_IsIdle = true;
                        m_CooldownTimer += Time.deltaTime;
                    }
                    if (Vector2.Distance(m_Rigid.position, m_Target.transform.position) > m_Random)
                    {
                        m_IsIdle = false;
                        m_Target = m_IdlePoint;
                    }

                    if (m_CooldownTimer >= m_Cooldown)
                    {
                        m_CooldownTimer = 0.0f;
                        m_BCooldown = false;
                        m_Hunting = true;
                        m_IsIdle = false;
                    }
                }

                if (m_Rabbits.Count != 0 && m_Hunting && m_Hunter)
                {
                    m_Target = GetClosestTarget(m_Rabbits);
                    m_CurrentRabbit = m_Target;

                    m_ShootTime += Time.deltaTime;

                    m_Distance = Vector2.Distance(m_Rigid.position, m_Target.transform.position);

                    if (m_Distance <= Random.Range(1.5f, 4.5f))
                    {
                        m_IsIdle = true;

                        m_Rigid.velocity = new Vector2(0, 0);

                        if (m_ShootTime >= m_ShootTimer)
                        {
                            Shoot(m_Target);
                        }
                    }
                    if (m_Distance > Random.Range(1.5f, 4.5f))
                    {
                        m_IsIdle = false;
                        m_Target = GetClosestTarget(m_Rabbits);
                    }
                }
            }
        }

        if (GameManager.Instance.AlmostNight && !m_IsAttacking)
        {
            m_Hunting = false;
            m_IsAttacking = false;

            if (m_DefendingWall != null)
                m_Target = m_DefendingWall;

            if (m_DefendingWall == null)
                m_Target = m_VillagerPoints[Random.Range(0, m_VillagerPoints.Length)];

            m_Distance = Vector2.Distance(m_Rigid.position, m_Target.transform.position);

            if (m_Distance <= Random.Range(1.0f, 8.5f))
            {
                m_Rigid.velocity = new Vector2(0, 0);
                m_IsDefending = true;
            }

            return;
        }

        if (GameManager.Instance.IsNight && !m_IsAttacking)
        {
            if (!m_IsDefending)
            {
                m_Distance = Vector2.Distance(m_Rigid.position, m_Target.transform.position);

                if (m_DefendingWall != null)
                    m_Target = m_DefendingWall;

                if (m_Distance <= 2.5f)
                {
                    m_Rigid.velocity = new Vector2(0, 0);
                    m_IsDefending = true;
                }

                return;
            }

            if (m_IsDefending)
            {
                if (m_Enemies.Count == 0 && !m_Searched)
                {
                    m_Enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
                    m_Searched = true;
                    return;
                }

                if (m_Enemies.Count == 0 && m_Searched)
                    return;

                if(m_Enemies.Count > 0 && m_Searched)
                {
                    for (int i = 0; i < m_Enemies.Count; i++)
                    {
                        if(m_Enemies[i] == null)
                        {
                            m_Enemies.RemoveAt(i);
                        }
                    }
                }

                m_Rigid.velocity = new Vector2(0, 0);

                m_EnemyToShoot = GetClosestTarget(m_Enemies);

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
    /// 
    /// </summary>
    /// <param name="_Target"></param>
    /// <param name="_Spawner"></param>
    /// <returns></returns>
    private GameObject GetClosestTarget(List<GameObject> _Target, List<GameObject> _Spawner)
    {
        GameObject TargetT = null;
        GameObject TargetS = null;
        GameObject Target = null;
        float MinDist = Mathf.Infinity;
        Vector2 CurrentPos = transform.position;
        float TDist = -1.0f;
        float SDist = -1.0f;
        float Dist = 0.0f;

        for (int i = 0; i < _Target.Count; i++)
        {
            if (_Target[i] == null)
                continue;

            Dist = Vector2.Distance(_Target[i].transform.position, CurrentPos);

            if (Dist < MinDist)
            {
                TargetT = _Target[i];
                MinDist = Dist;
                TDist = Dist;
            }
        }

        Dist = 0.0f;
        MinDist = Mathf.Infinity;

        for (int i = 0; i < _Spawner.Count; i++)
        {
            Dist = Vector2.Distance(_Spawner[i].transform.position, CurrentPos);

            if (Dist < MinDist)
            {
                TargetS = _Spawner[i];
                MinDist = Dist;
                SDist = Dist;
            }
        }

        Debug.Log("Spawner: " + TargetS);
        Debug.Log("Gegener: " + TargetT);

        if (TargetT == null)
        {
            Target = TargetS;
        }

        if (TargetS = null)
        {
            Target = TargetT;
        }

        if (TDist <= SDist)
        {
            Target = TargetT;
        }

        if (TDist == -1.0f)
        {
            Target = TargetS;
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
        YDistance = _Target.transform.position.y - m_ThrowPoint.position.y;

        float ThrowAngle;
        ThrowAngle = Mathf.Atan((YDistance + 4.905f) / XDistance);

        float TotalVelo = XDistance / Mathf.Cos(ThrowAngle);

        float XVelo;
        float YVelo;
        XVelo = TotalVelo * Mathf.Cos(ThrowAngle);
        YVelo = TotalVelo * Mathf.Sin(ThrowAngle);

        GameObject Arrow = Instantiate(m_Arrow, m_ThrowPoint.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(XVelo, YVelo);
        Arrow.GetComponent<Arrow>().Parent = this.gameObject;

        m_IsIdle = false;
    }

    /// <summary>
    /// Remove Spawner Function
    /// </summary>
    /// <param name="_Spawner">Spawner to Remove</param>
    public void RemoveSpawner(GameObject _Spawner)
    {
        if (m_EnemySpawner.Count > 0)
        {
            m_EnemySpawner.Remove(_Spawner);
        }
        if (m_EnemySpawner.Count == 0)
        {

        }
    }

    /// <summary>
    /// Add Enemy function
    /// </summary>
    /// <param name="_Enemy">Enemy to add</param>
    public void AddEnemy(GameObject _Enemy)
    {
        m_Enemies.Add(_Enemy);
    }

    /// <summary>
    /// Remove Enemy function
    /// </summary>
    /// <param name="_Enemy">Enemy to remove</param>
    public void RemoveEnemy(GameObject _Enemy)
    {
        if (m_Enemies.Count > 0)
        {
            for (int i = 0; i < m_Enemies.Count; i++)
            {
                if (m_Enemies[i] == _Enemy)
                    m_Enemies.Remove(_Enemy);
            }
        }
    }

    /// <summary>
    /// Remove Rabbit Function
    /// </summary>
    /// <param name="_Rabbit">Rabbiz to remove</param>
    public void RemoveRabbit(GameObject _Rabbit)
    {
        for (int i = 0; i < m_Rabbits.Count; i++)
        {
            if (_Rabbit == m_Rabbits[i])
                m_Rabbits.Remove(_Rabbit);
        }

        m_Hunting = false;
    }

    public void Attack()
    {
        m_IsIdle = false;
        m_BCooldown = false;
        m_IsDefending = false;
        m_Hunter = false;
    }
}
