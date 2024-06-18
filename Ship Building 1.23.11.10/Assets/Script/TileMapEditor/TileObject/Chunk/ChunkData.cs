using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkData 
{
    public ChunkData(ChunkTypeSO chunkType, Vector2Int gridPosition)
    {
        this.chunkType = chunkType;
        this.gridPosition = gridPosition;
    }

    public ChunkTypeSO chunkType;
    public Vector2Int gridPosition;
}
