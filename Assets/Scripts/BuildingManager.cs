using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour 
{
    [SerializeField] private GameObject[] _buildingGameObjects;
    private List<Building> _buildingScripts = new List<Building>();

    public void OnEnable() 
    {
        ServiceLocator.Instance.RegisterService(this);
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
