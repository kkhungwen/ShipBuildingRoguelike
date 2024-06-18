using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSO : TileObjectSO
{
    public override object CreateTileObjectData()
    {
        object chunkData = new ChunkData(tileObjectType as ChunkTypeSO, gridPositionUnityCordinate);

        return chunkData;    
    }
}
