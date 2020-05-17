using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
	public Image FadeImage;
	public AnimationCurve FadeCurve;

	void Start()
	{
		StartCoroutine(FadeIn());
	}

	public void FadeTo(string scene)
	{
		Debug.Log($"FadeTo: {scene}");
		StartCoroutine(FadeOut(scene));
	}

	IEnumerator FadeIn()
	{
		Debug.Log("FadeIn()");
		var time = 1f;

		// Decrease gradually over time and don't continue until we get to 0;
		while (time > 0f)
		{
			time -= Time.deltaTime;
			var alpha = FadeCurve.Evaluate(time);
			FadeImage.color = new Color(0f, 0f, 0f, alpha);
			yield return 0;
		}
	}

	IEnumerator FadeOut(string scene)
	{
		Debug.Log("FadeOut()");
		var time = 0f;

		// Decrease gradually over time and don't continue until we get to 0;
		while (time < 1f)
		{
			time += Time.deltaTime;
			var alpha = FadeCurve.Evaluate(time);
			FadeImage.color = new Color(0f, 0f, 0f, alpha);
			yield return 0;
		}

		SceneManager.LoadScene(scene);
	}
}
