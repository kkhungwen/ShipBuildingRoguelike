using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileObjectTypeList_", menuName = "Scriptable Objects/Custom Tile Map Editor/List/Tile Object Type List")]
public class TileObjectTypeListSO : ScriptableObject
{
    // Tile object type list used by tile map as tile pallete
    public List<TileObjectTypeSO> list;

    #region Validation
#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(list), list);
    }

#endif
    #endregion
}
