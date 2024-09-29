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
                Enemy clonedObject = enemyPool.GetFromPool(transform.position, transform.rotation);
                if (clonedObject == null)
                {
                    Debug.LogError("Failed to spawn enemy from the pool!");
                }
            }

            yield return new WaitForSeconds(period);
        }
    }
}
