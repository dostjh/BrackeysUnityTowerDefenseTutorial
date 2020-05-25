using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class RoundsSurvived : MonoBehaviour
{
	public Text roundsText;

	void OnEnable()
	{
		StartCoroutine(AnimateText());
	}

	// NOTE: Coroutine
	IEnumerator AnimateText()
	{
		roundsText.text = "0";
		var round = 0;

		// Wait for the text field to appear
		// TODO: Can we base this on the time of fade in the animation?
		yield return new WaitForSeconds(.7f);

		while(round < PlayerStats.RoundsSurvived)
		{
			round++;
			roundsText.text = round.ToString();

			// Wait a bit between increments to allow human eye to see increments
			yield return new WaitForSeconds(.05f);
		}
	}
}
