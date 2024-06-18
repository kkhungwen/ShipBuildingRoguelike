using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BreakableBlock : MonoBehaviour, IWorldObjectInstance
{
    [SerializeField] private SpriteRenderer baseSpriteRenderer;

    public event Action<IWorldObjectInstance.OnInitializeEventArgs> OnInitialize;

    public void Initialize(TileObjectTypeSO tileObjectType, Vector2Int worldCellGridPosition, Orientation.orientation orientation, Sprite sprite)
    {
        UpdateTransform(tileObjectType, worldCellGridPosition, orientation);
        baseSpriteRenderer.sprite = sprite;

        OnInitialize?.Invoke(new IWorldObjectInstance.OnInitializeEventArgs() { tileObjectType = tileObjectType});
    }

    public void UpdateTransform(TileObjectTypeSO tileObjectType, Vector2Int worldCellGridPosition, Orientation.orientation orientation)
    {
        Vector2 worldPosition = (worldCellGridPosition - tileObjectType.GetRotatedCenterPointOffsetUnityCordinate(orientation)) * Settings.cellSize;
        transform.localPosition = worldPosition;
        transform.localRotation = Quaternion.Euler(0, 0, Orientation.GetOrientationAngle(orientation));
    }
}
