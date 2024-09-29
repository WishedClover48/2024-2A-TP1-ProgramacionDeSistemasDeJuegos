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

    // The initial size of the pool
    public int initialPoolSize = 50;
    public Vector3 _initialPosition;
    public Quaternion _initialRotation;

    // PreLoad is called when the script instance is being loaded
    private void Awake()
    {
        ServiceLocator.Instance.RegisterService(this);
        // Pre-instantiate the pool with inactive objects
        for (int i = 0; i < initialPoolSize; i++)
        {
            Enemy obj = objectPrefab.Clone(_initialPosition,_initialRotation) as Enemy;
            obj.enabled = false;  // Disable it initially
            pool.Enqueue(obj);
        }
    }

    // Method to get an object from the pool
    public Enemy GetFromPool()
    {
        if (pool.Count > 0)
        {
            Enemy obj = pool.Dequeue();
            obj.enabled = true;  // Enable the object before returning
            return obj;
        }
        else
        {
            // If the pool is empty, create a new object and return it
            Enemy obj = objectPrefab.Clone(_initialPosition, _initialRotation) as Enemy;
            return obj;
        }
    }

    // Method to return an object back to the pool
    public void ReturnToPool(Enemy obj)
    {
        obj.gameObject.SetActive(false);  // Disable the object
        pool.Enqueue(obj);  // Add it back to the pool
    }
}
