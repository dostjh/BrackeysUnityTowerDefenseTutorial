using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public GameObject UI;
	public SceneFader SceneFader;
	public string MenuSceneName = "MainMenu";

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			Toggle();
		}
	}

	/// <summary>
	/// Toggles the timeScale between 0 (Off - Game is paused) and 1 (On - Game is running).
	/// </summary>
	void Toggle()
	{
		UI.SetActive(!UI.activeSelf);

		if (UI.activeSelf)
		{
			// NOTE: When you adjust timeScale, also need to adjust Time.deltaTime.
			// However, since we're setting timeScale to 0, no need to adjust both.
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	public void Continue()
	{
		Toggle();
	}

	/*
	 * TODO: Move PauseMenu into GameManager
	 * It's not entirely clear why we don't have the Pause Menu script in the Game Manager script like we do the Game Over UI. 
	 * Then we get the Retry() and MainMenu() clicks for free.
	 */

	public void Retry()
	{
		var currentSceneName = SceneManager.GetActiveScene().name;

		Debug.Log($"Retry Scene: {currentSceneName}");
		Toggle();
		SceneFader.FadeTo(currentSceneName);
	}

	public void MainMenu()
	{
		Debug.Log($"Go to {MenuSceneName}");
		Toggle();
		SceneFader.FadeTo(MenuSceneName);
	}
}
