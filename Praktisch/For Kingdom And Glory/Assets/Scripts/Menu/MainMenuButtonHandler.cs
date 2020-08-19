using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Main;
    [SerializeField]
    private GameObject m_Setting;
    [SerializeField]
    private AudioSource m_Source;

    void Awake()
    {
        m_Main.SetActive(true);
        m_Setting.SetActive(false);
    }
    

    public void ClickedStart()
    {
        m_Main.SetActive(false);
        m_Setting.SetActive(true);
        m_Source.Play();
    }

    public void ClickedExit()
    {
        m_Source.Play();
        Application.Quit();
    }

    public void ClickedEasy()
    {
        m_Source.Play();
        PlayerPrefs.SetInt("Setting", (int)EGameSetting.EASY);
        SceneManager.LoadScene("Game");
    }

    public void ClickedMedium()
    {
        m_Source.Play();
        PlayerPrefs.SetInt("Setting", (int)EGameSetting.MEDUIM);
        SceneManager.LoadScene("Game");
    }

    public void ClickedHard()
    {
        m_Source.Play();
        PlayerPrefs.SetInt("Setting", (int)EGameSetting.HARD);
        SceneManager.LoadScene("Game");
    }

    public void ClickedX()
    {
        m_Source.Play();
        m_Main.SetActive(true);
        m_Setting.SetActive(false);
    }
}
