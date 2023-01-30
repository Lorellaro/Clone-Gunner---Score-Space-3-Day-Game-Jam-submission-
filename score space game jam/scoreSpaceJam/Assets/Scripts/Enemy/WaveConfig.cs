using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float moveSpeedCopy = 5f;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] float timeBetweenEnemySpawns = 0.5f;
    [SerializeField] float spawnTimerVariance = 0.2f;
    [SerializeField] float minimumSpawnTime = 0.2f;

    private void OnEnable()
    {
        moveSpeed = moveSpeedCopy;
    }

    public  Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach(Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void SetWaveMoveSpeedMultiplier(float moveSpeedMultiplication)
    {
        moveSpeed *= moveSpeedMultiplication;
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimerVariance,
                                        timeBetweenEnemySpawns + spawnTimerVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
