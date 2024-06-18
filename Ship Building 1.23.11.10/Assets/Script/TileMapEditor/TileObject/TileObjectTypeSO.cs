using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for containing unchangable values for tileObject. Should only contains parameters for game logic. Should be inherite to contain differant values for different type of tileObject
/// </summary>
public abstract class TileObjectTypeSO : ScriptableObject
{
    [Space(10f)]
    [Header("BASIC")]
    public string tileObjectTypeName;

    [Space(10f)]
    [Header("GRID")]
    #region Tooltip
    [Tooltip(" Is orientation editable")]
    #endregion
    public bool isRotatable;

    #region Tooltip
    [Tooltip("Initial grid position at orientation east.")]
    #endregion
    public List<Vector2Int> initialGridPositionList;
    public Vector2Int[] initialGridPositionArray;

    #region ToolTip
    [Tooltip("Grid center point relative to the bottom left corner. Used for cordinate with grid system")]
    #endregion
    public Vector2 initialCenterPointOffset;

    #region ToolTip
    [Tooltip("Grid rotation point relative to the bottom left corner. Used for mouse anchoring and rotation")]
    #endregion
    public Vector2 initialRotationPointOffset;

    #region Tooltip
    [Tooltip("Initial surrounding position sould conect to the edge of initial grid position list")]
    #endregion
    [HideInInspector] public List<Vector2Int> initialSurroundingPositionList;
    [HideInInspector] public Vector2Int[] initialSurroundingPositionArray;


    // Get initialGridPositionList after rotation 
    public List<Vector2Int> GetRotatedPositionListUnityCordinate(List<Vector2Int> list, Orientation.orientation orientation)
    {
        list.Clear();

        foreach (Vector2Int initialPosition in initialGridPositionList)
        {
            list.Add(Vector2Int.RoundToInt(Orientation.GetRotatedPointUnityCordinate(initialPosition, orientation)));
        }

        return list;
    }
    public Vector2Int[] GetRotatedPositionArrayUnityCordinate(Vector2Int[] arrayToFill, Orientation.orientation orientation)
    {
        if (arrayToFill.Length != initialGridPositionArray.Length)
            arrayToFill = new Vector2Int[initialGridPositionArray.Length];

        for (int i = 0; i < initialGridPositionArray.Length; i++)
        {
            arrayToFill[i] = Vector2Int.RoundToInt(Orientation.GetRotatedPointUnityCordinate(initialGridPositionArray[i], orientation));
        }

        return arrayToFill;
    }

    // Get grid position list after rotation + centerpoint gridposition
    public List<Vector2Int> GetGridPositionListUnityCordinate(List<Vector2Int> list, Vector2Int gridPosition, Orientation.orientation orientation)
    {
        List<Vector2Int> rotatePositionList = GetRotatedPositionListUnityCordinate(list, orientation);

        for (int i = 0; i < rotatePositionList.Count; i++)
        {
            rotatePositionList[i] += gridPosition;
        }

        return list;
    }
    public Vector2Int[] GetGridPositionArrayUnityCordinate(Vector2Int[] arrayToFill, Orientation.orientation orientation, Vector2Int gridPosition)
    {
        arrayToFill = GetRotatedPositionArrayUnityCordinate(arrayToFill, orientation);

        for (int i = 0; i < arrayToFill.Length; i++)
        {
            arrayToFill[i] += gridPosition;
        }

        return arrayToFill;
    }

    // Get surrounding position list after rotation
    public List<Vector2Int> GetRotatedSurroundingPositionListUnityCordinate(List<Vector2Int> list, Vector2Int gridPosition, Orientation.orientation orientation)
    {
        list.Clear();

        foreach (Vector2Int initialSurroundingPosition in initialSurroundingPositionList)
        {
            list.Add(Vector2Int.RoundToInt(Orientation.GetRotatedPointUnityCordinate(initialSurroundingPosition, orientation)) + gridPosition);
        }

        return list;
    }
    public Vector2Int[] GetRotatedSurroundingPositionArrayUnityCordinate(Vector2Int[] arrayToFill, Vector2Int gridPosition, Orientation.orientation orientation)
    {
        if (arrayToFill.Length != initialSurroundingPositionArray.Length)
            arrayToFill = new Vector2Int[initialSurroundingPositionArray.Length];

        for (int i = 0; i < initialSurroundingPositionArray.Length; i++)
        {
            arrayToFill[i] = Vector2Int.RoundToInt(Orientation.GetRotatedPointUnityCordinate(initialSurroundingPositionArray[i], orientation)) + gridPosition;
        }

        return arrayToFill;
    }

    //Get rotated centerpoint ofset
    public Vector2 GetRotatedCenterPointOffsetUnityCordinate(Orientation.orientation orientation)
    {
        Vector2 rotateCenterPointOffeset = Orientation.GetRotatedPointUnityCordinate(initialCenterPointOffset, orientation);

        return rotateCenterPointOffeset;
    }


    #region Editor Code
#if UNITY_EDITOR

    [Space(10f)]
    [Header("EDITOR")]
    #region Tooltip
    [Tooltip("Texture2D to show on the editor")]
    #endregion
    public Texture2D previewTexture;

    #region Tooltip
    [Tooltip("Initial grid position at orientation east in gui cordinate. y is reversed")]
    #endregion
    [HideInInspector] public List<Vector2Int> initialGridPositionListGUI;

