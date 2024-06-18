using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chunk 
{
    public CustomGrid<ChunkGridObject> grid;
    public Vector2Int worldGridPosition;

    public event Action OnLoad;
    public event Action OnUnload;

    public event Action<CustomGrid<ChunkGridObject>> OnInitializeRuleSprite;

    /// <summary>
    /// Load and unload chunk, called by chunk loader
    /// </summary>
    public void LoadChunk()
    {
        OnLoad?.Invoke();
    }
    public void UnloadChunk()
    {
        OnUnload?.Invoke();
    }

    // Initializing all world object, called after world generation
    public void InitializeIWorldObject()
    {
        OnInitializeRuleSprite?.Invoke(grid);
    }

    // Initializing chunk, called by world generator wwhen generating chunk grid
    public void Initialize(Vector2Int worldGridPosition)
    {
        this.worldGridPosition = worldGridPosition;
        grid = new CustomGrid<ChunkGridObject>(Settings.chunkWidth, Settings.chunkHeight, Settings.cellSize, (Vector2)worldGridPosition * Settings.chunkWidth * Settings.cellSize, () => new ChunkGridObject());
    }

    // Adding world object to corrisponding ChunkGridObject, and publish events to world object. Called by WorldGenerator after creating worldObjectData 
    public void AddWorldObjectToChunk(IWorldObject worldObject, Vector2Int gridPosition, Orientation.orientation orientation)
    {
        // Set chunk grid with world object
        List<Vector2Int> gridPositionList = worldObject.GetGridPositionList();

        foreach(Vector2Int position in gridPositionList)
        {
            if(grid.TryGetValue(position.x,position.y,out ChunkGridObject chunkGridObject))
                chunkGridObject.TryAddWorldObject(worldObject);
            
            else
                Debug.Log("position out of chunk grid range");
        }

        // Initialize Chunk Events
        worldObject.SubscribeToChunkEvents(this);
    } 
}
