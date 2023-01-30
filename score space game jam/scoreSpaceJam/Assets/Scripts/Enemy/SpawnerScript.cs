using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] float timeBtwWaveDecrMulti = 0.75f;
    [SerializeField] float movementSpeedIncreaseAfterWaveFinish = 1.1f;
    [SerializeField] float timeBeforeGameStart = 5f;
    WaveConfig currentWave;

    [SerializeField] bool isLooping = true;

    private void Awake()
    {
        ScriptableObject.CreateInstance("WaveConfig");
    }

    void Start()
    {
        StartCoroutine(SpawnSpawnEnemyWaves());
    }

    public WaveConfig GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnSpawnEnemyWaves()
    {
        yield return new WaitForSeconds(timeBeforeGameStart);
        do
        {
            foreach (WaveConfig wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i),
                                currentWave.GetStartingWaypoint().position,
                                Quaternion.identity,
                                transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
            for(int i = 0; i < waveConfigs.Count; i++)
            {
                waveConfigs[i].SetWaveMoveSpeedMultiplier(movementSpeedIncreaseAfterWaveFinish);
            }
            timeBetweenWaves *= timeBtwWaveDecrMulti;
        }
        while (isLooping);
    }
}
