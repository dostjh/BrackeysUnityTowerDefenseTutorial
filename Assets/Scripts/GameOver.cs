using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public Text roundsText;

	void OnEnable()
	{
		roundsText.text = PlayerStats.RoundsSurvived.ToString();	
	}

	public void Retry()
	{
		Debug.Log("Retry button pressed");
		// NOTE: SceneManager.LoadScene takes the build index of the scene or the string name of the Scene.
		// However, since both of these can change, we're going to call the active scene and get its
		// build index.
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void MainMenu()
	{
		Debug.Log("Menu button pressed");
	}
}
