using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public partial class VagrantBehaviour : MonoBehaviour
{
    private void Villager()
    {
        m_Render.color = Color.gray;
        this.gameObject.tag = "Villager";
        #region Idle Path
        if (!m_ToolInRange)
        {
            m_Target = m_VillagerPoints[m_CurrentDirection];
        }
        #endregion

        #region if object is not active
        // If Hammer is null
        if (m_Hammer == null)
        {
            // Find Hammer Object
            m_Hammer = GameObject.FindGameObjectWithTag("Hammer");
            // Set Tool in Range false
            m_ToolInRange = false;
        }
        else if (m_Bow == null)
        {
            m_Bow = GameObject.FindGameObjectWithTag("Bow");
            m_ToolInRange = false;
        }
        else if (m_Sword == null)
        {
            m_Sword = GameObject.FindGameObjectWithTag("Sword");
            m_ToolInRange = false;
        }
        #endregion

        #region If Object is Active
        if (m_Hammer != null)
        {
            if (Vector2.SqrMagnitude((Vector2)m_Hammer.transform.position - m_Rigid.position) <= 45)
            {
                // Set Hammer to Target
                m_Target = m_Hammer;
                // Set Tool in Range true
                m_ToolInRange = true;
            }
        }
        else if (m_Bow != null)
        {
            if (Vector2.SqrMagnitude((Vector2)m_Bow.transform.position - m_Rigid.position) <= 45)
            {
                // Set Bow to Target
                m_Target = m_Bow;
                m_ToolInRange = true;
            }
        }
        else if (m_Sword != null)
        {
            if (Vector2.SqrMagnitude((Vector2)m_Sword.transform.position - m_Rigid.position) <= 45)
            {
                // Set Sword to Target
                m_Target = m_Sword;
                m_ToolInRange = true;
            }
        }
        #endregion
    }
}
