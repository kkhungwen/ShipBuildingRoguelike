using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChunkTileMapList_", menuName = "Scriptable Objects/Custom Tile Map Editor/List/Chunk Tile Map List")]
public class ChunkTileMapListSO : ScriptableObject
{
    // Chunk list used by map generator to get random chunk map
    public List<ChunkTileMapSO> list;

    #region Validation
#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(list), list);
    }

#endif
    #endregion
}
