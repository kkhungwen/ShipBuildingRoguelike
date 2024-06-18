using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGridObject
{
    private IWorldObject worldObject;

    public bool TryAddWorldObject(IWorldObject worldObject)
    {
        if (this.worldObject == null)
        {
            this.worldObject = worldObject;
            return true;
        }

        Debug.Log("Chunk grid object already contains world object");
        return false;
    }

    public bool TryGetWorldObject(out IWorldObject worldObject)
    {
        if (this.worldObject != null)
        {
            worldObject = this.worldObject;
            return true;
        }

        worldObject = null;
        return false;
    }
}
