using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TileMap_", menuName = "Scriptable Objects/Custom Tile Map Editor/Tile Map")]
public abstract class TileMapSO : ScriptableObject
{
    [HideInInspector] public List<TileObjectSO> tileObjectList = new List<TileObjectSO>();

    #region EditorCode
#if UNITY_EDITOR

    #region Tooltip
    [Tooltip("populate with TileObjectTypeListSO as tile palletes for tile map")]
    #endregion Tooltip
    public TileObjectTypeListSO[] tileObjectTypePalleteArray;

    // Get the width & height of the map from Settings
    public abstract void GetMapWidthHeight(out int width, out int height);

#endif
    #endregion EditorCode
}
