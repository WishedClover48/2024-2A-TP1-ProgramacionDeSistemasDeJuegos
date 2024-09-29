using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private int spawnsPerPeriod = 10;
    [SerializeField] private float frequency = 30;
    [SerializeField] private float period = 0;

    private void OnEnable()
    {
        if (frequency > 0) period = 1 / frequency;
    }

    private IEnumerator Start()
    {
        while (!destroyCancellationToken.IsCancellationRequested)
        {
            for (int i = 0; i < spawnsPerPeriod; i++)
            {
                Instantiate(characterPrefab, transform.position, transform.rotation);
            }

            yield return new WaitForSeconds(period);
        }
    }
}
