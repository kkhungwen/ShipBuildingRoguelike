using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClusterType_", menuName = "Scriptable Objects/Custom Tile Map Editor/Tile Object Type/Cluster")]
public class ClusterTypeSO : TileObjectTypeSO
{
    [Space(10f)]
    [Header("CLUSTER")]
    public int clusterWidth;
    public int clusterHeight;
    #region ToolTip
    [Tooltip("is this cluster appear as random orientation in world generation")]
    #endregion
    public bool isRandomOrientation;

    #region Editor Code
#if UNITY_EDITOR
    public override TileObjectSO CreateTileObject()
    {
        TileObjectSO tileObject = ScriptableObject.CreateInstance<ClusterSO>();
        return tileObject;
    }
#endif
    #endregion

    #region Validation
#if UNITY_EDITOR

    protected override void OnValidate()
    {
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(clusterWidth), clusterWidth,false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(clusterHeight), clusterHeight, false);

        initialGridPositionList = CaculateInitialGridPositionList();
        initialGridPositionArray =  CreateInitialGridPositionArray(initialGridPositionList);

        initialCenterPointOffset = new Vector2(0.5f, 0.5f);
        initialRotationPointOffset = new Vector2((float)clusterWidth / 2, (float)clusterHeight / 2);

        ValidationCheckIsRandomOrientation();

        base.OnValidate();
    }

    /// <summary>
    /// Clculates the initial grid position base on the rect of the cluster
    /// </summary>
    private List<Vector2Int> CaculateInitialGridPositionList()
    {
        List<Vector2Int> initialGridPositionList = new List<Vector2Int>();

        for (int x = 0; x < clusterWidth; x++)
        {
            for (int y = 0; y < clusterHeight; y++)
            {
                initialGridPositionList.Add(new Vector2Int(x, y));
            }
        }

        return initialGridPositionList;
    }

    private Vector2Int[] CreateInitialGridPositionArray(List<Vector2Int> list)
    {
        Vector2Int[] initialGridPositionArray = new Vector2Int[list.Count];

        for(int i = 0; i< list.Count; i++)
        {
            initialGridPositionArray[i] = list[i];
        }

        return initialGridPositionArray;
    }

    /// <summary>
    /// Check if cluster is able to randomize orientation. width and height must be equal to randomize orientation;
    /// </summary>
    private void ValidationCheckIsRandomOrientation()
    {
        if (clusterWidth != clusterHeight && isRandomOrientation)
            Debug.Log(this + " width and height is not the same. Orientation cannot be random");

        if (isRandomOrientation && isRotatable)
            Debug.Log(this + " is rotatable. Orientation cannot be random");
    }

#endif
    #endregion
}
