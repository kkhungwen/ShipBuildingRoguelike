using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFirable 
{
    public void FireInitialize(Vector3 startPosition, Vector3 directionVector, float angle, Transform parentTransform);
}
