using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public partial class VagrantBehaviour : MonoBehaviour
{
    private void Builder()
    {
        m_Render.color = Color.green;
        this.gameObject.tag = "Builder";

        if (m_BuildWalls.Count != 0 || m_BuildWalls != null)
        {
            if (!m_ReparingWall)
            {
                for (int i = 0; i < m_BuildWalls.Count; i++)
                {

                }
            }
        }
    }
}
