using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    public static VillagerManager Instance { get; private set; }


    //private GameObject[] m_Bows = new GameObject[4];
    //private GameObject[] m_Hammers = new GameObject[4];
    private List<GameObject> m_AllVillager = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToList(GameObject _Villager)
    {
        if (_Villager == null)
            return;

        m_AllVillager.Add(_Villager);
    }

    public void RemoveFromList(GameObject _Villager)
    {
        if (_Villager == null)
            return;

        if(m_AllVillager.Count > 0)
        {
            for (int i = 0; i < m_AllVillager.Count; i++)
            {
                if (_Villager == m_AllVillager[i])
                    m_AllVillager.Remove(_Villager);
            }
        }
    }

    public void AddAllBow(GameObject _Bow)
    {
        if (_Bow == null)
            return;

        if(m_AllVillager.Count > 0)
        {
            for (int i = 0; i < m_AllVillager.Count; i++)
            {
                m_AllVillager[i].GetComponent<VagrantBehaviour>().AddToBowList(_Bow);
            }
        }
        else if(m_AllVillager.Count == 0)
        {
            AddAllBow(_Bow);
        }
    }

    public void RemoveAllBow(GameObject _Bow)
    {
        if (_Bow == null)
            return;

        if(m_AllVillager.Count > 0)
        {
            for (int i = 0; i < m_AllVillager.Count; i++)
            {
                m_AllVillager[i].GetComponent<VagrantBehaviour>().RemoveFromBowList(_Bow);
            }
        }
    }

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
            AddAllHammer(_Hammer);
        }
    }

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
    }
}
