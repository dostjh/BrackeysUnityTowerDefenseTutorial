﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
	public static int EnemiesAlive = 0;

    public float timeBetweenWaves = 5.5f;
    public Text waveCountdownText;
    public Transform spawnPoint;

	public Wave[] waves;

    float countdown = 2f;
    int waveIndex = 0;

    void Update()
    {
		if (EnemiesAlive > 0)
		{
			// Return early when we still have enemies on the map
			return;
		}

		if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
			// Return early to avoid single frame countdown below
			return;
        }

        // Down by one every second
        countdown -= Time.deltaTime;
		countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

		waveCountdownText.text = $"{countdown.ToString("00.00")}";
    }

    // TODO: Watch Wave Spawner short series for more robust wave spawner by Brackeys
    // NOTE: To build a co-routine, you use an IEnumerator. This allows you to give the method its own timing by using yield return. You then need to call the method with StartCoroutine().
	// BUG: Multiple waves on map?
    IEnumerator SpawnWave()
    {
		PlayerStats.RoundsSurvived = waveIndex;
		var wave = waves[waveIndex];

		// Using localWaveIndex to track waveIndex for coroutine locally so we can do whatever we want to the waveIndex.
		var localWaveIndex = waveIndex;

        Debug.Log($"Spawning {wave.count} enemies!");

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

		waveIndex++;

		// BUG: Not displaying when level complete. Game over script eventually appeared with 0 rounds survived.
		// BUG: Not updating rounds survived when a wave is destroyed.
		if (waveIndex == waves.Length)
		{
			Debug.Log("LEVEL COMPLETE");
			this.enabled = false;
		}
	}

	void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
		EnemiesAlive++;
    }
}
