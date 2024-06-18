using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Contains changable ingame values for tileObject, Create by corrisponding TileObjectTypeSO inheritance. Should be inherite to contain different data for tileObject 
/// </summary>
public abstract class TileObjectSO : ScriptableObject
{
    [HideInInspector] public TileObjectTypeSO tileObjectType;
    public Vector2Int gridPositionUnityCordinate;
    public Orientation.orientation orientation;

    // Create the pre instantiate data class for tileObject
    public abstract object CreateTileObjectData();

    #region Editor Code
#if UNITY_EDITOR
    public Vector2Int gridPositionGUI;

    public void Initialize(TileObjectTypeSO tileObjectType, Vector2Int mousrGridPosition, Orientation.orientation orientation)
    {
        this.tileObjectType = tileObjectType;
        this.name = tileObjectType.name;
        this.gridPositionGUI = mousrGridPosition;
        this.orientation = orientation;
    }

    public void Draw(int tilePixelSize,Vector2 graphOffset)
    {
        Vector2 unrotatedRotationPosition = graphOffset + (gridPositionGUI + new Vector2(0.5f, 0.5f) - tileObjectType.GetRotatedCenterPointOffsetGUI(orientation) + tileObjectType.GetRotatedRotationPointOffsetGUI(orientation)) * tilePixelSize;
        GUIUtility.RotateAroundPivot(-Orientation.GetOrientationAngle(orientation), unrotatedRotationPosition);
        GUI.DrawTexture(GetRect(unrotatedRotationPosition - tileObjectType.initialRotationPointOffset * tilePixelSize, tilePixelSize), tileObjectType.previewTexture);
        GUIUtility.RotateAroundPivot(Orientation.GetOrientationAngle(orientation), unrotatedRotationPosition);
    }

    public void DrawSelectedBackGround(int tilePixelSize, Vector2 graphOffset)
    {
        Vector2 unrotatedRotationPosition = graphOffset + (gridPositionGUI + new Vector2(0.5f, 0.5f) - tileObjectType.GetRotatedCenterPointOffsetGUI(orientation) + tileObjectType.GetRotatedRotationPointOffsetGUI(orientation)) * tilePixelSize;
        GUIUtility.RotateAroundPivot(-Orientation.GetOrientationAngle(orientation), unrotatedRotationPosition);
        // Draw scaled up texture as outline 
        GUI.color = Color.black;
        Rect originalRect = GetRect(unrotatedRotationPosition - tileObjectType.initialRotationPointOffset * tilePixelSize, tilePixelSize);
        int scaledPixelSize = 5;
        Rect scaledRect = new Rect(originalRect.x - scaledPixelSize, originalRect.y - scaledPixelSize, originalRect.width + scaledPixelSize * 2, originalRect.height + scaledPixelSize * 2);
        GUI.DrawTexture(scaledRect, tileObjectType.previewTexture);
        GUI.color = Color.white;
        GUIUtility.RotateAroundPivot(Orientation.GetOrientationAngle(orientation), unrotatedRotationPosition);
    }

    public void DrawDrag(int tilePixelSize, Vector2 graphOffset, Vector2 dragOffset)
    {
        Vector2 unrotatedRotationPosition = graphOffset + dragOffset + (gridPositionGUI + new Vector2(0.5f, 0.5f) - tileObjectType.GetRotatedCenterPointOffsetGUI(orientation) + tileObjectType.GetRotatedRotationPointOffsetGUI(orientation)) * tilePixelSize;
        GUI.color = new Color(1f, 1f, 1f, .25f);
        GUIUtility.RotateAroundPivot(-Orientation.GetOrientationAngle(orientation), unrotatedRotationPosition);
        GUI.DrawTexture(GetRect(unrotatedRotationPosition - tileObjectType.initialRotationPointOffset * tilePixelSize, tilePixelSize), tileObjectType.previewTexture);
        GUI.color = Color.white;
        GUIUtility.RotateAroundPivot(Orientation.GetOrientationAngle(orientation), unrotatedRotationPosition);
    }

