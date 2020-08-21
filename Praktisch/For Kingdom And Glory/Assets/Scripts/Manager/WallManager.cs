using UnityEngine;
using TMPro;

public class WallManager : MonoBehaviour
{
    public static WallManager Instance { get; private set; }

    #region SerializeField
    [SerializeField]
    private TextMeshProUGUI m_Text;
    [SerializeField]
    private AudioClip m_BuildSound;
    [SerializeField]
    private GameObject[] m_LeftSideWalls;
    [SerializeField]
    private GameObject[] m_RightSideWalls;
    #endregion

    #region private Variables
    private GameObject m_WallObj;
    private float m_Timer = 0.0f;
    private float m_Time = 3.0f;
    private GameObject m_LeftWall;
    private GameObject m_RightWall;
    #endregion

    #region Properties
    public GameObject WallObj { get => m_WallObj; set => m_WallObj = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.Setting == EGameSetting.EASY)
        {
            for (int i = 3; i < m_LeftSideWalls.Length; i++)
            {
                m_LeftSideWalls[i].SetActive(false);
                m_RightSideWalls[i].SetActive(false);
            }
        }
        if(GameManager.Instance.Setting == EGameSetting.MEDUIM)
        {
            for (int i = 5; i < m_LeftSideWalls.Length; i++)
            {
                m_LeftSideWalls[i].SetActive(false);
                m_RightSideWalls[i].SetActive(false);
            }
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Timer >= m_Time)
        {
            for (int i = 0; i < m_LeftSideWalls.Length; i++)
            {
                if (m_LeftSideWalls[i].GetComponent<Wall>().Building != EBuildingUpgrade.NONE)
                {
                    m_LeftWall = m_LeftSideWalls[i];
                }
            }

            for (int i = 0; i < m_RightSideWalls.Length; i++)
            {
                if (m_RightSideWalls[i].GetComponent<Wall>().Building != EBuildingUpgrade.IRON)
                {
                    m_RightWall = m_RightSideWalls[i];
                }
            }
        }

        m_Timer += Time.deltaTime;

        if (m_WallObj == null)
            return;

        if (WallObj.GetComponent<Wall>().Build == false)
        {
            return;
        }

        switch (WallObj.GetComponent<Wall>().Building)
        {
            case EBuildingUpgrade.NONE:
                m_Text.text = $"{Inventory.Instance.Coins} Coins / {WallObj.GetComponent<Wall>().CoinCost} Coins \n";
                break;
            case EBuildingUpgrade.PILE:
                m_Text.text = $"{Inventory.Instance.Coins} Coins / {WallObj.GetComponent<Wall>().CoinCost} Coins \n" +
                              $"{Inventory.Instance.Wood} Wood / {WallObj.GetComponent<Wall>().WoodCost} Wood";
                break;
            case EBuildingUpgrade.WOOD:
                m_Text.text = $"{Inventory.Instance.Coins} Coins / {WallObj.GetComponent<Wall>().CoinCost} Coins \n" +
                              $"{Inventory.Instance.Wood} Wood / {WallObj.GetComponent<Wall>().WoodCost} Wood \n" +
                              $"{Inventory.Instance.Stone} Stone / {WallObj.GetComponent<Wall>().StoneCost} Stone";
                break;
            case EBuildingUpgrade.STONE:
                m_Text.text = $"{Inventory.Instance.Coins} Coins / {WallObj.GetComponent<Wall>().CoinCost} Coins \n" +
                              $"{Inventory.Instance.Stone} Stone / {WallObj.GetComponent<Wall>().StoneCost} Stone \n" +
                              $"{Inventory.Instance.Iron} Iron / {WallObj.GetComponent<Wall>().IronCost} Iron";
                break;
            case EBuildingUpgrade.IRON:
                m_Text.text = "Max upgrade reached.";
                break;
            default:
                break;
        }
    }

    #region UI Functions
    public void Build()
    {
        if (WallObj == null)
            return;

        switch (WallObj.GetComponent<Wall>().Building)
        {
            case EBuildingUpgrade.NONE:
                if (Inventory.Instance.Coins >= WallObj.GetComponent<Wall>().CoinCost)
                {
                    Inventory.Instance.Coins -= WallObj.GetComponent<Wall>().CoinCost;

                    WallObj.GetComponent<Wall>().UpgradeBuilding();
                    PlayerBehaviour.Instance.EffectSource.clip = m_BuildSound;
                    PlayerBehaviour.Instance.EffectSource.Play();
                }
                break;
            case EBuildingUpgrade.PILE:
                if (Inventory.Instance.Coins >= WallObj.GetComponent<Wall>().CoinCost &&
                    Inventory.Instance.Wood >= WallObj.GetComponent<Wall>().WoodCost)
                {
                    Inventory.Instance.Coins -= WallObj.GetComponent<Wall>().CoinCost;
                    Inventory.Instance.Wood -= WallObj.GetComponent<Wall>().WoodCost;

                    WallObj.GetComponent<Wall>().UpgradeBuilding();
                    PlayerBehaviour.Instance.EffectSource.clip = m_BuildSound;
                    PlayerBehaviour.Instance.EffectSource.Play();
                }
                break;
            case EBuildingUpgrade.WOOD:
                if (Inventory.Instance.Coins >= WallObj.GetComponent<Wall>().CoinCost &&
                    Inventory.Instance.Wood >= WallObj.GetComponent<Wall>().WoodCost &&
                    Inventory.Instance.Stone >= WallObj.GetComponent<Wall>().StoneCost)
                {
                    Inventory.Instance.Coins -= WallObj.GetComponent<Wall>().CoinCost;
                    Inventory.Instance.Wood -= WallObj.GetComponent<Wall>().WoodCost;
                    Inventory.Instance.Stone -= WallObj.GetComponent<Wall>().StoneCost;

                    WallObj.GetComponent<Wall>().UpgradeBuilding();
                    PlayerBehaviour.Instance.EffectSource.clip = m_BuildSound;
                    PlayerBehaviour.Instance.EffectSource.Play();
                }
                break;
            case EBuildingUpgrade.STONE:
                if (Inventory.Instance.Coins >= WallObj.GetComponent<Wall>().CoinCost &&
                    Inventory.Instance.Stone >= WallObj.GetComponent<Wall>().StoneCost &&
                    Inventory.Instance.Iron >= WallObj.GetComponent<Wall>().IronCost)
                {
                    Inventory.Instance.Coins -= WallObj.GetComponent<Wall>().CoinCost;
                    Inventory.Instance.Stone -= WallObj.GetComponent<Wall>().StoneCost;
                    Inventory.Instance.Iron -= WallObj.GetComponent<Wall>().IronCost;

                    WallObj.GetComponent<Wall>().UpgradeBuilding();
                    PlayerBehaviour.Instance.EffectSource.clip = m_BuildSound;
                    PlayerBehaviour.Instance.EffectSource.Play();
                }
                break;
            case EBuildingUpgrade.IRON:
                break;
            default:
                break;
        }
    }
    public void ExitButton()
    {
        WallObj.GetComponent<Wall>().Build = false;
    }

    public GameObject GetLeftWall()
    {
        if (m_LeftWall != null)
            return m_LeftWall.GetComponent<Wall>().DefendPoint;
        else
            return null;
            

    }

    public GameObject GetRightWall()
    {
        if (m_RightWall != null)
            return m_RightWall.GetComponent<Wall>().DefendPoint;
        else
            return null;
    }
    #endregion
}
