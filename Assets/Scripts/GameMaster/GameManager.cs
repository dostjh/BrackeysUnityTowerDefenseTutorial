using UnityEngine;

public class GameManager : MonoBehaviour
{
	// NOTE: Static variables persist even when scene is restarted.
	public static bool IsGameOver;
	public GameObject gameOverUI;
	// TODO: Make a better level manager
	public string nextLevel = "Level02";
	public int levelToUnlock = 2;

	public SceneFader sceneFader;

	void Start()
	{
		IsGameOver = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (IsGameOver)
		{
			return;
		}

		// TODO: Remove. Just using this to make it easy to end game early.
		if (Input.GetKeyDown("e"))
		{
			EndGame();
		}

		if (PlayerStats.Lives <= 0)
		{
			EndGame();
		}
	}

	void EndGame()
	{
		IsGameOver = true;
		Debug.Log("Game Over!");

		gameOverUI.SetActive(true);
	}

	public void WinLevel()
	{
		Debug.Log("Level Complete");
		PlayerPrefs.SetInt("LevelReached", levelToUnlock);
		sceneFader.FadeTo(nextLevel);
	}
}
