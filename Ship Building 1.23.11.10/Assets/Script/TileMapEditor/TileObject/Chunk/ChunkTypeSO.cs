using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChunkType_", menuName = "Scriptable Objects/Custom Tile Map Editor/Tile Object Type/Chunk")]
public class ChunkTypeSO : TileObjectTypeSO
{
    #region Editor Code
#if UNITY_EDITOR
    public override TileObjectSO CreateTileObject()
    {
        TileObjectSO tileObject = ScriptableObject.CreateInstance<ChunkSO>();
        return tileObject;
    }
#endif
    #endregion
}
