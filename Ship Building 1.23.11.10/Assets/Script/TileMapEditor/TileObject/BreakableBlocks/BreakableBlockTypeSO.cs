using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BreakableBlockType_", menuName = "Scriptable Objects/Custom Tile Map Editor/Tile Object Type/Breakable Block")]
public class BreakableBlockTypeSO : TileObjectTypeSO
{
    [Space(10f)]
    [Header("BREAKABLE BLOCK")]

    #region Tooltip
    [Tooltip("Breackable block should contains shape of a rect")]
    #endregion
    public int blockWidth;
    public int blockHeight;

    #region Tooltip
    [Tooltip("prefab used as key for breakabledata to get breakable instance in object pool")]
    #endregion
    public GameObject prefab;

    #region Tooltip
    [Tooltip("defult sprite. pivot sould be set on bottom left")]
    #endregion
    public Sprite defaultSprite;

    #region Tooltip
    [Tooltip("is sprite rule tile. currently only available for 1x1 tiles")]
    #endregion
    public bool isSpriteRuleTile;
    public SpriteRuleTileSO spriteRuleTile;



    #region Editor Code
#if UNITY_EDITOR
    public override TileObjectSO CreateTileObject()
    {
        TileObjectSO tileObject = ScriptableObject.CreateInstance<BreakableBlockSO>();
        return tileObject;
    }
#endif
    #endregion

    #region Validation
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();

        HelperUtilities.ValidateCheckPositiveValue(this, nameof(blockWidth), blockWidth, false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(blockHeight), blockHeight, false);

        HelperUtilities.ValidateCheckNullReference(this, nameof(prefab), prefab);

        if (isSpriteRuleTile)
            HelperUtilities.ValidateCheckNullReference(this, nameof(spriteRuleTile), spriteRuleTile);
        else
            HelperUtilities.ValidateCheckNullReference(this, nameof(defaultSprite), defaultSprite);

        CaculateInitialGridPositionList();

        initialCenterPointOffset = new Vector2(0.5f, 0.5f);
        initialRotationPointOffset = new Vector2((float)blockWidth / 2, (float)blockHeight / 2);
    }


    /// <summary>
    /// Clculates the initial grid position base on the rect of the cluster
    /// </summary>
    private void CaculateInitialGridPositionList()
    {
        List<Vector2Int> list = new List<Vector2Int>();

        for (int x = 0; x < blockWidth; x++)
        {
            for (int y = 0; y < blockHeight; y++)
            {
                list.Add(new Vector2Int(x, y));
            }
        }

        initialGridPositionList = list;
    }
#endif
    #endregion Validation
}
