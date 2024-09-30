using Audio;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerPool : MonoBehaviour
{
    // The prefab for the objects in the pool
    public AudioPlayer audioPlayerPrefab;

    // The pool of objects
    private Queue<AudioPlayer> pool = new Queue<AudioPlayer>();

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService(this);
    }

    // Method to get an object from the pool
    public AudioPlayer GetFromPool()
    {
        // Check if the prefab is assigned before using it
        if (audioPlayerPrefab == null)
        {
            Debug.LogError("audioPlayerPrefab is not assigned in the AudioPlayerPool!");
            return null;
        }

        AudioPlayer obj;

        if (pool.Count > 0)
        {
            // Get an object from the pool
            obj = pool.Dequeue();
        }
        else
        {
            // If the pool is empty, create a new object
            obj = audioPlayerPrefab.Clone();

            // Check if the instantiation was successful
            if (obj == null)
            {
                Debug.LogError("Failed to instantiate a new AudioPlayer from the audioPlayerPrefab!");
                return null;
            }
        }

        // Enable the object before returning it
        obj.gameObject.transform.SetParent(transform);
        obj.gameObject.SetActive(true);
        return obj;
    }
    // Method to return an object back to the pool
    public void ReturnToPool(AudioPlayer obj)
    {
        if (obj == null)
        {
            Debug.LogError("Cannot return a null object to the pool!");
            return;
        }
        // The object disables itself so there is no need to do it here.
        pool.Enqueue(obj);  // Add it back to the pool
    }
}

