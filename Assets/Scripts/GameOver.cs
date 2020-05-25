using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public SceneFader SceneFader;
	public string MenuSceneName = "MainMenu";

	public void Retry()
	{
		var currentScene = SceneManager.GetActiveScene().name;

		Debug.Log($"Retry scene {currentScene}");
		SceneFader.FadeTo(currentScene);
	}

	public void MainMenu()
	{
		Debug.Log($"Go to {MenuSceneName}");
		SceneFader.FadeTo(MenuSceneName);
	}
}
