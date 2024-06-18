using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartData : IShipObject
{
    public ShipPartData(TileObjectTypeSO tileObjectType, Vector2Int gridPosition, Orientation.orientation orientation, GameObject prefab, Sprite iconSprite, List<AttributeData> attributeDataList)
    {
        this.tileObjectType = tileObjectType;
        this.gridPosition = gridPosition;
        this.orientation = orientation;
        this.prefab = prefab;
        this.iconSprite = iconSprite;

        LoadAttributes(attributeDataList);
    }

    private TileObjectTypeSO tileObjectType;

    private Vector2Int gridPosition;

    private Orientation.orientation orientation;

    private GameObject prefab;

    private Sprite iconSprite;

    private Dictionary<AttributeTypeSO, Attribute> attributeDictionary;

    private int level = 15;

    // Ship reference to subscribe and unsubscribe to Events, and the parent transform when instantiating ship part instance
    private Ship ship;

    // Reference to the ship part instance
    private ShipPart shipPart;

    // Caching List of grid position to reduce garbage collect
    private List<Vector2Int> gridPositionList = new List<Vector2Int>();

    private void LoadAttributes(List<AttributeData> attributeDataList)
    {
        attributeDictionary = new Dictionary<AttributeTypeSO, Attribute>();

        foreach (AttributeData attributeData in attributeDataList)
        {
            Attribute attribute = new Attribute(attributeData.normalized_ValueCurve);
            attributeDictionary.Add(attributeData.attributeType, attribute);
        }
    }

    public void SubscribeToShipEvents(Ship ship)
    {
        this.ship = ship;
        ship.OnLoad += Ship_OnLoad;
    }

    private void Ship_OnLoad()
    {
        LoadInstance();
    }

    private void LoadInstance()
    {
        shipPart = ObjectPoolManager.Instance.GetComponentFromPool(prefab) as ShipPart;

        shipPart.gameObject.SetActive(true);

        shipPart.Initialize(this, tileObjectType, ship.transform, gridPosition, orientation);
    }

    public TileObjectTypeSO GetTileObjectType()
    {
        return tileObjectType;
    }

    public List<Vector2Int> GetGridPositionList()
    {
        return tileObjectType.GetGridPositionListUnityCordinate(gridPositionList, gridPosition, orientation);
    }

    public float GetAttributeValue(AttributeTypeSO attributeType)
    {
        if (attributeDictionary.TryGetValue(attributeType, out Attribute attribute))
        {
            return attribute.GetValue(level);
        }

        Debug.Log(this + " attribute list does not contain " + attributeType);
        return 0f;
    }
}
