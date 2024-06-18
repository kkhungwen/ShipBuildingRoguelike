using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor.Callbacks;
using UnityEditor;

public class CustomTileMapEditor : EditorWindow
{
    enum EditorState
    {
        defult,
        dragSingle,
        dragMultiple,
        draw,
        erase
    }

    private static EditorState currentState = EditorState.defult;
    private static TileMapSO currentTileMap;
    private static TileObjectSO selectedSingleTileObject;
    private TileObjectTypeSO drawingTileObjectType;

    [Header("CONTROLS")]
    private static List<TileObjectSO> selectionObjectList = new List<TileObjectSO>();
    private static Vector2 graphOffset;
    private static Vector2 dragOffset;
    private Orientation.orientation previewOrientation;
    bool isLeftShiftKeyDown;
    bool isLeftControlKeyDown;

    [Header("GRID")]
    private static CustomGrid<GridObject> grid;
    private static int gridWidth = 10;
    private static int gridHeight = 10;
    private const int defultGridPixelSize = 50;
    private static int adjustGridPixelSize;

    #region GridObject
    public class GridObject
    {
        public TileObjectSO ocuupiedTileObject = null;
        public bool isOccupied = false;
    }
    #endregion

    /// <summary>
    /// Create menu selection for opening the editor window
    /// </summary>
    [MenuItem("Custom Tile Map Editor", menuItem = "Window/Custom Editor/Custom Tile Map Editor")]
    private static void OpenWindow()
    {
        GetWindow<CustomTileMapEditor>("Custom Tile Map Editor");
    }

    /// <summary>
    /// Open the editor window and reset editor state if TileMapSO asset has been double click
    /// </summary>
    [OnOpenAsset(0)]
    public static bool OnDoubleClickAsset(int instanceID, int line)
    {
        TileMapSO tileMap = EditorUtility.InstanceIDToObject(instanceID) as TileMapSO;

        if (tileMap != null)
        {
            OpenWindow();

            currentState = EditorState.defult;

            selectedSingleTileObject = null;

            ClearTileObjectSelection();

            currentTileMap = tileMap;

            currentTileMap.GetMapWidthHeight(out gridWidth, out gridHeight);

            adjustGridPixelSize = defultGridPixelSize;

            UpdateGrid();

            graphOffset = Vector2.zero;

            return true;
        }
        return false;
    }


    private void OnGUI()
    {
        if (currentTileMap != null)
        {
            DrawBackgroundGrid(adjustGridPixelSize, 0.3f, Color.gray, graphOffset);

            ProcessEvents(Event.current);

            DrawTileObject();

            DrawSelectedTileObject();

            DrawTileObjectDrag();

            DrawTileObjectDrawGhost();

            DrawToolTipText();
        }
        if (GUI.changed)
        {
            Repaint();
        }
    }

