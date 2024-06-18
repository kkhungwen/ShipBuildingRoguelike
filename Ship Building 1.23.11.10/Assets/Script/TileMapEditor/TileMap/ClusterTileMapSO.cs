using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClusterTileMap_", menuName = "Scriptable Objects/Custom Tile Map Editor/Tile Map/Cluster")]
public class ClusterTileMapSO : TileMapSO
{
    [Space(10f)]
    [Header("CLUSTER TILE MAP")]
    public ClusterTypeSO clusterType;

    #region EditorCode
#if UNITY_EDITOR
    public override void GetMapWidthHeight(out int width, out int height)
    {
        width = clusterType.clusterWidth;
        height = clusterType.clusterHeight;
    }
#endif
    #endregion EditorCode

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullReference(this, nameof(clusterType), clusterType);

        // Check if tile pallete contains none breakableBlock tileObjectType
        foreach (TileObjectTypeListSO tileObjectTypeList in tileObjectTypePalleteArray)
        {
            foreach (TileObjectTypeSO tileObjectType in tileObjectTypeList.list)
            {
                if (tileObjectType.GetType() != typeof(BreakableBlockTypeSO))
                {
                    Debug.Log(this + "cluster tilemap tile pallete contains none breakableBlock TileObjectType");
                }
            }
        }
    }
#endif
    #endregion Validation
}
