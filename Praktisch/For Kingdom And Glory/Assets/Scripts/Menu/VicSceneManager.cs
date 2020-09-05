using UnityEngine;
using UnityEngine.SceneManagement;

public class VicSceneManager : MonoBehaviour
{
	[SerializeField]
	private AudioSource m_Source;

	#region UI Functions
	public void ExitGame()
	{
		m_Source.Play();
		Application.Quit();
	}

	public void MainMenu()
	{
		m_Source.Play();
		SceneManager.LoadScene("MainMenu");
	}
	#endregion
}