    private void ProcessEvents(Event currentEvent)
    {
        switch (currentEvent.type)
        {
            case EventType.MouseDown:
                ProcessMouseDownEvent(currentEvent);
                break;

            case EventType.MouseUp:
                ProcessMouseUpEvent(currentEvent);
                break;

            case EventType.MouseDrag:
                ProcessMouseDragEvent(currentEvent);
                break;

            case EventType.KeyDown:
                ProcessKeyDownEvent(currentEvent);
                break;

            case EventType.KeyUp:
                ProcessKeyUpEvent(currentEvent);
                break;

            case EventType.ScrollWheel:
                ProcessScrollWheelEvent(currentEvent);
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// DRAW METHODS
    /// </summary>
    #region DRAW METHODS
    // Draw lines of background grid
    private void DrawBackgroundGrid(float gridSize, float gridOpacity, Color gridColor, Vector2 graphOffset)
    {
        int verticalLineCount = gridWidth + 1;
        int horizontalLineCount = gridHeight + 1;

        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        for (int i = 0; i < verticalLineCount; i++)
        {
            Handles.DrawLine(new Vector3(i * gridSize, 0, 0) + (Vector3)graphOffset, new Vector3(i * gridSize, gridHeight * gridSize, 0) + (Vector3)graphOffset, 0f);
        }

        for (int i = 0; i < horizontalLineCount; i++)
        {
            Handles.DrawLine(new Vector3(0, i * gridSize, 0) + (Vector3)graphOffset, new Vector3(gridWidth * gridSize, i * gridSize, 0) + (Vector3)graphOffset, 0f);
        }
    }

    // Loop through TileObjectSO and draw 
    private void DrawTileObject()
    {
        foreach (TileObjectSO tileObject in currentTileMap.tileObjectList)
        {
            tileObject.Draw(adjustGridPixelSize, graphOffset);
        }

        GUI.changed = true;
    }

    // Draw the transparent object texture on mouse position if dragging
    private void DrawTileObjectDrag()
    {
        switch (currentState)
        {
            case EditorState.dragSingle:
                if (selectedSingleTileObject != null)
                {
                    selectedSingleTileObject.DrawDrag(Event.current.mousePosition, adjustGridPixelSize, previewOrientation);

                    GUI.changed = true;
                }
                break;

            case EditorState.dragMultiple:
                foreach (TileObjectSO tileObject in selectionObjectList)
                {
                    tileObject.DrawDrag(adjustGridPixelSize, graphOffset, dragOffset);

                    GUI.changed = true;
                }
                break;

            default:
                break;
        }

    }

    private void DrawTileObjectDrawGhost()
    {
        if (currentState == EditorState.draw)
        {
            if (drawingTileObjectType != null)
            {
                TileObjectSO.DrawDrawGhost(drawingTileObjectType, Event.current.mousePosition, adjustGridPixelSize, previewOrientation);
            }
        }
    }

    // Draw the current selected part with outline
    private void DrawSelectedTileObject()
    {
        foreach (TileObjectSO tileObject in selectionObjectList)
        {
            tileObject.DrawSelectedBackGround(adjustGridPixelSize, graphOffset);
        }
        foreach (TileObjectSO tileObject in selectionObjectList)
        {
            tileObject.Draw(adjustGridPixelSize, graphOffset);
        }
        GUI.changed = true;
    }

    // Draw tooltip text
    private void DrawToolTipText()
    {
        Vector2 mousePosition = Event.current.mousePosition;
        switch (currentState)
        {
            case EditorState.draw:
                GUI.Label(new Rect(mousePosition.x, mousePosition.y - 20, 100, 20), "=ÉÃ µ§¨ê ÉÃ=");
                break;
            case EditorState.erase:
                GUI.Label(new Rect(mousePosition.x, mousePosition.y - 20, 100, 20), "~É@ ¾ó¥ÖÀ¿ É@~");
                break;
            default:
                break;
        }
    }
    #endregion


    /// <summary>
    /// EVENTS METHODS
    /// </summary>
    #region HANDLE MOUSE DOWN
    private void ProcessMouseDownEvent(Event currentEvent)
    {
        // RIGHT CLICK
        if (currentEvent.button == 1)
        {
            ProcessRightClickEvent(currentEvent);
        }

        // LEFT CLICK
        if (currentEvent.button == 0)
        {
            ProcessLeftClickEvent(currentEvent);
        }
    }

    // RIGHT CLICK
    private void ProcessRightClickEvent(Event currentEvent)
    {
        switch (currentState)
        {
            case EditorState.defult:
                // right click on tile object
                if (TryGetMousePositionOccupiedTileObject(currentEvent.mousePosition,out TileObjectSO tileObjectSO))
                {
                    if (selectionObjectList.Contains(tileObjectSO))
                    {
                        // show delete all select tile object context
                        ShowRightClickTileObjectContextMenu(selectionObjectList);
                    }
                    else
                    {
                        // show delete single select tile object context
                        ShowRightClickTileObjectContextMenu(new List<TileObjectSO>() { tileObjectSO });
                    }
                }
                // right click on blank space
                else
                {
                    ShowRightClickGraphContextMenu(currentEvent);
                }
                break;

            case EditorState.draw:
                ExitDraw();
                break;

            case EditorState.erase:
                ExitErase();
                break;

            default:
                break;
        }
    }

    // LEFT CLICK
    private void ProcessLeftClickEvent(Event currentEvent)
    {
        switch (currentState)
        {
            case EditorState.defult:
                if (isLeftShiftKeyDown)
                {

                }
                else if (isLeftControlKeyDown)
                {
                    // ctrl + left click  on object
                    if (TryGetMousePositionOccupiedTileObject(currentEvent.mousePosition, out TileObjectSO tileObject))
                    {
                        AddTileObjectToSelection(tileObject);
                    }
                }
                else
                {
                    // Left click on object
                    if (TryGetMousePositionOccupiedTileObject(currentEvent.mousePosition, out TileObjectSO tileObject))
                    {
                        // Left click on multiple selected object
                        if (selectionObjectList.Contains(tileObject) && selectionObjectList.Count > 1)
                        {
                            EnterDragMultiple();
                        }
                        // Left click on single selected object or unselected object
                        else
                        {
                            ClearTileObjectSelection();
                            AddTileObjectToSelection(tileObject);

                            selectedSingleTileObject = tileObject;
                            previewOrientation = selectedSingleTileObject.orientation;
                            EnterDragSingle();
                        }
                    }
                    // Left click on blank space
                    else
                    {
                        ClearTileObjectSelection();

                        selectedSingleTileObject = null;
                    }
                }

                break;

            case EditorState.draw:
                TryPlaceTileObject(drawingTileObjectType, currentEvent.mousePosition, previewOrientation);
                break;

            case EditorState.erase:
                if (TryGetMousePositionOccupiedTileObject(currentEvent.mousePosition, out TileObjectSO tileObjectSO))
                {
                    DeleteTileObject(new List<TileObjectSO>() { tileObjectSO });
                }
                break;

            default:
                break;
        }

    }
    #endregion

    #region HANDLE MOUSE DRAG
    private void ProcessMouseDragEvent(Event currentEvent)
    {
        // LEFT CLICK DRAG
        if (currentEvent.button == 0)
        {
            ProcessLeftClickMouseDragEvent(currentEvent);
        }
    }

    private void ProcessLeftClickMouseDragEvent(Event currentEvent)
    {
        switch (currentState)
        {
            case EditorState.defult:
                graphOffset += currentEvent.delta;
                break;

            case EditorState.dragMultiple:
                dragOffset += currentEvent.delta;
                break;

            case EditorState.draw:
                TryPlaceTileObject(drawingTileObjectType, currentEvent.mousePosition, previewOrientation);
                break;

            case EditorState.erase:
                if (TryGetMousePositionOccupiedTileObject(currentEvent.mousePosition, out TileObjectSO tileObjectSO))
                {
                    DeleteTileObject(new List<TileObjectSO>() { tileObjectSO });
                }
                break;

            default:
                break;
        }
    }
    #endregion

    #region HANDLE MOUSE UP
    private void ProcessMouseUpEvent(Event currentEvent)
    {
        // LEFT MOUSE UP
        if (currentEvent.button == 0)
        {
            ProcessLeftMouseUpEvent(currentEvent);
        }
    }

    private void ProcessLeftMouseUpEvent(Event currentEvent)
    {
        switch (currentState)
        {
            // Try place single tileObject
            case EditorState.dragSingle:
                if (selectedSingleTileObject != null)
                {
                    TryPlaceTileObject(selectedSingleTileObject, currentEvent.mousePosition);
                }
                ExitDragSingle();
                break;
            
            // Try place multiple tileObject
            case EditorState.dragMultiple:

                Vector2 dragCellSizeOffset = dragOffset / grid.GetCellSize();
                Vector2Int dragGridPositionOffset = Vector2Int.zero;

                if (dragCellSizeOffset.x < 0.5) dragGridPositionOffset.x = Mathf.FloorToInt(dragCellSizeOffset.x + 0.5f);
                if (dragCellSizeOffset.x > 0.5) dragGridPositionOffset.x = Mathf.CeilToInt(dragCellSizeOffset.x - 0.5f);
                if (dragCellSizeOffset.y < 0.5) dragGridPositionOffset.y = Mathf.FloorToInt(dragCellSizeOffset.y + 0.5f);
                if (dragCellSizeOffset.y > 0.5) dragGridPositionOffset.y = Mathf.CeilToInt(dragCellSizeOffset.y - 0.5f);

                TryPlaceTileObject(selectionObjectList, dragGridPositionOffset);

                ExitDragMultiple();
                break;

            default:
                break;
        }
    }
    #endregion

    #region HANDLE KEY DOWN
    private void ProcessKeyDownEvent(Event currentEvent)
    {
        if (currentEvent.keyCode == KeyCode.R)
        {
            ProcessRKeyDownEvent();
        }
        else if (currentEvent.keyCode == KeyCode.Escape)
        {
            ProcessEscapeKeyDownEvent();
        }
        else if (currentEvent.keyCode == KeyCode.LeftShift)
        {
            ProcessLeftShiftKeyDownEvent();
        }
        else if (currentEvent.keyCode == KeyCode.LeftControl)
        {
            ProcessLeftControlKeyDownEvent();
        }
    }

    private void ProcessRKeyDownEvent()
    {
        switch (currentState)
        {
            case EditorState.dragSingle:
                if (selectedSingleTileObject.tileObjectType.isRotatable)
                    previewOrientation = Orientation.GetNextOrientation(previewOrientation);
                break;
            case EditorState.draw:
                if (drawingTileObjectType.isRotatable)
                    previewOrientation = Orientation.GetNextOrientation(previewOrientation);
                break;

            default:
                break;
        }
    }

    private void ProcessEscapeKeyDownEvent()
    {
        switch (currentState)
        {
            case EditorState.draw:
                ExitDraw();
                break;
            case EditorState.erase:
                ExitErase();
                break;
            default:
                break;
        }
    }

    private void ProcessLeftShiftKeyDownEvent()
    {
        isLeftShiftKeyDown = true;
    }

    private void ProcessLeftControlKeyDownEvent()
    {
        isLeftControlKeyDown = true;
    }
    #endregion

    #region HANDLE KEY UP
    private void ProcessKeyUpEvent(Event currentEvent)
    {
        if (currentEvent.keyCode == KeyCode.LeftShift)
        {
            ProcessLeftShiftKeyUpEvent();
        }
        else if (currentEvent.keyCode == KeyCode.LeftControl)
        {
            ProcessLeftControlKeyUpEvent();
        }
    }

    private void ProcessLeftShiftKeyUpEvent()
    {
        isLeftShiftKeyDown = false;
    }

    private void ProcessLeftControlKeyUpEvent()
    {
        isLeftControlKeyDown = false;
    }
    #endregion

    #region HANDLE SCROLL WHEEL
    private void ProcessScrollWheelEvent(Event currentEvent)
    {
        Vector2 delta = currentEvent.delta;
        if (delta.y > 0)
        {
            adjustGridPixelSize -= 5;
        }
        else if (delta.y < 0)
        {
            adjustGridPixelSize += 5;
        }
        adjustGridPixelSize = Mathf.Clamp(adjustGridPixelSize, 5, 100);
        UpdateGrid();
    }
    #endregion


    /// <summary>
    /// STATE CONTROL
    /// </summary>
    #region STATE CONTROL
    private void EnterDraw(TileObjectTypeSO tileObjectTypeSO)
    {
        ClearTileObjectSelection();
        drawingTileObjectType = tileObjectTypeSO;
        previewOrientation = Orientation.orientation.East;
        currentState = EditorState.draw;
    }

    private void ExitDraw()
    {
        drawingTileObjectType = null;  
        currentState = EditorState.defult;
    }

    private void EnterErase()
    {
        ClearTileObjectSelection();
        currentState = EditorState.erase;
    }

    private void ExitErase()
    {
        currentState = EditorState.defult;
    }

    private void EnterDragSingle()
    {
        currentState = EditorState.dragSingle;
    }

    private void ExitDragSingle()
    {
        currentState = EditorState.defult;
    }

    private void EnterDragMultiple()
    {
        dragOffset = Vector2.zero;
        currentState = EditorState.dragMultiple;
    }

    private void ExitDragMultiple()
    {
        dragOffset = Vector2.zero;
        currentState = EditorState.defult;
    }
    #endregion


    /// <summary>
    /// CONTEXT MENUS
    /// </summary>
    #region CONTEXT MENUS
    // Right click object ( DELETE )
    private void ShowRightClickTileObjectContextMenu(List<TileObjectSO> tileObjectList)
    {
        GenericMenu menu = new GenericMenu();

        menu.AddItem(new GUIContent("Delete"), false, DeleteShipPart, tileObjectList);

        menu.ShowAsContext();
    }
    private void DeleteShipPart(object tileObjectListObject)
    {
        List <TileObjectSO> tileObjectToDeleteList = tileObjectListObject as List<TileObjectSO>;

        DeleteTileObject(tileObjectToDeleteList);
    }

    // Right click Graph ( DRAW, ERASE )
    private void ShowRightClickGraphContextMenu(Event currentEvent)
    {
        GenericMenu menu = new GenericMenu();

        foreach(TileObjectTypeListSO tileObjectTypePallete in currentTileMap.tileObjectTypePalleteArray)
        {
            foreach(TileObjectTypeSO tileObjectType in tileObjectTypePallete.list)
            {
                menu.AddItem(new GUIContent("Draw /" + tileObjectTypePallete.name + "/" + tileObjectType.name), false, EnterDraw, tileObjectType);
            }
        }

        menu.AddItem(new GUIContent("Eraser"), false, EnterErase);

        menu.ShowAsContext();
    }
    private void EnterDraw(object shipPartTypeObject)
    {
        EnterDraw(shipPartTypeObject as TileObjectTypeSO);
    }

    #endregion


    /// <summary>
    /// Misc
    /// </summary>
    #region SELECTION
    private void AddTileObjectToSelection(TileObjectSO tileObject)
    {
        if (!selectionObjectList.Contains(tileObject))
        {
            selectionObjectList.Add(tileObject);
            Selection.objects = selectionObjectList.ToArray();
        }
    }

    private static void ClearTileObjectSelection()
    {
        selectionObjectList.Clear();
        Selection.objects = null;
    }
    #endregion

    private void TryPlaceTileObject(TileObjectTypeSO tileObjectType, Vector2 mousePosition, Orientation.orientation orientation)
    {
        Vector2 OffsetMousePosition = mousePosition - graphOffset - tileObjectType.GetRotatedRotationPointOffsetGUI(orientation) * adjustGridPixelSize + tileObjectType.GetRotatedCenterPointOffsetGUI(orientation) * adjustGridPixelSize;
        grid.GetXY(OffsetMousePosition, out int x, out int y);
        Vector2Int mouseGridCenterPointPosition = new Vector2Int(x, y);
        List<Vector2Int> occupiedPosition = tileObjectType.GetGridPositionListGUI(mouseGridCenterPointPosition, orientation);

        if (!IsGridPositionOccupied(occupiedPosition))
        {
            CreateTileObject(tileObjectType, mouseGridCenterPointPosition, orientation);
        }
        else
        {
            // position is occupied
        }
    }

    private void TryPlaceTileObject(TileObjectSO tileObject, Vector2 mousePosition)
    {
        Vector2 OffsetMousePosition = mousePosition - graphOffset - tileObject.tileObjectType.GetRotatedRotationPointOffsetGUI(previewOrientation) * adjustGridPixelSize + tileObject.tileObjectType.GetRotatedCenterPointOffsetGUI(previewOrientation) * adjustGridPixelSize;
        grid.GetXY(OffsetMousePosition, out int x, out int y);
        Vector2Int mouseGridCenterPointPosition = new Vector2Int(x, y);
        List<Vector2Int> occupiedPosition = tileObject.tileObjectType.GetGridPositionListGUI(mouseGridCenterPointPosition, previewOrientation);

        if (!IsGridPositionOccupiedExclude(occupiedPosition, new List<TileObjectSO>() { tileObject }))
        {
            PlaceTileObject(tileObject,mouseGridCenterPointPosition,previewOrientation);
        }
        else
        {
            // position is occupied
        }
    }

    private void TryPlaceTileObject(List<TileObjectSO> tileObjectSOList, Vector2Int gridPositionOffset)
    {

        bool isGridPositionOccupied = false;

        foreach (TileObjectSO tileObject in tileObjectSOList)
        {
            List<Vector2Int> occupiedPosition = tileObject.tileObjectType.GetGridPositionListGUI(tileObject.gridPositionGUI + gridPositionOffset, tileObject.orientation);

            if (IsGridPositionOccupiedExclude(occupiedPosition, tileObjectSOList))
            {
                isGridPositionOccupied = true;
            }
        }

        if (!isGridPositionOccupied)
        {
            foreach (TileObjectSO tileObject in tileObjectSOList)
            {
                PlaceTileObject(tileObject, tileObject.gridPositionGUI + gridPositionOffset, tileObject.orientation);
            }
        }
    }

    private void PlaceTileObject(TileObjectSO tileObject, Vector2Int gridPosition, Orientation.orientation orientation)
    {
        tileObject.SetGridPosition(gridPosition);
        tileObject.orientation = orientation;
        UpdateGrid();
        ConvertTileObjectGridPositionToUnityCordinate();
    }

    private void CreateTileObject(TileObjectTypeSO tileObjectType, Vector2Int gridPosition, Orientation.orientation orientation)
    {
        //TileObjectSO tileObject = ScriptableObject.CreateInstance<TileObjectSO>();

        TileObjectSO tileObject = tileObjectType.CreateTileObject();

        currentTileMap.tileObjectList.Add(tileObject);

        tileObject.Initialize(tileObjectType, gridPosition, orientation);

        AssetDatabase.AddObjectToAsset(tileObject, currentTileMap);
        AssetDatabase.SaveAssets();

        UpdateGrid();
        ConvertTileObjectGridPositionToUnityCordinate();
    }

    private void DeleteTileObject(List<TileObjectSO> tileObjectToDeleteList)
    {
        Queue<TileObjectSO> tileObjectToDeleteQueue = new Queue<TileObjectSO>();

        foreach(TileObjectSO tileObject in tileObjectToDeleteList)
        {
            tileObjectToDeleteQueue.Enqueue(tileObject);
        }

        tileObjectToDeleteList.Clear();

        while (tileObjectToDeleteQueue.Count > 0)
        {
            TileObjectSO tileObjectToDelete = tileObjectToDeleteQueue.Dequeue();
            currentTileMap.tileObjectList.Remove(tileObjectToDelete);
            UpdateGrid();
            DestroyImmediate(tileObjectToDelete, true);
            AssetDatabase.SaveAssets();
        }
    }

    private bool IsGridPositionOccupied(List<Vector2Int> checkPositionList)
    {
        foreach (Vector2Int position in checkPositionList)
        {
            if (grid.GetValue(position.x, position.y) != null)
            {
                if (grid.GetValue(position.x, position.y).isOccupied)
                {
                    return true;
                }
            }
            else
            {
                Debug.Log("grid does not contain occupied check position");
                return true;
            }
        }
        return false;
    }


    private bool IsGridPositionOccupiedExclude(List<Vector2Int> checkPositionList, List<TileObjectSO> excludeShipPartList)
    {
        foreach (Vector2Int position in checkPositionList)
        {
            GridObject gridObject = grid.GetValue(position.x, position.y);
            if (gridObject != null)
            {
                if (gridObject.isOccupied && !excludeShipPartList.Contains( gridObject.ocuupiedTileObject))
                {
                    return true;
                }
            }
            else
            {
                Debug.Log("grid does not contain occupied check position");
                return true;
            }
        }
        return false;
    }


    // Updates the gridObjects and grid pixel size in grid. Used when open editor, making changes on tileObject, changes the grid pixelsize
    private static void UpdateGrid()
    {
        grid = new CustomGrid<GridObject>(gridWidth, gridHeight, adjustGridPixelSize, Vector2.zero, () => new GridObject());

        foreach (TileObjectSO tileObject in currentTileMap.tileObjectList)
        {
            foreach (Vector2Int occupiedGridPosition in tileObject.GetGridPositionListGUI())
            {
                //Debug.Log("occupie position "grid.GetValue(occupiedGridPosition.x, occupiedGridPosition.y));

                GridObject gridObject = grid.GetValue(occupiedGridPosition.x, occupiedGridPosition.y);
                gridObject.isOccupied = true;
                gridObject.ocuupiedTileObject = tileObject;
            }
        }
    }


    private bool TryGetMousePositionGridObject(Vector3 mousePosition, out GridObject gridObject)
    {
        Vector3 offsetMousePosition = new Vector3(mousePosition.x - graphOffset.x, mousePosition.y - graphOffset.y, 0);
        GridObject returnGridObject = grid.GetValue(offsetMousePosition);

        if (returnGridObject != null)
        {
            gridObject = returnGridObject;
            return true;
        }
        else
        {
            Debug.Log("mouse out of grid range");
            gridObject = null;
            return false;
        }
    }

    private bool TryGetMousePositionOccupiedTileObject(Vector3 mousePosition, out TileObjectSO tileObjectSO)
    {
        if (TryGetMousePositionGridObject(mousePosition, out GridObject gridObject))
        {
            if (gridObject.ocuupiedTileObject != null)
            {
                tileObjectSO = gridObject.ocuupiedTileObject;
                return true;
            }
            else
            {
                //Grid object does not contain ship part
            }
        }

        tileObjectSO = null;
        return false;
    }



    // Change the grid position y to Uniy Cordinate
    private void ConvertTileObjectGridPositionToUnityCordinate()
    {
        foreach (TileObjectSO tileObject in currentTileMap.tileObjectList)
        {
            tileObject.ConvertGridPositionGUItoUnityCordinate(gridHeight);
        }
    }
}


