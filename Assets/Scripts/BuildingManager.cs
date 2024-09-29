using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour 
{
    private GameObject[] _buildingGameObjects;
    private List<Building> _buildingScripts = new List<Building>();

    public void Start() 
    {
        ServiceLocator.Instance.RegisterService(this);
        _buildingGameObjects = GameObject.FindGameObjectsWithTag("TownCenter");
        foreach (GameObject building in _buildingGameObjects) 
        {
            Building buildingScript = building.GetComponent<Building>();
            if (buildingScript != null)
            {
                _buildingScripts.Add(buildingScript);
            }
            else
            {
                Debug.LogWarning($"GameObject {building.name} with tag 'TownCenter' has no Building component.");
            }
        }
    }
    public Building GiveBuilding()
    {
        Building reference;
        System.Random random = new System.Random();
        int randomPosition = random.Next(0, _buildingScripts.Count);
        if (!_buildingScripts[randomPosition]._isAlive)
        {
            reference = GiveBuilding();
            return reference;
        }
        reference = _buildingScripts[randomPosition];
        return reference;
    }
}
