using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public string levelToLoad = "MainLevel";

	public void Play()
	{
		Debug.Log($"Opening level {levelToLoad}");
		SceneManager.LoadScene(levelToLoad);
	}

	public void Quit()
	{
		Debug.Log("Exiting");
		Application.Quit();
	}
}
