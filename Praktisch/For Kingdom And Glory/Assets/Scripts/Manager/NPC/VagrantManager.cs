using System.Collections.Generic;
using UnityEngine;

public class VagrantManager : MonoBehaviour
{
    public static VagrantManager Instance { get; private set; }

    private List<GameObject> m_AllVagrants = new List<GameObject>();
    private List<GameObject> m_Coins = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;   
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Add Vagrant to VagrantList
    /// </summary>
    /// <param name="_Vagrant">Vagrant to add</param>
    public void AddToList(GameObject _Vagrant)
    {
        if (_Vagrant == null)
            return;

        m_AllVagrants.Add(_Vagrant);

        if(m_Coins.Count > 0)
        {
            for (int i = 0; i < m_Coins.Count; i++)
            {
                AddCoinsForAll(m_Coins[i]);
            }
        }
    }

    /// <summary>
    /// Remove from VagrantList
    /// </summary>
    /// <param name="_Vagrant">Vagrant to remove</param>
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

    /// <summary>
    /// Add coin for all Vagrants
    /// </summary>
    /// <param name="_Coin">Coin to add</param>
    public void AddCoinsForAll(GameObject _Coin)
    {
        if(m_AllVagrants.Count > 0)
        {
            for (int i = 0; i < m_AllVagrants.Count; i++)
            {
                m_AllVagrants[i].GetComponent<VagrantBehaviour>().AddCoin(_Coin);
            }
        }
        else if(m_AllVagrants.Count == 0)
        {
            m_Coins.Add(_Coin);
        }

    }

    /// <summary>
    /// Remove coin for all Vagrants
    /// </summary>
    /// <param name="_Coin">Coin to remove</param>
    public void RemoveCoinsForAll(GameObject _Coin)
    {
        if(m_AllVagrants.Count > 0)
        {
            for (int i = 0; i < m_AllVagrants.Count; i++)
            {
                m_AllVagrants[i].GetComponent<VagrantBehaviour>().RemoveCoin(_Coin);
            }
        }
        if(m_Coins.Count > 0)
        {
            for (int i = 0; i < m_Coins.Count; i++)
            {
                if(_Coin == m_Coins[i])
                {
                    m_Coins.Remove(m_Coins[i]);
                }
            }
        }
    }
}
