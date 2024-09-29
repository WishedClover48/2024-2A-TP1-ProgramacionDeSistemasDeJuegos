using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class  ServiceLocator
{
    private static ServiceLocator _instance;

    // Dictionary to hold references to services
    private Dictionary<Type, object> _services = new Dictionary<Type, object>();

    // Singleton access point to the Service Locator
    public static ServiceLocator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ServiceLocator();
            }
            return _instance;
        }
    }

    // Register a service with the Service Locator
    public void RegisterService<T>(T service)
    {
        var type = typeof(T);
        if (!_services.ContainsKey(type))
        {
            _services.Add(type, service);
        }
    }

    // Get a service from the Service Locator
    public T GetService<T>()
    {
        var type = typeof(T);
        if (_services.TryGetValue(type, out var service))
        {
            return (T)service;
        }
        else
        {
            throw new Exception($"Service of type {type} not registered.");
        }
    }

    // Unregister a service
    public void UnregisterService<T>()
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
        {
            _services.Remove(type);
        }
    }
}
