using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class VagrantBehaviour : MonoBehaviour
{
    private bool m_IsDefending = false;
    private GameObject m_CurrentRabbit;

    private void Archer()
    {
        m_Render.color = Color.red;
        this.gameObject.tag = "Archer";

        m_Rabbits = GameObject.FindGameObjectsWithTag("Rabbit").OfType<GameObject>().ToList();

        if (GameManager.Instance.IsDay)
        {
            if (m_Rabbits.Count != 0)
            {
                if (!m_Hunting)
                {
                    m_Target = GetClosestRabbit(m_Rabbits);
                    m_CurrentRabbit = m_Target;
                }

                else if (m_Hunting)
                {
                    m_ShootTime += Time.deltaTime;

                    m_Distance = Vector2.Distance(m_Rigid.position, m_Target.transform.position);

                    if (m_Distance <= 2.5f)
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

        else if (GameManager.Instance.IsNight)
        {
            m_Hunting = false;

            if (!m_IsDefending)
            {
                m_Target = GetClosestWall(m_BuildWalls);

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
    private GameObject GetClosestRabbit(List<GameObject> _Rabbits)
    {
        GameObject Target = null;
        float MinDist = Mathf.Infinity;
        Vector2 CurrentPos = transform.position;
        float Dist = 0.0f;

        for (int i = 0; i < _Rabbits.Count; i++)
        {
            Dist = Vector2.Distance(_Rabbits[i].transform.position, CurrentPos);

            if (Dist < MinDist)
            {
                Target = _Rabbits[i];
                MinDist = Dist;
            }
        }

        return Target;
    }

    private GameObject GetClosestWall(List<GameObject> _Walls)
    {
        GameObject Target = null;
        float MinDist = Mathf.Infinity;
        Vector2 CurrentPos = transform.position;
        float Dist = 0.0f;

        for (int i = 0; i < _Walls.Count; i++)
        {
            Dist = Vector2.Distance(_Walls[i].transform.position, CurrentPos);

            if (Dist < MinDist)
            {
                Target = _Walls[i];
                MinDist = Dist;
            }
        }

        return Target;
    }

    private void Shoot()
    {
        m_ShootTime = 0.0f;

        GameObject Arrow = Instantiate(m_Arrow, this.transform.position, Quaternion.identity);
        Arrow.GetComponent<Rigidbody2D>().velocity = m_ArrowSpeed * m_Direction * Time.deltaTime;
    }

    public void RemoveRabbit()
    {
        m_Rabbits.Remove(m_CurrentRabbit);
    }
}
