using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
	public Text livesText;
	
	// NOTE: If this were mobile, we'd want to make this a "coroutine" or have it updated by decrease in life in PlayerStats instead of on every update
	// Update is called once per frame
    void Update()
    {
		livesText.text = $"{PlayerStats.Lives} {(PlayerStats.Lives != 1 ? "Lives" : "Life")}".ToUpper();
    }
}
