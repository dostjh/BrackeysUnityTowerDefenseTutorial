using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public Text roundsText;
	public SceneFader SceneFader;
	public string MenuSceneName = "MainMenu";

	void OnEnable()
	{
		roundsText.text = PlayerStats.RoundsSurvived.ToString();	
	}

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
