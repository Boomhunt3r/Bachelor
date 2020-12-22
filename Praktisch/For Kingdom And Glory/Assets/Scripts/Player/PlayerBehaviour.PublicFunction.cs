using UnityEngine;

public partial class PlayerBehaviour : MonoBehaviour
{
    #region public functions
    public void HealPlayer()
    {
        if (m_Health < m_MaxHealth)
        {
            if (Inventory.Instance.Coins >= m_HealthCost)
            {
                m_Health = m_MaxHealth;
                Inventory.Instance.Coins -= m_HealthCost;
                m_HealthCost += 2;
            }
        }
    }

    public void GetDamage(int _Amount)
    {
        if (m_Armor <= 0)
        {
            m_Health -= _Amount;
        }
        else if (m_Armor > 0)
        {
            m_Armor -= _Amount;
        }

        if (m_Health <= 0)
        {
            GameManager.Instance.IsAlive = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_Armor">Armor being Upgraded</param>
    public void UpgradeArmor(ECraftingType _Armor)
    {
        switch (_Armor)
        {
            case ECraftingType.HELMET:
                switch (Inventory.Instance.Helmet)
                {
                    case EPlayerUpgrade.NONE:
                        m_Helmet = 0;
                        break;
                    case EPlayerUpgrade.STONE:
                        m_Helmet = 5;
                        break;
                    case EPlayerUpgrade.IRON:
                        m_Helmet = 10;
                        break;
                    default:
                        break;
                }
                break;
            case ECraftingType.PLATE:
                switch (Inventory.Instance.Plate)
                {
                    case EPlayerUpgrade.NONE:
                        m_Plate = 0;
                        break;
                    case EPlayerUpgrade.STONE:
                        m_Plate = 10;
                        break;
                    case EPlayerUpgrade.IRON:
                        m_Plate = 20;
                        break;
                    default:
                        break;
                }
                break;
            case ECraftingType.BOOTS:
                switch (Inventory.Instance.Boots)
                {
                    case EPlayerUpgrade.NONE:
                        m_Boots = 0;
                        break;
                    case EPlayerUpgrade.STONE:
                        m_Boots = 5;
                        break;
                    case EPlayerUpgrade.IRON:
                        m_Boots = 10;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }

        m_MaxArmor = m_Helmet + m_Plate + m_Boots;

        Armor = m_Helmet + m_Plate + m_Boots;
    }
    #endregion
}