using UnityEngine;

public partial class PlayerBehaviour : MonoBehaviour
{
    /// <summary>
    /// Repair Armor Function
    /// </summary>
    public void RepairArmor()
    {
        #region One Stone, others none
        // If one is Stone and others are none
        if (Inventory.Instance.Helmet == EPlayerUpgrade.STONE &&
           Inventory.Instance.Plate == EPlayerUpgrade.NONE &&
           Inventory.Instance.Boots == EPlayerUpgrade.NONE
           ||
           Inventory.Instance.Plate == EPlayerUpgrade.STONE &&
           Inventory.Instance.Boots == EPlayerUpgrade.NONE &&
           Inventory.Instance.Helmet == EPlayerUpgrade.NONE
           ||
           Inventory.Instance.Boots == EPlayerUpgrade.STONE &&
           Inventory.Instance.Helmet == EPlayerUpgrade.NONE &&
           Inventory.Instance.Plate == EPlayerUpgrade.NONE)
        {
            // If helmet is stone
            if (Inventory.Instance.Helmet == EPlayerUpgrade.STONE)
            {
                // If there 3 or more stones in inventory
                if (Inventory.Instance.Stone >= 3)
                {
                    // Reduce stones in inventory
                    Inventory.Instance.Stone -= 3;
                    // Set Armor to max armor
                    m_Armor = m_MaxArmor;
                }
            }
            if (Inventory.Instance.Plate == EPlayerUpgrade.STONE)
            {
                if (Inventory.Instance.Stone >= 6)
                {
                    Inventory.Instance.Stone -= 6;

                    m_Armor = m_MaxArmor;
                }
            }
            if (Inventory.Instance.Boots == EPlayerUpgrade.STONE)
            {
                if (Inventory.Instance.Stone >= 2)
                {
                    Inventory.Instance.Stone -= 6;

                    m_Armor = m_MaxArmor;
                }
            }
        }
        #endregion

        #region One Iron, others none
        if (Inventory.Instance.Helmet == EPlayerUpgrade.IRON &&
           Inventory.Instance.Plate == EPlayerUpgrade.NONE &&
           Inventory.Instance.Boots == EPlayerUpgrade.NONE
           ||
           Inventory.Instance.Plate == EPlayerUpgrade.IRON &&
           Inventory.Instance.Boots == EPlayerUpgrade.NONE &&
           Inventory.Instance.Helmet == EPlayerUpgrade.NONE
           ||
           Inventory.Instance.Boots == EPlayerUpgrade.IRON &&
           Inventory.Instance.Helmet == EPlayerUpgrade.NONE &&
           Inventory.Instance.Plate == EPlayerUpgrade.NONE)
        {
            if (Inventory.Instance.Helmet == EPlayerUpgrade.IRON)
            {
                if (Inventory.Instance.Iron >= 3)
                {
                    Inventory.Instance.Iron -= 3;
                    m_Armor = m_MaxArmor;
                }
            }
            if (Inventory.Instance.Plate == EPlayerUpgrade.IRON)
            {
                if (Inventory.Instance.Iron >= 6)
                {
                    Inventory.Instance.Iron -= 6;
                    m_Armor = m_MaxArmor;
                }
            }
            if (Inventory.Instance.Boots == EPlayerUpgrade.IRON)
            {
                if (Inventory.Instance.Iron >= 2)
                {
                    Inventory.Instance.Iron -= 6;
                    m_Armor = m_MaxArmor;
                }
            }
        }
        #endregion

        #region Two Stone, other none
        if(Inventory.Instance.Helmet == EPlayerUpgrade.STONE && 
           Inventory.Instance.Plate == EPlayerUpgrade.STONE &&
           Inventory.Instance.Boots == EPlayerUpgrade.NONE
           ||
           Inventory.Instance.Helmet == EPlayerUpgrade.STONE &&
           Inventory.Instance.Boots == EPlayerUpgrade.STONE && 
           Inventory.Instance.Plate == EPlayerUpgrade.NONE
           ||
           Inventory.Instance.Plate == EPlayerUpgrade.STONE &&
           Inventory.Instance.Boots == EPlayerUpgrade.STONE &&
           Inventory.Instance.Helmet == EPlayerUpgrade.NONE)
        {
            if(Inventory.Instance.Helmet == EPlayerUpgrade.STONE &&
               Inventory.Instance.Plate == EPlayerUpgrade.STONE)
            {
                if(Inventory.Instance.Stone >= 9)
                {
                    Inventory.Instance.Stone -= 9;
                    m_Armor = m_MaxArmor;
                }
            }

            if (Inventory.Instance.Plate == EPlayerUpgrade.STONE &&
               Inventory.Instance.Boots == EPlayerUpgrade.STONE)
            {
                if (Inventory.Instance.Stone >= 5)
                {
                    Inventory.Instance.Stone -= 5;
                    m_Armor = m_MaxArmor;
                }
            }

            if (Inventory.Instance.Helmet == EPlayerUpgrade.STONE &&
               Inventory.Instance.Plate == EPlayerUpgrade.STONE)
            {
                if (Inventory.Instance.Stone >= 8)
                {
                    Inventory.Instance.Stone -= 8;
                    m_Armor = m_MaxArmor;
                }
            }
        }
        #endregion

        #region Two Iron, other none
        if (Inventory.Instance.Helmet == EPlayerUpgrade.IRON &&
           Inventory.Instance.Plate == EPlayerUpgrade.IRON &&
           Inventory.Instance.Boots == EPlayerUpgrade.NONE
           ||
           Inventory.Instance.Helmet == EPlayerUpgrade.IRON &&
           Inventory.Instance.Boots == EPlayerUpgrade.IRON &&
           Inventory.Instance.Plate == EPlayerUpgrade.NONE
           ||
           Inventory.Instance.Plate == EPlayerUpgrade.IRON &&
           Inventory.Instance.Boots == EPlayerUpgrade.IRON &&
           Inventory.Instance.Helmet == EPlayerUpgrade.NONE)
        {
            if (Inventory.Instance.Helmet == EPlayerUpgrade.IRON &&
               Inventory.Instance.Plate == EPlayerUpgrade.IRON)
            {
                if (Inventory.Instance.Iron >= 9)
                {
                    Inventory.Instance.Iron -= 9;

                    m_Armor = m_MaxArmor;
                }
            }

            if (Inventory.Instance.Plate == EPlayerUpgrade.IRON &&
               Inventory.Instance.Boots == EPlayerUpgrade.IRON)
            {
                if (Inventory.Instance.Iron >= 5)
                {
                    Inventory.Instance.Iron -= 5;

                    m_Armor = m_MaxArmor;
                }
            }

            if (Inventory.Instance.Helmet == EPlayerUpgrade.IRON &&
               Inventory.Instance.Plate == EPlayerUpgrade.IRON)
            {
                if (Inventory.Instance.Iron >= 8)
                {
                    Inventory.Instance.Iron -= 8;

                    m_Armor = m_MaxArmor;
                }
            }
        }
        #endregion

        #region All Stone
        if (Inventory.Instance.Helmet == EPlayerUpgrade.STONE &&
           Inventory.Instance.Plate == EPlayerUpgrade.STONE &&
           Inventory.Instance.Boots == EPlayerUpgrade.STONE)
        {
            if (Inventory.Instance.Stone >= 11)
            {
                Inventory.Instance.Stone -= 11;

                m_Armor = m_MaxArmor;
            }
        }
        #endregion

        #region All Iron

        if (Inventory.Instance.Helmet == EPlayerUpgrade.IRON &&
           Inventory.Instance.Plate == EPlayerUpgrade.IRON &&
           Inventory.Instance.Boots == EPlayerUpgrade.IRON)
        {
            if (Inventory.Instance.Iron >= 11)
            {
                Inventory.Instance.Iron -= 11;

                m_Armor = m_MaxArmor;
            }
        } 
        #endregion
    }
}