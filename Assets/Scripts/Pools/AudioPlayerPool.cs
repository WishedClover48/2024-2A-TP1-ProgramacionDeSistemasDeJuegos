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
    public AudioPlayer GetFromPool(Vector3 position, Quaternion rotation)
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
            obj = Instantiate(audioPlayerPrefab, position, rotation);

            // Check if the instantiation was successful
            if (obj == null)
            {
                Debug.LogError("Failed to instantiate a new AudioPlayer from the audioPlayerPrefab!");
                return null;
            }
        }

        // Set the object's position and rotation before returning it
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        // Enable the object before returning it
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

        obj.gameObject.SetActive(false);  // Disable the object
        pool.Enqueue(obj);  // Add it back to the pool
    }
}

