using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChunkTileMap_", menuName = "Scriptable Objects/Custom Tile Map Editor/Tile Map/Chunk")]
public class ChunkTileMapSO : TileMapSO
{
    [Space(10f)]
    [Header("CHUNK TILE MAP")]
    public ChunkTypeSO chunkType;

    #region EditorCode
#if UNITY_EDITOR
    public override void GetMapWidthHeight(out int width, out int height)
    {
        width = Settings.chunkWidth;
        height = Settings.chunkHeight;
    }
#endif
    #endregion EditorCode

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullReference(this, nameof(chunkType), chunkType);

        // Check if tile pallete contains none breakableBlock or cluster tileObjectType
        foreach (TileObjectTypeListSO tileObjectTypeList in tileObjectTypePalleteArray)
        {
            foreach (TileObjectTypeSO tileObjectType in tileObjectTypeList.list)
            {
                if (!(tileObjectType.GetType() == typeof(ClusterTypeSO) || tileObjectType.GetType() == typeof(BreakableBlockTypeSO)))
                {
                    Debug.Log(this + "chunk tilemap tile pallete contains none cluster or breakableBlock TileObjectType");
                }
            }
        }
    }
#endif
    #endregion Validation
}
