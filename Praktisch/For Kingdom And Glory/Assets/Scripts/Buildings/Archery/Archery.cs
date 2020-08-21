using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Archery : MonoBehaviour
{
    public static Archery Instance { get; private set; }

    #region SerializeFields
    [SerializeField]
    private int m_PricePerBow = 2;
    [SerializeField]
    private int m_MaxBowsInStand = 4;
    [SerializeField]
    private GameObject[] m_BowObjectsInStand;
    #endregion

    #region private Variables
    private int m_BowsInStand = 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_BowObjectsInStand.Length; i++)
        {
            m_BowObjectsInStand[i].SetActive(false);
        }
        Instance = this;
    }

    public void AddBowToStand()
    {
        if (Inventory.Instance.Coins >= m_PricePerBow)
        {
            int PrevAmount = m_BowsInStand;

            m_BowsInStand++;

            int Bow = 0;

            if (m_BowsInStand > m_MaxBowsInStand)
            {
                m_BowsInStand = PrevAmount;
                return;
            }

            Inventory.Instance.Coins -= m_PricePerBow;

            for (int i = 0; i < m_BowObjectsInStand.Length; i++)
            {
                Bow++;

                if (Bow > m_BowsInStand)
                    return;

                if (!m_BowObjectsInStand[i].activeSelf)
                {
                    m_BowObjectsInStand[i].SetActive(true);
                }
            }
        }
    }

    public void RemoveBowFromStand(GameObject _Bow)
    {
        for (int i = 0; i < m_BowObjectsInStand.Length; i++)
        {
            if (_Bow == m_BowObjectsInStand[i])
            {
                m_BowObjectsInStand[i].SetActive(false);
            }
        }
        m_BowsInStand--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerBehaviour.Instance.CanBuyBows = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerBehaviour.Instance.CanBuyBows = false;
        }
    }
}
