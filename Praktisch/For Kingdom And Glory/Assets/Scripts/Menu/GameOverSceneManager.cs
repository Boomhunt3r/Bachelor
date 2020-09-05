using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceneManager : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    private GameObject m_Over;
    [SerializeField]
    private GameObject m_Setting;
    [SerializeField]
    private AudioSource m_Source;
    #endregion

    private void Start()
    {
        m_Over.SetActive(true);
        m_Setting.SetActive(false);
    }

    #region UI Functions
    public void MainMenu()
    {
        m_Source.Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        m_Source.Play();
        Application.Quit();
    }

    public void StartNewGame()
    {
        m_Source.Play();
        m_Over.SetActive(false);
        m_Setting.SetActive(true);
    }

    public void Easy()
    {
        m_Source.Play();
        PlayerPrefs.SetInt("Setting", (int)EGameSetting.EASY);
        SceneManager.LoadScene("Game");
    }

    public void Medium()
    {
        m_Source.Play();
        PlayerPrefs.SetInt("Setting", (int)EGameSetting.MEDUIM);
        SceneManager.LoadScene("Game");
    }

    public void Hard()
    {
        m_Source.Play();
        PlayerPrefs.SetInt("Setting", (int)EGameSetting.HARD);
        SceneManager.LoadScene("Game");
    }

    public void Return()
    {
        m_Source.Play();
        m_Over.SetActive(true);
        m_Setting.SetActive(false);
    }

    #endregion
}
