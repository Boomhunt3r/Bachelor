
using UnityEngine;


public class Smithy : MonoBehaviour
{
    public static Smithy Instance { get; private set; }

    #region SerializeField
    [SerializeField]
    private GameObject m_OuterWall;
    [SerializeField]
    private GameObject m_CraftingUI;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