    public void DrawDrag(Vector2 mousePosition, int tilePixelSize, Orientation.orientation orientation)
    {
        GUI.color = new Color(1f,1f,1f,.25f);
        GUIUtility.RotateAroundPivot(-Orientation.GetOrientationAngle(orientation), mousePosition);
        GUI.DrawTexture(GetRect(mousePosition - tileObjectType.initialRotationPointOffset*tilePixelSize, tilePixelSize), tileObjectType.previewTexture);
        GUI.color = Color.white;
        GUIUtility.RotateAroundPivot(Orientation.GetOrientationAngle(orientation), mousePosition);
    }

    public static void DrawDrawGhost(TileObjectTypeSO tileObjectType ,Vector2 mousePosition, int tilePixelSize, Orientation.orientation orientation)
    {
        GUI.color = new Color(1f, 1f, 1f, .25f);
        GUIUtility.RotateAroundPivot(-Orientation.GetOrientationAngle(orientation), mousePosition);
        GUI.DrawTexture(GetRect(tileObjectType, mousePosition - tileObjectType.initialRotationPointOffset * tilePixelSize, tilePixelSize), tileObjectType.previewTexture);
        GUI.color = Color.white;
        GUIUtility.RotateAroundPivot(Orientation.GetOrientationAngle(orientation), mousePosition);
    }


    public void SetGridPosition(Vector2Int gridPosition)
    {
        this.gridPositionGUI = gridPosition;
        EditorUtility.SetDirty(this);
    }
    

    public void SetOrientation(Orientation.orientation orientation)
    {
        this.orientation = orientation;
        EditorUtility.SetDirty(this);
    }


    private  Rect GetRect(Vector2 worldPosition, int tilePixelSize)
    {
        Vector2 rectSize = GetRectSize(tilePixelSize);

        Rect rect = new Rect(worldPosition, rectSize);

        return rect;
    }
    private static Rect GetRect(TileObjectTypeSO tileObjectType,Vector2 worldPosition, int tilePixelSize)
    {
        Vector2 rectSize = GetRectSize(tileObjectType, tilePixelSize);

        Rect rect = new Rect(worldPosition, rectSize);

        return rect;
    }


    private Vector2 GetRectSize(int tilePixelSize)
    {
        int width = 0;
        int height = 0;

        foreach (Vector2Int initialGridPosition in tileObjectType.initialGridPositionList)
        {
            if (initialGridPosition.x + 1 > width)
                width = initialGridPosition.x + 1;

            if (initialGridPosition.y + 1 > height)
                height = initialGridPosition.y + 1;
        }

        Vector2 rectSize = new Vector2(width, height) * tilePixelSize;

        return rectSize;
    }
    private static Vector2 GetRectSize(TileObjectTypeSO tileObjectType, int tilePixelSize)
    {
        int width = 0;
        int height = 0;

        foreach (Vector2Int initialGridPosition in tileObjectType.initialGridPositionList)
        {
            if (initialGridPosition.x + 1 > width)
                width = initialGridPosition.x + 1;

            if (initialGridPosition.y + 1 > height)
                height = initialGridPosition.y + 1;
        }

        Vector2 rectSize = new Vector2(width, height) * tilePixelSize;

        return rectSize;
    }


    public List<Vector2Int> GetGridPositionListGUI()
    {
        return tileObjectType.GetGridPositionListGUI(gridPositionGUI, orientation);
    }


    // Change the grid position y to Uniy Cordinate
    public void ConvertGridPositionGUItoUnityCordinate(int gridHeight)
    {
        Vector2Int convertedPosition = new Vector2Int(gridPositionGUI.x, gridHeight - gridPositionGUI.y - 1);
        gridPositionUnityCordinate = convertedPosition;
        EditorUtility.SetDirty(this);
    }

#endif
    #endregion
}
