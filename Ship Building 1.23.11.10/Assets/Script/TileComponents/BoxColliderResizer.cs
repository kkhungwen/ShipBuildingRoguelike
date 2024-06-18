using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BoxColliderResizer : MonoBehaviour
{
    private IWorldObjectInstance worldObjectInstance;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        worldObjectInstance = GetComponentInParent<IWorldObjectInstance>();

        if (worldObjectInstance != null)
            worldObjectInstance.OnInitialize += WorldObjectInstance_OnInitialize; ;
    }

    private void OnDestroy()
    {
        if (worldObjectInstance != null)
            worldObjectInstance.OnInitialize -= WorldObjectInstance_OnInitialize;
    }

    private void WorldObjectInstance_OnInitialize(IWorldObjectInstance.OnInitializeEventArgs onInitializeEventArgs)
    {
        TileObjectTypeSO tileObjectType = onInitializeEventArgs.tileObjectType;

        ResizeBoxCollider(tileObjectType.initialRotationPointOffset * 2, tileObjectType.initialRotationPointOffset);
    }


    private void ResizeBoxCollider(Vector2 widthHeight,Vector2 offset)
    {
        boxCollider.size = widthHeight;
        boxCollider.offset = offset;
    }
}
