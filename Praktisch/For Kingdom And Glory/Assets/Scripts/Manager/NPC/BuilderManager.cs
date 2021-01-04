using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuilderManager : MonoBehaviour
{
    public static BuilderManager Instance { get; private set; }

    private List<GameObject> m_AllBuilder = new List<GameObject>();
    private List<GameObject> m_AllWalls = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsAlive)
            return;

        if (GameManager.Instance.IsNight)
            return;

        if (m_AllWalls.Count > 0)
        {
            for (int w = 0; w < m_AllWalls.Count; w++)
            {
                if (m_AllWalls[w].GetComponent<Wall>().CurrentHitPoints < m_AllWalls[w].GetComponent<Wall>().MaxHitPoints &&
                    m_AllWalls[w].GetComponent<Wall>().Building != EBuildingUpgrade.NONE && 
                    !m_AllWalls[w].GetComponent<Wall>().BeingRepaired)
                {
                    if (m_AllBuilder.Count > 0)
                    {
                        for (int b = 0; b < m_AllBuilder.Count; b++)
                        {
                            if(!m_AllBuilder[b].GetComponent<VagrantBehaviour>().CheckIfHasWallToRepair())
                            {
                                m_AllBuilder[b].GetComponent<VagrantBehaviour>().WallToRepair(m_AllWalls[w]);

                                m_AllWalls[w].GetComponent<Wall>().BeingRepaired = true;

                                break;
                            }
                            else if(m_AllBuilder[b].GetComponent<VagrantBehaviour>().CheckIfHasWallToRepair())
                            {
                                continue;
                            }
                        }
                    }
                }
            }
        }
    }

    public void AddBuilderToList(GameObject _Builder)
    {
        if (_Builder == null)
            return;

        m_AllBuilder.Add(_Builder);
    }

    public void RemoveBuilderFromList(GameObject _Builder)
    {
        if (_Builder == null)
            return;

        if (m_AllBuilder.Count == 0)
            return;

        for (int i = 0; i < m_AllBuilder.Count; i++)
        {
            if (_Builder == m_AllBuilder[i])
            {
                m_AllBuilder.RemoveAt(i);
                break;
            }
        }
    }

    public void AddWallToList(GameObject _Wall)
    {
        if (_Wall == null)
            return;

        m_AllWalls.Add(_Wall);
    }

    public void RemoveWallFromList(GameObject _Wall)
    {
        if (_Wall == null)
            return;

        if (m_AllWalls.Count == 0)
            return;

        for (int i = 0; i < m_AllWalls.Count; i++)
        {
            if (_Wall == m_AllWalls[i])
            {
                m_AllWalls.RemoveAt(i);
                break;
            }
        }
    }
}
