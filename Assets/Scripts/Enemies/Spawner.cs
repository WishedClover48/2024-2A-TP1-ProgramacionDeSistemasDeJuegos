using Enemies;
using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int spawnsPerPeriod = 10;
    [SerializeField] private float frequency = 30;
    [SerializeField] private float period = 0;
    [SerializeField] private EnemyPool enemyPool;

    private void OnEnable()
    {
        if (frequency > 0) period = 1 / frequency;
    }

    private IEnumerator Start()
    {
        if (enemyPool == null)
        {
            Debug.LogError("EnemyPool is not assigned to the Spawner!");
            yield break;
        }
        while (!destroyCancellationToken.IsCancellationRequested)
        {
            for (int i = 0; i < spawnsPerPeriod; i++)
            {
                // Randomize spawn positions within a certain range around the spawner in order to avoid enemies getting stuck.
                Vector3 randomOffset = new Vector3(
                    UnityEngine.Random.Range(-5f, 5f),
                    0,  // Keep the Y position consistent
                    UnityEngine.Random.Range(-5f, 5f)
                );

                Vector3 spawnPosition = transform.position + randomOffset;
                Quaternion spawnRotation = transform.rotation;

                Enemy clonedObject = enemyPool.GetFromPool(spawnPosition, spawnRotation);
                if (clonedObject == null)
                {
                    Debug.LogError("Failed to spawn enemy from the pool!");
                }
                clonedObject.transform.parent = transform;
            }

            yield return new WaitForSeconds(period);
        }
    }
}
