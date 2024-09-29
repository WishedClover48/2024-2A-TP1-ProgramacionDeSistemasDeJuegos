using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class EnemyPool : MonoBehaviour
{
    // The prefab for the objects in the pool
    public Enemy objectPrefab;

    // The pool of objects
    private Queue<Enemy> pool = new Queue<Enemy>();

    // PreLoad is called when the script instance is being loaded
    private void Awake()
    {
        ServiceLocator.Instance.RegisterService(this);
    }

    // Method to get an object from the pool
    public Enemy GetFromPool(Vector3 position, Quaternion rotation)
    {
        if (objectPrefab == null)
        {
            Debug.LogError("objectPrefab is not assigned in the EnemyPool!");
            return null;
        }
        Enemy obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            // If the pool is empty, create a new object
            obj = objectPrefab.Clone(position, rotation);

            if (obj == null)
            {
                Debug.LogError("Failed to instantiate a new Enemy from the objectPrefab!");
                return null;
            }
        }

        obj.gameObject.SetActive(true);  // Enable the object before returning
        return obj;
    }

    // Method to return an object back to the pool
    public void ReturnToPool(Enemy obj)
    {
        if (obj == null)
        {
            Debug.LogError("Cannot return a null object to the pool!");
            return;
        }
        obj.gameObject.SetActive(false);  // Disable the object
        obj.gameObject.transform.SetParent(transform);
        pool.Enqueue(obj);  // Add it back to the pool
    }
}
