using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        // Register BuildingManager with the Service Locator
        ServiceLocator.Instance.RegisterService(new BuildingManager());
    }
}

