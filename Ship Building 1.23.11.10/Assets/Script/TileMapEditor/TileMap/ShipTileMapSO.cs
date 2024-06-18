using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipTileMap_", menuName = "Scriptable Objects/Custom Tile Map Editor/Tile Map/Ship")]
public class ShipTileMapSO : TileMapSO
{
    [Space(10f)]
    [Header("SHIP TILE MAP")]
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
        HelperUtilities.ValidateCheckPositiveValue(this,nameof(mapHeight),mapHeight, false);

        // Check if tile pallete contains none shippart tileObjectType
        foreach (TileObjectTypeListSO tileObjectTypeList in tileObjectTypePalleteArray)
        {
            foreach (TileObjectTypeSO tileObjectType in tileObjectTypeList.list)
            {
                if (tileObjectType.GetType() != typeof(ShipPartTypeSO))
                {
                    Debug.Log(this + "ship tilemap tile pallete contains none shippart TileObjectType");
                }
            }
        }
    }
#endif
    #endregion Validation

}
