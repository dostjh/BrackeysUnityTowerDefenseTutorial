using UnityEngine;

public class GameManager : MonoBehaviour
{
	// NOTE: Static variables persist even when scene is restarted.
	public static bool IsGameOver;
	public GameObject gameOverUI;
	public GameObject levelCompleteUI;
	// TODO: Make a better level manager

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
		IsGameOver = true;

		Debug.Log("Level Complete");
		levelCompleteUI.SetActive(true);
	}
}
