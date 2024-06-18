using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipObject 
{
    public TileObjectTypeSO GetTileObjectType();

    public List<Vector2Int> GetGridPositionList();

    public void SubscribeToShipEvents(Ship ship);
}
