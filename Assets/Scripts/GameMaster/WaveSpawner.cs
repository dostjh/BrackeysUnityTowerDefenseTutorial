using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public float timeBetweenWaves = 5.5f;
    public Text waveCountdownText;
    public Transform spawnPoint;

    float countdown = 2f;
    int waveIndex = 0;

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        // Down by one every second
        countdown -= Time.deltaTime;
		countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

		waveCountdownText.text = $"{countdown.ToString("00.00")}";
    }

    // TODO: Watch Wave Spawner short series for more robust wave spawner by Brackeys
    // NOTE: To build a co-routine, you use an IEnumerator. This allows you to give the method its own timing by using yield return. You then need to call the method with StartCoroutine().
    IEnumerator SpawnWave()
    {
        waveIndex++;
		// Using localWaveIndex to track waveIndex for coroutine locally so we can do whatever we want to the waveIndex.
		var localWaveIndex = waveIndex;

        Debug.Log($"Spawning {localWaveIndex} enemies!");

        for (int i = 0; i < localWaveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
