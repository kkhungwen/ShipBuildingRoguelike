using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private WorldTileMapSO worldTileMapSO;
    [SerializeField] private ChunkTileMapListSO chunkTileMapList;
    [SerializeField] private ClusterTileMapListSO clusterTileMapList;


    public CustomGrid<Chunk> GenerateWorldGrid()
    {
        CustomGrid<Chunk> worldGrid = new CustomGrid<Chunk>(worldTileMapSO.mapWidth, worldTileMapSO.mapHeight, Settings.chunkWidth * Settings.cellSize, Vector3.zero, () => new Chunk());

        InitializeWorldGrid(worldGrid);

        PopulateChunkGridWithWorldObject(worldGrid, worldTileMapSO);

        InitializeChunkIWorldObject(worldGrid);

        return worldGrid;
    }

    private void InitializeWorldGrid(CustomGrid<Chunk> worldGrid)
    {
        for (int x = 0; x < worldGrid.GetWidth(); x++)
        {
            for (int y = 0; y < worldGrid.GetHeight(); y++)
            {
                worldGrid.GetValue(x, y).Initialize(new Vector2Int(x, y));
            }
        }
    }

    private void PopulateChunkGridWithWorldObject(CustomGrid<Chunk> worldGrid ,WorldTileMapSO worldTileMap)
    {
        foreach (ChunkSO chunkSO in worldTileMap.tileObjectList)
        {
            Chunk chunk = worldGrid.GetValue(chunkSO.gridPositionUnityCordinate.x, chunkSO.gridPositionUnityCordinate.y);

            ChunkTileMapSO chunkTileMap = GetRandomChunkTileMap(chunkSO.tileObjectType as ChunkTypeSO);

            // Loop through tile object in chunk tile map
            foreach (TileObjectSO ChunkTileMapTileObject in chunkTileMap.tileObjectList)
            {
                // If tile object is Cluster
                if (TryCastTileObjectTypeToClusterType(ChunkTileMapTileObject.tileObjectType, out ClusterTypeSO clusterType))
                {
                    ClusterTileMapSO clusterTileMap = GetRandomClusterTileMap(clusterType);

                    // if cluster is random orientation
                    if (clusterType.isRandomOrientation)
                    {
                        Orientation.orientation clusterOrientation = Orientation.GetRandomOrientation();

                        foreach (TileObjectSO ClusterTileMapTileObject in clusterTileMap.tileObjectList)
                        {
                            // If tile object in cluster is world object
                            if (TryCreateIWorldObjectFromTileObject(ClusterTileMapTileObject, out IWorldObject worldObject))
                            {
                                // Initializing IWorldObject position in chunk space and orientation
                                Vector2 clusterRotateTileObjectPosition = Orientation.GetRotatedPointInSquareShape(ClusterTileMapTileObject.gridPositionUnityCordinate, clusterType.clusterWidth, clusterOrientation);
                                Vector2Int gridPositionChunkSpace = ChunkTileMapTileObject.gridPositionUnityCordinate + Vector2Int.RoundToInt(clusterRotateTileObjectPosition);                              
                                Orientation.orientation orientationChunkSpace = Orientation.GetOrientationWithinOrientation(ClusterTileMapTileObject.orientation, clusterOrientation);

                                worldObject.InitializeGridPositionOrientation(gridPositionChunkSpace, orientationChunkSpace);

                                //Set the Iworld object value in chunk grid object
                                chunk.AddWorldObjectToChunk(worldObject, gridPositionChunkSpace, orientationChunkSpace);
                            }
                        }
                    }
                    // if not cluster is random orientation
                    else
                    {
                        foreach (TileObjectSO ClusterTileMapTileObject in clusterTileMap.tileObjectList)
                        {
                            // If tile object in cluster is world object
                            if (TryCreateIWorldObjectFromTileObject(ClusterTileMapTileObject, out IWorldObject worldObject))
                            {
                                // Initializing IWorldObject position in chunk space and orientation
                                Vector2 clusterRotateTileObjectPosition = Orientation.GetRotatedPointUnityCordinate(ClusterTileMapTileObject.gridPositionUnityCordinate, ChunkTileMapTileObject.orientation);
                                Vector2Int gridPositionChunkSpace = ChunkTileMapTileObject.gridPositionUnityCordinate + Vector2Int.RoundToInt(clusterRotateTileObjectPosition);
                                Orientation.orientation orientationChunkSpace = Orientation.GetOrientationWithinOrientation(ClusterTileMapTileObject.orientation, ChunkTileMapTileObject.orientation);

                                worldObject.InitializeGridPositionOrientation(gridPositionChunkSpace, orientationChunkSpace);

                                //Set the Iworld object value in chunk grid object
                                chunk.AddWorldObjectToChunk(worldObject, gridPositionChunkSpace, orientationChunkSpace);
                            }
                        }
                    }
                }
                // If tile object is world object
                else if (TryCreateIWorldObjectFromTileObject(ChunkTileMapTileObject,out IWorldObject worldObject))
                {
                    // Initializing IWorldObject position in chunk space and orientation
                    worldObject.InitializeGridPositionOrientation(ChunkTileMapTileObject.gridPositionUnityCordinate, ChunkTileMapTileObject.orientation);

                    //Set the Iworld object value in chunk grid object
                    chunk.AddWorldObjectToChunk(worldObject, ChunkTileMapTileObject.gridPositionUnityCordinate, ChunkTileMapTileObject.orientation);
                }
            }
        }
    }

    private void InitializeChunkIWorldObject(CustomGrid<Chunk> worldGrid)
    {
        foreach(Chunk chunk in worldGrid.GetValues())
        {
            chunk.InitializeIWorldObject();
        }
    }


    private bool TryCastTileObjectTypeToClusterType(TileObjectTypeSO tileObjectType, out ClusterTypeSO clusterType)
    {
        clusterType = tileObjectType as ClusterTypeSO;

        if (clusterType != null)
        {
            return true;
        }
        else
        {
            Debug.Log("Cannot cast " + tileObjectType + " to ClusterTypeSO");
            return false;
        }
    }

    private bool TryCreateIWorldObjectFromTileObject(TileObjectSO tileObject, out IWorldObject worldObject)
    {
        worldObject = tileObject.CreateTileObjectData() as IWorldObject;

        if(worldObject != null)
        {
            return true;
        }
        else
        {
            Debug.Log("Cannot cast " + tileObject + " TileObjectData to IWorldObject");
            return false;
        }
    }



    private ChunkTileMapSO GetRandomChunkTileMap(ChunkTypeSO chunkType)
    {
        List<ChunkTileMapSO> chunkTileMapList = new List<ChunkTileMapSO>();

        foreach (ChunkTileMapSO chunkTileMap in this.chunkTileMapList.list)
        {
            if (chunkTileMap.chunkType == chunkType)
            {
                chunkTileMapList.Add(chunkTileMap);
            }
        }

        if (chunkTileMapList.Count != 0)
        {
            ChunkTileMapSO chunkTileMap = chunkTileMapList[Random.Range(0, chunkTileMapList.Count)];
            return chunkTileMap;
        }
        else
        {
            Debug.Log("No chunk type " + chunkType + " tilemap in mapChunkTileMapList");
            return null;
        }
    }

    private ClusterTileMapSO GetRandomClusterTileMap(ClusterTypeSO clusterType)
    {
        List<ClusterTileMapSO> clusterTileMapList = new List<ClusterTileMapSO>();

        foreach (ClusterTileMapSO clusterTileMap in this.clusterTileMapList.list)
        {
            if (clusterTileMap.clusterType == clusterType)
            {
                clusterTileMapList.Add(clusterTileMap);
            }
        }

        if (clusterTileMapList.Count != 0)
        {
            ClusterTileMapSO clusterTileMap = clusterTileMapList[Random.Range(0, clusterTileMapList.Count)];
            return clusterTileMap;
        }
        else
        {
            Debug.Log("No cluster type " + clusterType + " tilemap in clusterTileMapList");
            return null;
        }
    }
}
