using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding
{
    public bool _isAlive { get; set; }
    public Transform _transform { get; set; }
}
