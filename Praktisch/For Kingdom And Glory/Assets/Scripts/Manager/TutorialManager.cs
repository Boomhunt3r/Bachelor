using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    #region SerilaizeField
    [Header("Tutorial UI")]
    [SerializeField]
    private GameObject m_TutUI;
    [SerializeField]
    private GameObject m_ContUI;
    [SerializeField]
    private GameObject m_TownUI;
    [SerializeField]
    private GameObject m_ArchUI;
    [SerializeField]
    private GameObject m_BuildUI;
    [SerializeField]
    private GameObject m_SmithUI;
    [SerializeField]
    private GameObject m_SpawnUI;
    [SerializeField]
    private GameObject m_WallUI;
    [Header("Tutoial Text")]
    [SerializeField]
    private GameObject m_ArchText;
    [SerializeField]
    private GameObject m_BuildText;
    [SerializeField]
    private GameObject m_ContText;
    [SerializeField]
    private GameObject m_SmithText;
    [SerializeField]
    private GameObject m_SpawnText;
    [SerializeField]
    private GameObject m_TownText;
    [SerializeField]
    private GameObject m_WallText;
    #endregion

    #region private Variables
    private GameObject m_ActiveText = null;
    private bool m_Tutorial = false;
    private bool m_Controls = false;
    private bool m_Town = false;
    private bool m_Archery = false;
    private bool m_Builder = false;
    private bool m_Smithy = false;
    private bool m_Spawner = false;
    private bool m_Wall = false;
    private bool m_FirstButton = false;
    #endregion

    private int m_Tut = 0;


    private void Awake()
    {
        m_ArchUI.SetActive(false);
        m_ArchText.SetActive(false);
        m_BuildUI.SetActive(false);
        m_BuildText.SetActive(false);
        m_ContUI.SetActive(false);
        m_ContText.SetActive(false);
        m_SmithUI.SetActive(false);
        m_SmithText.SetActive(false);
        m_SpawnUI.SetActive(false);
        m_SpawnText.SetActive(false);
        m_TownUI.SetActive(false);
        m_TownText.SetActive(false);
        m_TutUI.SetActive(false);
        m_WallUI.SetActive(false);
        m_WallText.SetActive(false);

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Tut = PlayerPrefs.GetInt("Tutorial", 0);

        if (m_Tut == 0)
            m_Tutorial = false;
        else if (m_Tut == 1)
            m_Tutorial = true;

        if (m_Tutorial == false)
        {
            m_TutUI.SetActive(false);
        }

        if (m_Tutorial == true)
        {
            m_TutUI.SetActive(true);

            m_ContUI.SetActive(true);

            m_Controls = true;
        }
    }

    private void Update()
    {
        if (m_ActiveText != null)
            Debug.Log(m_ActiveText.name);

        if (!CheckIfTutIsOpen())
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                SetTutfalse();
            }
        }
    }

    #region public Function
    public void Tutorial(string _Obj)
    {
        if (m_Tutorial)
        {
            if (CheckTutStatus())
            {
                if (_Obj == "Wall")
                {
                    if (m_Wall == false)
                    {
                        if (CheckIfTutIsOpen())
                        {
                            m_WallUI.SetActive(true);

                            m_Wall = true;
                        }
                    }
                }
                if (_Obj == "Archery")
                {
                    if (m_Archery == false)
                    {
                        if (CheckIfTutIsOpen())
                        {
                            m_ArchUI.SetActive(true);

                            m_Archery = true;
                        }
                    }
                }
                if (_Obj == "Build")
                {
                    if (m_Builder == false)
                    {
                        if (CheckIfTutIsOpen())
                        {
                            m_BuildUI.SetActive(true);

                            m_Builder = true;
                        }
                    }
                }
                if (_Obj == "Smithy")
                {
                    if (m_Smithy == false)
                    {
                        if (CheckIfTutIsOpen())
                        {
                            m_SmithUI.SetActive(true);

                            m_Smithy = true;
                        }
                    }
                }
                if (_Obj == "Town")
                {
                    if (m_Town == false)
                    {
                        if (CheckIfTutIsOpen())
                        {
                            m_TownUI.SetActive(true);

                            m_Town = true;
                        }
                    }
                }
                if (_Obj == "VagrantSpawner")
                {
                    if (m_Spawner == false)
                    {
                        if (CheckIfTutIsOpen())
                        {
                            m_SpawnUI.SetActive(true);

                            m_Spawner = true;
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region private functions
    private bool CheckTutStatus()
    {
        if (m_Wall && m_Controls && m_Builder && m_Archery && m_Smithy && m_Spawner && m_Town)
        {
            m_TutUI.SetActive(false);
            return false;
        }

        return true;
    }

    private bool CheckIfTutIsOpen()
    {
        if (m_ArchUI.activeSelf || m_BuildUI.activeSelf ||
            m_ContUI.activeSelf || m_SmithUI.activeSelf ||
            m_SpawnUI.activeSelf || m_TownUI.activeSelf || m_WallUI.activeSelf)
            return false;

        return true;
    }

    private void SetTutfalse()
    {
        if (m_ArchUI.activeSelf)
            m_ArchUI.SetActive(false);
        if (m_BuildUI.activeSelf)
            m_BuildUI.SetActive(false);
        if (m_ContUI.activeSelf)
            m_ContUI.SetActive(false);
        if (m_SmithUI.activeSelf)
            m_SmithUI.SetActive(false);
        if (m_SpawnUI.activeSelf)
            m_SpawnUI.SetActive(false);
        if (m_TownUI.activeSelf)
            m_TownUI.SetActive(false);
        if (m_WallUI.activeSelf)
            m_WallUI.SetActive(false);
    }

    private void ActivateText(GameObject _Obj)
    {
        if (!m_FirstButton)
        {
            m_ActiveText = _Obj;

            _Obj.SetActive(true);

            m_FirstButton = true;

            return;
        }

        if (_Obj == m_ArchText)
        {
            if(_Obj == m_ActiveText)
            {
                _Obj.SetActive(false);
                return;
            }
            if(_Obj != m_ActiveText)
            {
                m_ActiveText.SetActive(false);
                m_ActiveText = _Obj;
                _Obj.SetActive(true);
            }
        }

        if (_Obj == m_BuildText)
        {
            if (_Obj == m_ActiveText)
            {
                _Obj.SetActive(false);
            }
            if (_Obj != m_ActiveText)
            {
                m_ActiveText.SetActive(false);
                m_ActiveText = _Obj;
                _Obj.SetActive(true);
            }
        }

        if (_Obj == m_ContText)
        {
            if (_Obj == m_ActiveText)
            {
                _Obj.SetActive(false);
            }
            if (_Obj != m_ActiveText)
            {
                m_ActiveText.SetActive(false);
                m_ActiveText = _Obj;
                _Obj.SetActive(true);
            }
        }

        if (_Obj == m_SmithText)
        {
            if (_Obj == m_ActiveText)
            {
                _Obj.SetActive(false);
            }
            if (_Obj != m_ActiveText)
            {
                m_ActiveText.SetActive(false);
                m_ActiveText = _Obj;
                _Obj.SetActive(true);
            }
        }

        if (_Obj == m_SpawnText)
        {
            if (_Obj == m_ActiveText)
            {
                _Obj.SetActive(false);
            }
            if (_Obj != m_ActiveText)
            {
                m_ActiveText.SetActive(false);
                m_ActiveText = _Obj;
                _Obj.SetActive(true);
            }
        }

        if (_Obj == m_TownText)
        {
            if (_Obj == m_ActiveText)
            {
                _Obj.SetActive(false);
            }
            if (_Obj != m_ActiveText)
            {
                m_ActiveText.SetActive(false);
                m_ActiveText = _Obj;
                _Obj.SetActive(true);
            }
        }

        if (_Obj == m_WallText)
        {
            if (_Obj == m_ActiveText)
            {
                _Obj.SetActive(false);
            }
            if (_Obj != m_ActiveText)
            {
                m_ActiveText.SetActive(false);
                m_ActiveText = _Obj;
                _Obj.SetActive(true);
            }
        }
    }
    #endregion

    #region UI Functions
    public void ClickedX(GameObject _Obj)
    {
        if (_Obj.activeSelf)
            _Obj.SetActive(false);
    }

    public void ClickedButton(string _Name)
    {
        switch (_Name)
        {
            case "Archery":
                ActivateText(m_ArchText);
                break;
            case "Builder":
                ActivateText(m_BuildText);
                break;
            case "Control":
                ActivateText(m_ContText);
                break;
            case "Smithy":
                ActivateText(m_SmithText);
                break;
            case "Spawner":
                ActivateText(m_SpawnText);
                break;
            case "Town":
                ActivateText(m_TownText);
                break;
            case "Wall":
                ActivateText(m_WallText);
                break;
            default:
                break;
        }
    }
    #endregion
}
