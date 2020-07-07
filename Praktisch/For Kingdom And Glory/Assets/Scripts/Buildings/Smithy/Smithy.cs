using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Smithy : MonoBehaviour
{
    public static Smithy Instance { get; private set; }

    #region SerializeField
    [SerializeField]
    private GameObject m_SmithyUI;
    [SerializeField]
    private Slider m_PaySlider;
    [SerializeField]
    private TextMeshProUGUI m_SliderText;
    [SerializeField]
    private TextMeshProUGUI m_NotificationText;
    #endregion

    #region private Variables
    private EBuildingUpgrade m_Building;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_Building = EBuildingUpgrade.NONE;

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
