using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClusterTileMapList_", menuName = "Scriptable Objects/Custom Tile Map Editor/List/Cluster Tile Map List")]
public class ClusterTileMapListSO : ScriptableObject
{
    // Cluster list used by map generator to get random cluster map 
    public List<ClusterTileMapSO> list;

    #region Validation
#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(list), list);
    }

#endif
    #endregion
}
