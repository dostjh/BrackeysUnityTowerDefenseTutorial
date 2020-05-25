using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
	public string menuSceneName = "MainMenu";

	public string nextLevel = "Level02";
	public int levelToUnlock = 2;

	public SceneFader sceneFader;
	
	public void Continue()
	{
		PlayerPrefs.SetInt("LevelReached", levelToUnlock);
		sceneFader.FadeTo(nextLevel);
	}

	public void Menu()
	{
		sceneFader.FadeTo(menuSceneName);
	}
}
