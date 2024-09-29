using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClone 
{
    public object Clone(Vector3 position, Quaternion rotation);
}
