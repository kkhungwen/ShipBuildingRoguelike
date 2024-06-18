using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWorldObject
{
    // Call to initialize when grid position is reletive to world grid position
    public void InitializeGridPositionOrientation(Vector2Int gridPositon, Orientation.orientation orientation);

    // Call by chunk to publish chunk events
    public void SubscribeToChunkEvents(Chunk chunk);

    public TileObjectTypeSO GetTileObjectType();

    public List<Vector2Int> GetGridPositionList();
}
