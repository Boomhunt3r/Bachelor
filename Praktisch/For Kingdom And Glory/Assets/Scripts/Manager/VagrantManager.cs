using System.Collections.Generic;
using UnityEngine;

public class VagrantManager : MonoBehaviour
{
    public static VagrantManager Instance { get; private set; }

    private List<GameObject> m_AllVagrants = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToList(GameObject _Vagrant)
    {
        if (_Vagrant == null)
            return;

        m_AllVagrants.Add(_Vagrant);
    }

    public void RemoveFromList(GameObject _Vagrant)
    {
        if (_Vagrant == null)
            return;

        if(m_AllVagrants.Count > 0)
        {
            for (int i = 0; i < m_AllVagrants.Count; i++)
            {
                if(_Vagrant == m_AllVagrants[i])
                {
                    m_AllVagrants.Remove(_Vagrant);
                }
            }
        }
    }

    public void RemoveCoinsForAll(GameObject _Coin)
    {
        if(m_AllVagrants.Count > 0)
        {
            for (int i = 0; i < m_AllVagrants.Count; i++)
            {
                m_AllVagrants[i].GetComponent<VagrantBehaviour>().RemoveCoin(_Coin);
            }
        }
    }
}
