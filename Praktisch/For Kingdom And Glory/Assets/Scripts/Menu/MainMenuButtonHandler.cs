using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Main;
    [SerializeField]
    private GameObject m_Setting;

    void Awake()
    {
        m_Main.SetActive(true);
        m_Setting.SetActive(false);
    }
    

    public void ClickedStart()
    {
        m_Main.SetActive(false);
        m_Setting.SetActive(true);
    }

    public void ClickedExit()
    {
        Application.Quit();
    }

    public void ClickedEasy()
    {
        PlayerPrefs.SetInt("Setting", (int)EGameSetting.EASY);
        SceneManager.LoadScene("Game");
    }

    public void ClickedMedium()
    {
        PlayerPrefs.SetInt("Setting", (int)EGameSetting.MEDUIM);
        SceneManager.LoadScene("Game");
    }

    public void ClickedHard()
    {
        PlayerPrefs.SetInt("Setting", (int)EGameSetting.HARD);
        SceneManager.LoadScene("Game");
    }

    public void ClickedX()
    {
        m_Main.SetActive(true);
        m_Setting.SetActive(false);
    }
}
