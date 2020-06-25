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
    private GameObject m_ArcheryUI;
    [SerializeField]
    private GameObject[] m_BowObjectsInStand;
    [SerializeField]
    private Slider m_PaySlider;
    [SerializeField]
    private TextMeshProUGUI m_SliderText;
    [SerializeField]
    private TextMeshProUGUI m_Text;
    #endregion

    #region private Variables
    private int m_BowsInStand = 0;
    private int m_CurrentIndex = 0;
    private bool m_Buy = false;
    #endregion

    public bool Buy { get => m_Buy; set => m_Buy = value; }

    // Start is called before the first frame update
    void Start()
    {
        m_PaySlider.maxValue = m_PricePerBow;
        m_SliderText.text = $"0 / {m_PaySlider.maxValue}";
        m_ArcheryUI.SetActive(false);
        for (int i = 0; i < m_BowObjectsInStand.Length; i++)
        {
            m_BowObjectsInStand[i].SetActive(false);
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        ShowUI(Buy);
    }

    private void ShowUI(bool _Buy)
    {
        if (_Buy)
        {
            m_ArcheryUI.SetActive(true);
        }
        else
        {
            m_ArcheryUI.SetActive(false);
        }
    }

    private void AddBowToStand()
    {
        if (m_BowsInStand >= m_MaxBowsInStand)
        {
            m_Text.text = "Max Bows in Stand Reached.";
            return;
        }

        m_BowObjectsInStand[m_CurrentIndex].SetActive(true);

        m_CurrentIndex++;

        m_BowsInStand++;
    }

    private void RemoveCoins()
    {
        PlayerBehaviour.Instance.CoinsInInventory -= m_PricePerBow;
    }

    public void RemoveBowFromStand(GameObject _Bow)
    {
        for (int i = 0; i < m_BowObjectsInStand.Length; i++)
        {
            if (_Bow.transform.position == m_BowObjectsInStand[i].transform.position)
            {
                m_BowObjectsInStand[i].SetActive(false);
                m_CurrentIndex = i;
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
            m_PaySlider.value = 0;
            Buy = false;
        }
    }

    public void BuyButton()
    {
        if (m_PaySlider.value == m_PaySlider.maxValue && m_PaySlider.value <= PlayerBehaviour.Instance.CoinsInInventory)
        {
            if (m_BowsInStand == m_MaxBowsInStand)
            {
                m_Text.text = "Max Bows in Stand Reached.";
                return;
            }

            AddBowToStand();
            RemoveCoins();
            m_Text.text = "";
        }
    }

    public void UpdateText()
    {
        m_SliderText.text = $"{m_PaySlider.value} / {m_PaySlider.maxValue}";
    }

    public void ExitButton()
    {
        Buy = false;
    }
}
