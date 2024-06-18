using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLoader : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private WorldGenerator worldGenerator;

    private CustomGrid<Chunk> worldGrid;

    // seconds to wait to check if target change the grid position. Used to replace update
    private WaitForSeconds waitToCheckTargetPosition = new WaitForSeconds(.5f);
    // map chunk grid offset by half the grid size. used for determine which chumk to load
    private Vector2Int currentTargetGridPosition = new Vector2Int(-10000, -10000);

    [SerializeField] private List<Vector2Int> toLoadChunkIndexList = new List<Vector2Int>();
    [SerializeField] private List<Vector2Int> toUnloadChunkIndexList = new List<Vector2Int>();


    private void Start()
    {
        worldGrid = worldGenerator.GenerateWorldGrid();
        StartCoroutine(CheckIfTargetPositionChangeRoutine());
    }


    /// <summary>
    /// Checks if target grid position changed every loop. Used to create a slower Update
    /// </summary>
    private IEnumerator CheckIfTargetPositionChangeRoutine()
    {
        while (true)
        {
            CheckIfTargetPositionChange();

            yield return waitToCheckTargetPosition;
        }
    }

    private void CheckIfTargetPositionChange()
    {
        float cellSize = worldGrid.GetCellSize();
        worldGrid.GetXY(targetTransform.position - new Vector3(cellSize, cellSize, 0) / 2, out int x, out int y);
        Vector2Int checkTargetGridPosition = new Vector2Int(x, y);

        if (checkTargetGridPosition != currentTargetGridPosition)
        {
            UpdateLoadUnloadIndexList(checkTargetGridPosition);
            currentTargetGridPosition = checkTargetGridPosition;

            UnloadLoadChunk();
        }
    }

    private void UpdateLoadUnloadIndexList(Vector2Int newLoadGridPosition)
    {
        List<Vector2Int> oldToLoadChunkIndexList = new List<Vector2Int>
        {
            new Vector2Int(currentTargetGridPosition.x,currentTargetGridPosition.y),
            new Vector2Int(currentTargetGridPosition.x+1,currentTargetGridPosition.y),
            new Vector2Int(currentTargetGridPosition.x+1,currentTargetGridPosition.y+1),
            new Vector2Int(currentTargetGridPosition.x,currentTargetGridPosition.y+1),
        };

        List<Vector2Int> newToLoadChunkIndexList = new List<Vector2Int>
        {
            new Vector2Int(newLoadGridPosition.x,newLoadGridPosition.y),
            new Vector2Int(newLoadGridPosition.x+1,newLoadGridPosition.y),
            new Vector2Int(newLoadGridPosition.x+1,newLoadGridPosition.y+1),
            new Vector2Int(newLoadGridPosition.x,newLoadGridPosition.y+1),
        };

        toUnloadChunkIndexList = new List<Vector2Int>();
        foreach (Vector2Int index in oldToLoadChunkIndexList)
        {
            toUnloadChunkIndexList.Add(index);
        }

        foreach (Vector2Int toLoadIndex in newToLoadChunkIndexList)
        {
            toUnloadChunkIndexList.Remove(toLoadIndex);
        }

        foreach (Vector2Int oldToLoadIndex in oldToLoadChunkIndexList)
        {
            newToLoadChunkIndexList.Remove(oldToLoadIndex);
        }

        toLoadChunkIndexList = newToLoadChunkIndexList;
    }


    private void UnloadLoadChunk()
    {
        foreach(Vector2Int index in toUnloadChunkIndexList)
        {
            if(worldGrid.TryGetValue(index.x,index.y,out Chunk chunk))
            {
                chunk.UnloadChunk();
            }
        }

        foreach (Vector2Int index in toLoadChunkIndexList)
        {
            if (worldGrid.TryGetValue(index.x, index.y, out Chunk chunk))
            {
                chunk.LoadChunk();
            }
        }
    }
}
