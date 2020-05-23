using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
	public SceneFader sceneFader;

	public Button[] levelButtons;

	void Start()
	{
		var levelReached = PlayerPrefs.GetInt("LevelReached", 1);

		// NOTE: Brackeys had us check in each loop if we were greater than level reached + 1.
		// More efficient to just set i to levelReached and lock out all the level buttons found that
		// way.
		for (var i = levelReached; i < levelButtons.Length; i++)
		{
			levelButtons[i].interactable = false;
		}
	}

	public void Select(string levelName)
	{
		sceneFader.FadeTo(levelName);
	}
}
