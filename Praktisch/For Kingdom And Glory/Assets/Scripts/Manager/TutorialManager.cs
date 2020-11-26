using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    #region SerilaizeField
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
    #endregion

    #region private Variables
    private bool m_Tutorial = false;
    private bool m_Controls = false;
    private bool m_Town = false;
    private bool m_Archery = false;
    private bool m_Builder = false;
    private bool m_Smithy = false;
    private bool m_Spawner = false;
    private bool m_Wall = false;
    #endregion

    private int m_Tut = 0;


    private void Awake()
    {
        m_ArchUI.SetActive(false);
        m_BuildUI.SetActive(false);
        m_ContUI.SetActive(false);
        m_SmithUI.SetActive(false);
        m_SpawnUI.SetActive(false);
        m_TownUI.SetActive(false);
        m_TutUI.SetActive(false);
        m_WallUI.SetActive(false);

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

    }

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

    public void ClickedX(GameObject _Obj)
    {
        if (_Obj.activeSelf)
            _Obj.SetActive(false);
    }
}
