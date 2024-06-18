using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BreakableBlockData : IWorldObject
{
    public BreakableBlockData(TileObjectTypeSO tileObjectType, Vector2Int gridPosition, Orientation.orientation orientation, GameObject prefab, Sprite defaultSprite, bool isSpriteRuleTile, SpriteRuleTileSO spriteRuleTile)
    {
        this.tileObjectType = tileObjectType;
        this.gridPosition = gridPosition;
        this.orientation = orientation;
        this.prefab = prefab;
        this.defaultSprite = defaultSprite;
        this.isSpriteRuleTile = isSpriteRuleTile;
        this.spriteRuleTile = spriteRuleTile;
    }

    private TileObjectTypeSO tileObjectType;

    private Orientation.orientation orientation;

    // Chunk grid position
    private Vector2Int gridPosition;

    // world grid position Set by world generator  
    private Vector2Int worldCellGridPosition;

    // Defult sprite of the tile if not sprite rule tile
    private Sprite defaultSprite;

    // Sprite rule tile currently only support 1x1 tile
    private bool isSpriteRuleTile;
    private SpriteRuleTileSO spriteRuleTile;
    private Sprite ruleSprite;

    // Prefab Uused as Key to get instance gameobject from object pool
    private GameObject prefab;

    // Referance to the instance gameobject from object pool
    private BreakableBlock breakableBlock;

    // Cache the List of surrounding position to reduce garbage collection
    private List<Vector2Int> surroundingPositionList = new List<Vector2Int>();

    // Caching List of grid position to reduce garbage collect
    private List<Vector2Int> gridPositionList = new List<Vector2Int>();



    // Methods for worldGenerator to initialize the position and orientation in chunk space
    public void InitializeGridPositionOrientation(Vector2Int gridPosition, Orientation.orientation orientation)
    {
        this.gridPosition = gridPosition;
        this.orientation = orientation;
    }


    // Call by chunk to subscribe to chunk events
    public void SubscribeToChunkEvents(Chunk chunk)
    {
        //Set world grid position
        worldCellGridPosition = chunk.worldGridPosition * Settings.chunkWidth + gridPosition;

        //Subscribe to chunk's events
        chunk.OnLoad += Chunk_OnLoad;
        chunk.OnUnload += Chunk_OnUnload;

        if (isSpriteRuleTile)
            chunk.OnInitializeRuleSprite += Chunk_OnInitializeRuleSprite;
    }

    // Chunk Events
    private void Chunk_OnLoad()
    {
        LoadInstance();
    }
    private void Chunk_OnUnload()
    {
        UnloadInstance();
    }
    private void Chunk_OnInitializeRuleSprite(CustomGrid<ChunkGridObject> chunkGrid)
    {
        UpdateRuleSprite(chunkGrid);
    }

    private void LoadInstance()
    {
        breakableBlock = ObjectPoolManager.Instance.GetComponentFromPool(prefab) as BreakableBlock;

        if (isSpriteRuleTile)
            breakableBlock.Initialize(tileObjectType, worldCellGridPosition, Orientation.orientation.East, ruleSprite);
        else
            breakableBlock.Initialize(tileObjectType, worldCellGridPosition, orientation, defaultSprite);

        breakableBlock.gameObject.SetActive(true);
    }

    private void UnloadInstance()
    {
        ObjectPoolManager.Instance.ReleaseComponentToPool(prefab, breakableBlock);
    }


    private void UpdateRuleSprite(CustomGrid<ChunkGridObject> chunkGrid)
    {
        Dictionary<Vector2Int, TileObjectTypeSO> surroundingObjectTypeDictionary = new Dictionary<Vector2Int, TileObjectTypeSO>();

        List<Vector2Int> surroundingPositionList = GetSuroundingPositionList();

        for (int i = 0; i < surroundingPositionList.Count; i++)
        {
            if (chunkGrid.TryGetValue(surroundingPositionList[i].x, surroundingPositionList[i].y, out ChunkGridObject chunkGridObject))
            {
                if (chunkGridObject.TryGetWorldObject(out IWorldObject worldObject))
                {
                    surroundingObjectTypeDictionary.Add(surroundingPositionList[i] - gridPosition, worldObject.GetTileObjectType());
                }
            }
        }

        ruleSprite = spriteRuleTile.GetSprite(surroundingObjectTypeDictionary);
    }


    public TileObjectTypeSO GetTileObjectType()
    {
        return tileObjectType;
    }

    public List<Vector2Int> GetSuroundingPositionList()
    {
        return tileObjectType.GetRotatedSurroundingPositionListUnityCordinate(surroundingPositionList, gridPosition, orientation);
    }

    public List<Vector2Int> GetGridPositionList()
    {
        return tileObjectType.GetGridPositionListUnityCordinate(gridPositionList, gridPosition, orientation);
    }
}
