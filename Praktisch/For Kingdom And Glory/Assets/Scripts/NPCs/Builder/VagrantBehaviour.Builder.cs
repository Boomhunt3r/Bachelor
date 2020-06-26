using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public partial class VagrantBehaviour : MonoBehaviour
{
   private void Builder()
    {
        m_Render.color = Color.green;
        this.gameObject.tag = "Builder";
    }
}
