using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WallManager : MonoBehaviour
{
    public static WallManager Instance { get; private set; }

    #region SerializeField
    [SerializeField]
    private TextMeshProUGUI m_Text;
    #endregion

    #region private Variables
    private GameObject m_WallObj;

    #endregion

    #region Properties
    public GameObject WallObj { get => m_WallObj; set => m_WallObj = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
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
                }
                break;
            case EBuildingUpgrade.PILE:
                if (Inventory.Instance.Coins >= WallObj.GetComponent<Wall>().CoinCost &&
                    Inventory.Instance.Wood >= WallObj.GetComponent<Wall>().WoodCost)
                {
                    Inventory.Instance.Coins -= WallObj.GetComponent<Wall>().CoinCost;
                    Inventory.Instance.Wood -= WallObj.GetComponent<Wall>().WoodCost;

                    WallObj.GetComponent<Wall>().UpgradeBuilding();
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
    #endregion
}
