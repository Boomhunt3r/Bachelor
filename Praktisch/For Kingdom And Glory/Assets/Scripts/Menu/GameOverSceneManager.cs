using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceneManager : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    private GameObject m_Over;
    [SerializeField]
    private AudioSource m_Source;
    #endregion

    private void Start()
    {
        m_Over.SetActive(true);
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
        SceneManager.LoadScene("Game");
    }

    public void Return()
    {
        m_Source.Play();
        m_Over.SetActive(true);
    }

    #endregion
}