    #region ToolTip
    [Tooltip("Grid center point relative to the top left corner in gui cordinate. Used for cordinate with grid system")]
    #endregion
    [HideInInspector] public Vector2 initialCenterPointOffsetGUI;

    #region ToolTip
    [Tooltip("Grid rotation point relative to the top left corner in gui cordinate. Used for mouse anchoring and rotation")]
    #endregion
    [HideInInspector] public Vector2 initialRotationPointOffsetGUI;



    //Create TileObjectSO. return TileObjectSO for tile editor to edit. Should be overide to create different type by child 
    public abstract TileObjectSO CreateTileObject();



    //Get initialGridPositionList after rotation 
    public List<Vector2Int> GetRotatedPositionListGUI(Orientation.orientation orientation)
    {
        List<Vector2Int> list = new List<Vector2Int>();

        foreach (Vector2Int initialPosition in initialGridPositionListGUI)
        {
            list.Add(Vector2Int.RoundToInt(Orientation.GetRotatedPointGUI(initialPosition, orientation)));
        }

        return list;
    }

    public Vector2 GetRotatedCenterPointOffsetGUI(Orientation.orientation orientation)
    {
        Vector2 rotateCenterPointOffeset = Orientation.GetRotatedPointGUI(initialCenterPointOffsetGUI, orientation);

        return rotateCenterPointOffeset;
    }

    public Vector2 GetRotatedRotationPointOffsetGUI(Orientation.orientation orientation)
    {
        Vector2 rotateRotationPointOffeset = Orientation.GetRotatedPointGUI(initialRotationPointOffsetGUI, orientation);

        return rotateRotationPointOffeset;
    }

    public List<Vector2Int> GetGridPositionListGUI(Vector2Int gridPosition, Orientation.orientation orientation)
    {
        List<Vector2Int> list = new List<Vector2Int>();

        foreach (Vector2Int initialRotatePosition in GetRotatedPositionListGUI(orientation))
        {
            list.Add(initialRotatePosition + gridPosition);
        }

        return list;
    }
#endif
    #endregion


    #region Validation
#if UNITY_EDITOR

    protected virtual void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(tileObjectTypeName), tileObjectTypeName);
        HelperUtilities.ValidateCheckNullReference(this, nameof(previewTexture), previewTexture);
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(initialGridPositionList), initialGridPositionList);
        HelperUtilities.ValidateCheckZeroVector2(this, nameof(initialCenterPointOffset), initialCenterPointOffset);
        if (isRotatable)
            HelperUtilities.ValidateCheckZeroVector2(this, nameof(initialRotationPointOffset), initialRotationPointOffset);

        initialSurroundingPositionList = CaculateInitialSurroundingPositionList();
        initialSurroundingPositionArray = CreateInitialSurroundingPositionArray(initialSurroundingPositionList);

        initialGridPositionListGUI = CaculateVector2IntList_UnityCordinate_GUICordinate(initialGridPositionList);
        initialCenterPointOffsetGUI = CaculateVector2_UnityCordinate_GUICordinate(initialCenterPointOffset);
        initialRotationPointOffsetGUI = CaculateVector2_UnityCordinate_GUICordinate(initialRotationPointOffset);
    }

    private List<Vector2Int> CaculateInitialSurroundingPositionList()
    {
        List<Vector2Int> list = new List<Vector2Int>();

        foreach (Vector2Int position in initialGridPositionList)
        {
            List<Vector2Int> surroundingPerPosition = new List<Vector2Int>() { new Vector2Int(position.x + 1, position.y) , new Vector2Int(position.x + 1, position.y-1), new Vector2Int(position.x , position.y-1),
                new Vector2Int(position.x - 1, position.y-1), new Vector2Int(position.x - 1, position.y), new Vector2Int(position.x - 1, position.y+1), new Vector2Int(position.x , position.y+1), new Vector2Int(position.x + 1, position.y+1) };

            foreach (Vector2Int positionPerPosition in surroundingPerPosition)
            {
                if (!initialGridPositionList.Contains(positionPerPosition) && !list.Contains(positionPerPosition))
                {
                    list.Add(positionPerPosition);
                }
            }
        }

        return list;
    }

    private Vector2Int[] CreateInitialSurroundingPositionArray(List<Vector2Int> list)
    {
        Vector2Int[] initialSurroundingPositionArray = new Vector2Int[list.Count];

        for (int i = 0; i < list.Count; i++)
        {
            initialSurroundingPositionArray[i] = list[i];
        }

        return initialSurroundingPositionArray;
    }

    private Vector2 CaculateVector2_UnityCordinate_GUICordinate(Vector2 inputPosition)
    {
        Vector2 newPosition = new Vector2(inputPosition.x, -inputPosition.y);

        return newPosition;
    }

    private List<Vector2Int> CaculateVector2IntList_UnityCordinate_GUICordinate(List<Vector2Int> inputList)
    {
        List<Vector2Int> outputList = new List<Vector2Int>();

        foreach (Vector2Int position in inputList)
        {
            Vector2Int newPosition = new Vector2Int(position.x, -position.y);
            outputList.Add(newPosition);
        }

        return outputList;
    }
#endif
    #endregion
}
