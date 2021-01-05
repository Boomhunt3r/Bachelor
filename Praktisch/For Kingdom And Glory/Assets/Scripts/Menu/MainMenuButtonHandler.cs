using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Main;
    [SerializeField]
    private GameObject m_Tutorial;
    [SerializeField]
    private AudioSource m_Source;

    void Awake()
    {
        m_Main.SetActive(true);
        m_Tutorial.SetActive(false);
    }

    public void ClickedStart()
    {
        m_Main.SetActive(false);
        m_Tutorial.SetActive(true);
        m_Source.Play();
    }

    public void ClickedExit()
    {
        m_Source.Play();
        Application.Quit();
    }

    public void ClickedX()
    {
        m_Source.Play();
        m_Tutorial.SetActive(false);
        m_Main.SetActive(true);
    }

    public void ClickedYes()
    {
        PlayerPrefs.SetInt("Tutorial", 0);
        m_Source.Play();
        SceneManager.LoadScene("Game");
    }

    public void ClickedNo()
    {
        PlayerPrefs.SetInt("Tutorial", 1);
        m_Source.Play();
        SceneManager.LoadScene("Game");
    }
}
