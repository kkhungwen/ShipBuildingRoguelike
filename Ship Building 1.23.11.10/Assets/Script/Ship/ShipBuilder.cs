using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region RQUIER COMPONENTS
[DisallowMultipleComponent]
[RequireComponent(typeof(Ship))]
#endregion REQUIER COMPONENTS
public class ShipBuilder : MonoBehaviour
{
    Ship ship;

    private void Awake()
    {
        ship = GetComponent<Ship>();
    }

    #region SHIP BUILDING
    public CustomGrid<ShipGridObject> BuildShip(ShipTileMapSO shipTileMap)
    {
        CustomGrid<ShipGridObject> shipGrid = new CustomGrid<ShipGridObject>(shipTileMap.mapWidth, shipTileMap.mapHeight, Settings.cellSize, Vector3.zero, () => new ShipGridObject());

        PopulateShipGridWithShipObject(shipGrid, shipTileMap);

        return shipGrid;
    }

    
    private void PopulateShipGridWithShipObject(CustomGrid<ShipGridObject> shipGrid, ShipTileMapSO shipTileMap)
    {
        foreach (TileObjectSO tileObject in shipTileMap.tileObjectList)
        {
            if (TryCreatIShipObjectFromTileObject(tileObject, out IShipObject shipObject))
            {
                TryPlaceShipObject(shipObject, shipGrid);
            }
        }
    }

    // Try cast TileObjectData created by tile object to IShipObject
    private bool TryCreatIShipObjectFromTileObject(TileObjectSO tileObject, out IShipObject shipObject)
    {
        shipObject = tileObject.CreateTileObjectData() as IShipObject;

        if (shipObject != null)
        {
            return true;
        }
        else
        {
            Debug.Log("Cannot cast " + tileObject + "'s TileObjectData to IShipObject");
            return false;
        }
    }
    #endregion

    #region SHIP CONSTRUCTING
    // Place a new IShipObject to ship grid
    public bool TryPlaceShipObject(IShipObject shipObject, CustomGrid<ShipGridObject> shipGrid)
    {
        List<Vector2Int> occupiedPositionList = shipObject.GetGridPositionList();

        if (isShipGridPositionPlacable(occupiedPositionList, shipGrid))
        {
            AddShipObjectToShipGrid(shipObject, shipGrid, occupiedPositionList);

            shipObject.SubscribeToShipEvents(ship);

            return true;
        }

        return false;
    }


    private void AddShipObjectToShipGrid(IShipObject shipObject, CustomGrid<ShipGridObject> shipGrid, List<Vector2Int> occupiedPositionList)
    {
        foreach (Vector2Int position in occupiedPositionList)
        {
            if (shipGrid.TryGetValue(position.x, position.y, out ShipGridObject shipGridObject))
            {
                shipGridObject.TryAddShipObject(shipObject);
            }
        }
    }


    private bool isShipGridPositionPlacable(List<Vector2Int> checkPositionList, CustomGrid<ShipGridObject> shipGrid)
    {
        bool isPlacable = true;

        foreach(Vector2Int position in checkPositionList)
        {
            if (shipGrid.TryGetValue(position.x, position.y,out ShipGridObject shipGridObject))
            {
                if (!shipGridObject.IsShipObjectPlacable())
                {
                    Debug.Log("Ship grid object not placeable");

                    isPlacable = false;
                }
            }
            else
            {
                Debug.Log("Position out of ship grid range");

                isPlacable = false;
            }
        }

        return isPlacable;
    }
    #endregion
}
