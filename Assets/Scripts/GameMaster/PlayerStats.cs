using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	// NOTE: Static variables carry across scenes
	public static int Money;
	public int startMoney = 400;

	public static int Lives;
	public int startLives = 20;

	public static int RoundsSurvived;

	void Start()
	{
		Money = startMoney;
		Lives = startLives;

		RoundsSurvived = 0;
	}
}
