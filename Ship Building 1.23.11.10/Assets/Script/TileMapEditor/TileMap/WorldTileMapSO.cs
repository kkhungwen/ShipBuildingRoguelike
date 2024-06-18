using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WorldTileMap_", menuName = "Scriptable Objects/Custom Tile Map Editor/Tile Map/World")]
public class WorldTileMapSO : TileMapSO
{
    [Header("WORLD TILE MAP")]
    public int mapWidth;
    public int mapHeight;

    #region EditorCode
#if UNITY_EDITOR
    public override void GetMapWidthHeight(out int width, out int height)
    {
        width = mapWidth;
        height = mapHeight;
    }
#endif
    #endregion EditorCode

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(mapWidth), mapWidth, false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(mapHeight), mapHeight, false);

        // Check if tile pallete contains none chunk tileObjectType
        foreach (TileObjectTypeListSO tileObjectTypeList in tileObjectTypePalleteArray)
        {
            foreach (TileObjectTypeSO tileObjectType in tileObjectTypeList.list)
            {
                if (tileObjectType.GetType() != typeof(ChunkTypeSO))
                {
                    Debug.Log(this + "world tilemap tile pallete contains none chunk TileObjectType");
                }
            }
        }
    }
#endif
    #endregion Validation
}
