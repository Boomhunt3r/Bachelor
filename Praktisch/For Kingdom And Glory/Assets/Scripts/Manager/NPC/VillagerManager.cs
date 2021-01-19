using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    public static VillagerManager Instance { get; private set; }

    private List<GameObject> m_Bows = new List<GameObject>();
    private List<GameObject> m_Hammers = new List<GameObject>();
    private List<GameObject> m_AllVillager = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"Bows: {m_Bows.Count}");
    }

    /// <summary>
    /// Add Villager to VillagerList
    /// </summary>
    /// <param name="_Villager">Villager to add</param>
    public void AddToList(GameObject _Villager)
    {
        // If villager is null return
        if (_Villager == null)
            return;

        m_AllVillager.Add(_Villager);

        if (m_Bows.Count > 0)
        {
            for (int i = 0; i < m_Bows.Count; i++)
            {
                AddAllBow(m_Bows[i]);
            }
        }

        if (m_Hammers.Count > 0)
        {
            for (int i = 0; i < m_Hammers.Count; i++)
            {
                AddAllHammer(m_Hammers[i]);
            }
        }
    }

    /// <summary>
    /// Remove Villager from VillagerList
    /// </summary>
    /// <param name="_Villager">Villager to remove</param>
    public void RemoveFromList(GameObject _Villager)
    {
        if (_Villager == null)
            return;

        if (m_AllVillager.Count > 0)
        {
            for (int i = 0; i < m_AllVillager.Count; i++)
            {
                if (_Villager == m_AllVillager[i])
                    m_AllVillager.Remove(_Villager);
            }
        }
    }

    /// <summary>
    /// Add Bow for all Villager
    /// </summary>
    /// <param name="_Bow">Bow to add</param>
    public void AddAllBow(GameObject _Bow)
    {
        if (_Bow == null)
            return;

        if (m_AllVillager.Count > 0)
        {
            for (int i = 0; i < m_AllVillager.Count; i++)
            {
                m_AllVillager[i].GetComponent<VagrantBehaviour>().AddToBowList(_Bow);
            }
        }
        else if (m_AllVillager.Count == 0)
        {
            m_Bows.Add(_Bow);
        }
    }

    /// <summary>
    /// Remove Bow for all Villager
    /// </summary>
    /// <param name="_Bow">Bow to remove</param>
    public void RemoveAllBow(GameObject _Bow)
    {
        if (_Bow == null)
            return;

        if (m_AllVillager.Count > 0)
        {
            for (int i = 0; i < m_AllVillager.Count; i++)
            {
                m_AllVillager[i].GetComponent<VagrantBehaviour>().RemoveFromBowList(_Bow);
            }
        }

        if (m_Bows.Count > 0)
        {
            for (int i = 0; i < m_Bows.Count; i++)
            {
                if (_Bow == m_Bows[i])
                    m_Bows.Remove(m_Bows[i]);
            }
        }
    }

    /// <summary>
    /// Add Hammer to all Villagers
    /// </summary>
    /// <param name="_Hammer">Hammer to add</param>
    public void AddAllHammer(GameObject _Hammer)
    {
        if (_Hammer == null)
            return;

        if (m_AllVillager.Count > 0)
        {
            for (int i = 0; i < m_AllVillager.Count; i++)
            {
                m_AllVillager[i].GetComponent<VagrantBehaviour>().AddToHammerList(_Hammer);
            }
        }
        else if (m_AllVillager.Count == 0)
        {
            m_Hammers.Add(_Hammer);
        }
    }

    /// <summary>
    /// Remove hammer for alle Villagers
    /// </summary>
    /// <param name="_Hammer">Hammer to remove</param>
    public void RemoveAllHammer(GameObject _Hammer)
    {
        if (_Hammer == null)
            return;

        if (m_AllVillager.Count > 0)
        {
            for (int i = 0; i < m_AllVillager.Count; i++)
            {
                m_AllVillager[i].GetComponent<VagrantBehaviour>().RemoveFromHammerList(_Hammer);
            }
        }

        if (m_Hammers.Count > 0)
        {
            for (int i = 0; i < m_Hammers.Count; i++)
            {
                if (_Hammer == m_Hammers[i])
                    m_Hammers.Remove(m_Hammers[i]);
            }
        }
    }
}
