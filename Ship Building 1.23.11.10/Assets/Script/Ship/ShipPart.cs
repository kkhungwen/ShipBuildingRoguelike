using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#region RQUIER COMPONENTS
[DisallowMultipleComponent]
#endregion REQUIER COMPONENTS
public class ShipPart : MonoBehaviour
{
    private ShipPartData shipPartData;

    public event Action OnInitialize;

    public void Initialize(ShipPartData shipPartData, TileObjectTypeSO tileObjectType,Transform parentTransform, Vector2Int shipGridPosition, Orientation.orientation orientation)
    {
        this.shipPartData = shipPartData;

        UpdateTransform(parentTransform, tileObjectType, shipGridPosition, orientation);

        OnInitialize?.Invoke();
    }

    public void UpdateTransform(Transform parentTranform, TileObjectTypeSO tileObjectType, Vector2Int shipGridPosition, Orientation.orientation orientation)
    {
        transform.parent = parentTranform;
        Vector2 shipSpacePosition = (shipGridPosition - tileObjectType.GetRotatedCenterPointOffsetUnityCordinate(orientation)) * Settings.cellSize;
        transform.localPosition = shipSpacePosition;
        transform.localRotation = Quaternion.Euler(0, 0, Orientation.GetOrientationAngle(orientation));
    }

    public float GetAttributeValue(AttributeTypeSO attributeType)
    {
        return shipPartData.GetAttributeValue(attributeType);
    }
}
