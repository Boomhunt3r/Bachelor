using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuilderManager : MonoBehaviour
{
    public static BuilderManager Instance { get; private set; }

    /// <summary>
    /// List with all Builders
    /// </summary>
    private List<GameObject> m_AllBuilder = new List<GameObject>();
    /// <summary>
    /// List with all Walls
    /// </summary>
    private List<GameObject> m_AllWalls = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // If Player not alive anymore return
        if (!GameManager.Instance.IsAlive)
            return;

        // if night return
        if (GameManager.Instance.IsNight)
            return;

        // if walls count is over 0
        if (m_AllWalls.Count > 0)
        {
            // For all walls in list
            for (int w = 0; w < m_AllWalls.Count; w++)
            {
                // if currenthitpoints from wall is lower than the maxhitpoints
                // the building is not none and is not being repaired
                if (m_AllWalls[w].GetComponent<Wall>().CurrentHitPoints < m_AllWalls[w].GetComponent<Wall>().MaxHitPoints &&
                    m_AllWalls[w].GetComponent<Wall>().Building != EBuildingUpgrade.NONE && 
                    !m_AllWalls[w].GetComponent<Wall>().BeingRepaired)
                {
                    // if builder count is over 0
                    if (m_AllBuilder.Count > 0)
                    {
                        // for all Buildr in List
                        for (int b = 0; b < m_AllBuilder.Count; b++)
                        {
                            // if Builder dosn't already have a wall to repair
                            if(!m_AllBuilder[b].GetComponent<VagrantBehaviour>().CheckIfHasWallToRepair())
                            {
                                // give builder wall to repair
                                m_AllBuilder[b].GetComponent<VagrantBehaviour>().WallToRepair(m_AllWalls[w]);

                                // set wall being repaired true
                                m_AllWalls[w].GetComponent<Wall>().BeingRepaired = true;

                                break;
                            }
                            // if builder has a wall to repair
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

    #region public functions
    /// <summary>
    /// Add Builder to Builder List
    /// </summary>
    /// <param name="_Builder">Builder to add</param>
    public void AddBuilderToList(GameObject _Builder)
    {
        if (_Builder == null)
            return;

        m_AllBuilder.Add(_Builder);
    }

    /// <summary>
    /// Remove Builder from Builder list
    /// </summary>
    /// <param name="_Builder">Builder to remove</param>
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

    /// <summary>
    /// Add Wall to Wall List
    /// </summary>
    /// <param name="_Wall">Wall to add</param>
    public void AddWallToList(GameObject _Wall)
    {
        if (_Wall == null)
            return;

        m_AllWalls.Add(_Wall);
    }

    /// <summary>
    /// Remove Wall from Wall List
    /// </summary>
    /// <param name="_Wall">Wall to remove</param>
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
    #endregion
}
