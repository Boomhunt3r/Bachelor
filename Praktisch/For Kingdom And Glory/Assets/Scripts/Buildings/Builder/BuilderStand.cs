using UnityEngine;

public class BuilderStand : MonoBehaviour
{
    public static BuilderStand Instance { get; private set; }

    #region SerializeField
    [SerializeField]
    private int m_PricePerHammer = 2;
    [SerializeField]
    private int m_MaxHammerInStand = 4;
    [SerializeField]
    private GameObject[] m_Hammers;
    #endregion

    #region private Variables
    private int m_HammerInStand = 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_Hammers.Length; i++)
        {
            m_Hammers[i].SetActive(false);
        }

        Instance = this;
    }

    public void AddHammerToStand()
    {
        if (Inventory.Instance.Coins >= m_PricePerHammer)
        {
            int PrevAmount = m_HammerInStand;

            m_HammerInStand++;
            int Hammer = 0;

            if (m_HammerInStand > m_MaxHammerInStand)
            {
                m_HammerInStand = PrevAmount;
                return;
            }

            Inventory.Instance.Coins -= m_PricePerHammer;

            for (int i = 0; i < m_Hammers.Length; i++)
            {
                Hammer++;

                if (Hammer > m_HammerInStand)
                    return;

                if (!m_Hammers[i].activeSelf)
                {
                    m_Hammers[i].SetActive(true);
                    VillagerManager.Instance.AddAllHammer(m_Hammers[i]);
                }
            }
        }
    }

    public void RemoveHammerFromStand(GameObject _Hammer)
    {
        for (int i = 0; i < m_Hammers.Length; i++)
        {
            if (_Hammer == m_Hammers[i])
            {
                m_Hammers[i].SetActive(false);
            }
        }
        m_HammerInStand--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerBehaviour.Instance.CanBuyHammer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerBehaviour.Instance.CanBuyHammer = false;
        }
    }
}
