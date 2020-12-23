using UnityEngine;

public class CheatManager : MonoBehaviour
{
    [SerializeField]
    [Range(1, 150)]
    private int m_Wood = 50;
    [SerializeField]
    [Range(1, 150)]
    private int m_Stone = 50;
    [SerializeField]
    [Range(1, 150)]
    private int m_Iron = 50;
    [SerializeField]
    [Range(1, 150)]
    private int m_Coin = 50;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Inventory.Instance.Wood += m_Wood;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Inventory.Instance.Stone += m_Stone;
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                Inventory.Instance.Iron += m_Iron;
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                Inventory.Instance.Coins = m_Coin;
            }
        }
    }
}
