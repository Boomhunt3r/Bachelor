using UnityEngine;

public partial class PlayerBehaviour : MonoBehaviour
{
    #region public functions
    // TODO: Repair
    /// <summary>
    /// 
    /// </summary>
    public void RepairArmor()
    {
        int Dif = m_MaxArmor - m_Armor;

        bool HelmetPayed = false;
        bool PlatePayed = false;
        bool BootsPayed = false;

        switch (Inventory.Instance.Helmet)
        {
            case EPlayerUpgrade.STONE:
                if (Inventory.Instance.Stone >= 3)
                    HelmetPayed = true;
                break;
            case EPlayerUpgrade.IRON:
                if (Inventory.Instance.Iron >= 3)
                    HelmetPayed = true;
                break;
            default:
                break;
        }
        switch (Inventory.Instance.Plate)
        {
            case EPlayerUpgrade.STONE:
                if (Inventory.Instance.Stone >= 6)
                    PlatePayed = true;
                break;
            case EPlayerUpgrade.IRON:
                if (Inventory.Instance.Iron >= 6)
                    PlatePayed = true;
                break;
            default:
                break;
        }
        switch (Inventory.Instance.Boots)
        {
            case EPlayerUpgrade.STONE:
                if (Inventory.Instance.Stone >= 2)
                    BootsPayed = true;
                break;
            case EPlayerUpgrade.IRON:
                if (Inventory.Instance.Iron >= 2)
                    BootsPayed = true;
                break;
            default:
                break;
        }

        if (!HelmetPayed && !PlatePayed && !BootsPayed)
            return;

        if(HelmetPayed && !PlatePayed && !BootsPayed ||
           PlatePayed && !BootsPayed && !HelmetPayed ||
           BootsPayed && !HelmetPayed && !PlatePayed)
        {
            if(HelmetPayed)
            {
                PayRepair(Inventory.Instance.Helmet, ECraftingType.HELMET);
            }
            if(PlatePayed)
            {
                PayRepair(Inventory.Instance.Plate, ECraftingType.PLATE);

            }
            if (BootsPayed)
            {
                PayRepair(Inventory.Instance.Boots, ECraftingType.BOOTS);
            }
        }

        if(HelmetPayed && PlatePayed && !BootsPayed ||
           HelmetPayed && BootsPayed && !PlatePayed ||
           PlatePayed && BootsPayed && !HelmetPayed)
        {
            if(HelmetPayed && BootsPayed)
            {
                PayRepair(Inventory.Instance.Helmet, ECraftingType.HELMET);
                PayRepair(Inventory.Instance.Boots, ECraftingType.BOOTS);
            }
            if(HelmetPayed && PlatePayed)
            {
                PayRepair(Inventory.Instance.Helmet, ECraftingType.HELMET);
                PayRepair(Inventory.Instance.Plate, ECraftingType.PLATE);
            }
            if (PlatePayed && BootsPayed)
            {
                PayRepair(Inventory.Instance.Plate, ECraftingType.PLATE);
                PayRepair(Inventory.Instance.Boots, ECraftingType.BOOTS);
            }
        }

        if(HelmetPayed && PlatePayed && BootsPayed)
        {
            PayRepair(Inventory.Instance.Helmet, ECraftingType.HELMET);
            PayRepair(Inventory.Instance.Plate, ECraftingType.PLATE);
            PayRepair(Inventory.Instance.Boots, ECraftingType.BOOTS);
        }

        m_Armor = m_MaxArmor;
    }

    public void HealPlayer()
    {
        if (m_Health < m_MaxHealth)
        {
            if (Inventory.Instance.Coins >= 2)
            {
                m_Health = m_MaxHealth;
                Inventory.Instance.Coins -= 2;
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